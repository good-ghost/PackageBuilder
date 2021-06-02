Imports PackageBuilder.MetaAPI
Imports System.ComponentModel
Imports System.IO
Imports System.Xml

Public Class BuildProcess

    Dim componentSet As Dictionary(Of String, List(Of Component)) = New Dictionary(Of String, List(Of Component))

    Private Sub BuildProcess_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblMessage.Text = ""

            '' these properties should be set to True (at design-time or runtime) before calling the RunWorkerAsync
            '' to ensure that it supports Cancellation and reporting Progress
            bgw.WorkerSupportsCancellation = True
            bgw.WorkerReportsProgress = True

            '' call this method to start your asynchronous Task.
            bgw.RunWorkerAsync()

        Catch ex As Exception
            Errorbox(ex.Message)
        End Try
    End Sub

    Private Sub bgw_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw.DoWork
        Try
            bgw.ReportProgress(0, "Describe metadata for package build...")
            ' query For the list Of metadata types
            Dim dmr As DescribeMetadataResult = describeMetadata()

            Dim metasize As Integer = dmr.metadataObjects.Length
            Dim percent As Integer = 0
            Dim row_counter As Integer = 1

            Dim metadataObjects() As DescribeMetadataObject = dmr.metadataObjects
            Dim metas = metadataObjects.OrderBy(Function(x) x.xmlName)
            ' loop through metadata types
            For Each meta As DescribeMetadataObject In metas

                bgw.ReportProgress(percent, "build metadata (" & meta.xmlName & ")...")

                ' Component IS NOT in a folder. Eg. normal component
                If Not meta.inFolder And Not isWildcard Then

                    ' set up the component type to query for components
                    Dim lmqp As ListMetadataQuery = New ListMetadataQuery
                    lmqp.type = meta.xmlName

                    Dim metaComponents() As FileProperties = metaClient.listMetadata(metaSessionHeader, {lmqp}, api)

                    If metaComponents IsNot Nothing Then

                        For Each metaComponent As FileProperties In metaComponents

                            ' If the user wants all components, or they don't want any packages  and it's not
                            If includeComponent(options, metaComponent) Then

                                ' create the component record and save
                                addComponent(lmqp.type, metaComponent)

                            ElseIf options = "none" And metaComponent.namespacePrefix IsNot Nothing Then
                                Continue For
                            End If
                        Next

                    End If

                    ' If it has child names, let's use that
                    If meta.childXmlNames IsNot Nothing Then
                        ' Iterate over the child components
                        For Each child As String In meta.childXmlNames

                            ' set up the component type to query for components
                            Dim lmqc As ListMetadataQuery = New ListMetadataQuery
                            lmqc.type = child
                            If lmqc.type = "ManagedTopic" Then
                                ' ManagedTopic is not a valid component Type and it should be "ManagedTopics"
                                lmqc.type = "ManagedTopics"
                            End If

                            Dim fps() As FileProperties = metaClient.listMetadata(metaSessionHeader, {lmqc}, api)

                            If fps IsNot Nothing Then
                                ' loop through the components returned from the component query

                                For Each fp As FileProperties In fps
                                    ' If the user wants all components, or they don't want any packages  and it's not
                                    If includeComponent(options, fp) Then

                                        ' create the component record and save
                                        addComponent(lmqc.type, fp)

                                    End If
                                Next

                            End If
                        Next
                    End If

                ElseIf meta.inFolder Then
                    ' Component is a folder component - eg Dashboard, Document, EmailTemplate, Report

                    ' set up the component type to query for components
                    Dim lmqc As ListMetadataQuery = New ListMetadataQuery
                    ' EmailTemplate = EmailFolder (for some reason)
                    If meta.xmlName = "EmailTemplate" Then
                        lmqc.type = "EmailFolder"
                    Else
                        lmqc.type = meta.xmlName & "Folder"
                    End If

                    Dim folders() As FileProperties = metaClient.listMetadata(metaSessionHeader, {lmqc}, api)

                    If folders IsNot Nothing Then

                        ' Loop through folders
                        For Each folder As FileProperties In folders

                            If includeComponent(options, folder) Then

                                ' create the component folder entry
                                addComponent(meta.xmlName, folder)

                            End If

                            ' Create component for folder to query
                            Dim lmqf As ListMetadataQuery = New ListMetadataQuery
                            lmqf.type = meta.xmlName
                            lmqf.folder = folder.fullName

                            Dim folder_components() As FileProperties = metaClient.listMetadata(metaSessionHeader, {lmqf}, api)

                            If folder_components IsNot Nothing Then
                                Dim compFolderList As List(Of Component) = New List(Of Component)
                                For Each folder_component As FileProperties In folder_components

                                    If includeComponent(options, folder_component) Then

                                        ' create the component folder entry
                                        addComponent(meta.xmlName, folder_component)

                                    End If
                                Next
                            End If
                        Next
                    End If

                End If

                percent = CInt((row_counter / metasize) * 100)
                bgw.ReportProgress(percent, "finishing metadata (" & meta.xmlName & ")...")
                row_counter = row_counter + 1

                '' check at regular intervals for CancellationPending
                If bgw.CancellationPending Then
                    bgw.ReportProgress(percent, "Cancelling...")
                    Exit For
                End If

            Next

            bgw.ReportProgress(percent, "Cleanup built data set...")
            ' If a Then component type has no child components, remove the component type altogether
            ' Unless Is wildcard, In which Case we'll keep them
            ' Or If In folder, we remove it
            If Not isWildcard Then
                For Each componentType As String In componentSet.Keys
                    If componentSet.Item(componentType).Count = 0 Then
                        componentSet.Remove(componentType)
                    End If
                Next
            End If

            bgw.ReportProgress(percent, "Build XML document...")
            Dim doc As XmlDocument = buildXML()

            bgw.ReportProgress(percent, "Build Pretty XML string...")
            Dim content As String = buildPrettyText(doc)
            setControlText(textBox, content)

            bgw.ReportProgress(percent, "Complete...")

            If bgw.CancellationPending Then
                e.Cancel = True
                bgw.ReportProgress(percent, "Cancelled.")
            End If

            GoTo done
        Catch ex As Exception
            setControlText(lblMessage, ex.Message)
            GoTo errors
        End Try

cancel:
        setControlText(lblMessage, "Process canceled...")
        e.Cancel = True
        GoTo done
errors:
        e.Cancel = True
done:
    End Sub

    Private Sub bgw_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgw.ProgressChanged
        '' This event is fired when you call the ReportProgress method from inside your DoWork.
        '' Any visual indicators about the progress should go here.
        progressBar.Value = CInt(e.ProgressPercentage)
        lblMessage.Text = e.UserState
    End Sub

    Private Sub bgw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bgw.RunWorkerCompleted
        '' This event is fired when your BackgroundWorker exits.
        '' It may have exitted Normally after completing its task, 
        '' or because of Cancellation, or due to any Error.

        If e.Error IsNot Nothing Then
            '' if BackgroundWorker terminated due to error
            'MessageBox.Show(e.Error.Message)
            lblMessage.Text = "Error occurred!" & e.Error.Message

        ElseIf e.Cancelled Then
            '' otherwise if it was cancelled
            'MessageBox.Show("Download cancelled!")
            lblMessage.Text = "Process Cancelled!"
        Else
            lblMessage.Text = "Process completed!"
        End If

        btnExit.Text = "Exit"
        btnExit.Enabled = True
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub setControlText(ByVal ctl As Control, ByVal text As String)
        If ctl.InvokeRequired Then
            ctl.Invoke(New setControlTextInvoker(AddressOf setControlText), ctl, text)
        Else
            ctl.Text = text
        End If
    End Sub
    Private Delegate Sub setControlTextInvoker(ByVal ctl As Control, ByVal text As String)

    Sub addComponent(ByVal key As String, ByVal fp As FileProperties)
        ' create the component folder entry
        Dim comp As Component = New Component
        comp.component_type = key
        comp.name = fp.fullName

        Dim compList As List(Of Component) = New List(Of Component)
        If componentSet.ContainsKey(key) Then
            compList = componentSet.Item(key)
            compList.Add(comp)
            componentSet(key) = compList
        Else
            compList.Add(comp)
            componentSet.Add(key, compList)
        End If

    End Sub

    Function buildXML() As XmlDocument
        Dim doc As XmlDocument = New XmlDocument()
        Try
            Dim docNode As XmlNode = doc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
            doc.AppendChild(docNode)

            Dim packageNode As XmlNode = doc.CreateElement("Package")
            Dim xmlns As XmlAttribute = doc.CreateAttribute("xmlns")
            xmlns.Value = "http://soap.sforce.com/2006/04/metadata"
            packageNode.Attributes.Append(xmlns)
            doc.AppendChild(packageNode)

            Dim keys() As String = componentSet.Keys.ToArray()
            Array.Sort(keys)

            For Each key As String In keys
                Dim typesNode As XmlNode = doc.CreateElement("types")

                Dim comps() As Component = componentSet.Item(key).ToArray()
                Dim sorted = comps.OrderBy(Function(x) x.name)
                For Each comp As Component In sorted
                    Dim members As XmlElement = doc.CreateElement(String.Empty, "members", String.Empty)
                    Dim member As XmlText = doc.CreateTextNode(comp.name)
                    members.AppendChild(member)
                    typesNode.AppendChild(members)
                Next

                Dim nameEl As XmlElement = doc.CreateElement(String.Empty, "name", String.Empty)
                Dim name As XmlText = doc.CreateTextNode(key)
                nameEl.AppendChild(name)
                typesNode.AppendChild(nameEl)
                packageNode.AppendChild(typesNode)
            Next

            Dim versionNode As XmlElement = doc.CreateElement(String.Empty, "version", String.Empty)
            Dim version As XmlText = doc.CreateTextNode(api.ToString("F1"))
            versionNode.AppendChild(version)
            packageNode.AppendChild(versionNode)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return doc
    End Function

    Function buildPrettyText(ByVal doc As XmlDocument) As String
        Dim mStream As MemoryStream = New MemoryStream()
        Dim writer As XmlTextWriter = New XmlTextWriter(mStream, System.Text.Encoding.UTF8)

        writer.Formatting = Formatting.Indented

        ' Write the XML into a formatting XmlTextWriter
        doc.WriteContentTo(writer)
        writer.Flush()
        mStream.Flush()

        ' Have to rewind the MemoryStream in order to read
        ' its contents.
        mStream.Position = 0

        ' Read MemoryStream contents into a StreamReader.
        Dim sReader As StreamReader = New StreamReader(mStream)
        ' Extract the text from the StreamReader.
        Dim formattedXml As String = sReader.ReadToEnd()

        Return formattedXml
    End Function

    Function describeMetadata() As DescribeMetadataResult
        Dim dmr As DescribeMetadataResult

        dmr = metaClient.describeMetadata(metaSessionHeader, api)
        Return dmr
    End Function

    Function listMetadata(ByVal types As String()) As FileProperties()
        Dim fileObjs() As FileProperties

        Dim metaTypes As List(Of ListMetadataQuery) = New List(Of ListMetadataQuery)
        For Each mtype As String In types
            Dim metaType As ListMetadataQuery = New ListMetadataQuery()
            metaType.type = mtype
            metaType.folder = vbNullString
            metaTypes.Add(metaType)
        Next

        fileObjs = metaClient.listMetadata(metaSessionHeader, metaTypes.ToArray(), api)
        Return fileObjs
    End Function

    Function includeComponent(ByRef options As String, ByRef component As FileProperties) As Boolean
        If options = "all" Then
            Return True
        ElseIf options = "none" Then
            If component.namespacePrefix Is Nothing Then
                Return True
            Else
                Return False
            End If
        ElseIf options = "unmanaged" Then
            If component.manageableState = Nothing Then
                Return True
            Else
                If component.manageableState = "unmanaged" Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If

        Return True
    End Function

End Class