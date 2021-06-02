Imports PackageBuilder.MetaAPI
Imports System.IO
Imports System.Xml


Module Main

    Public api As Double = 51.0

    Public accessToken As String
    Public refreshToken As String
    Public tokenType As String
    Public issuedAt As String
    Public id As String
    Public instanceUrl As String
    Public conInfo As ConnectionInfo

    Public metaClient As MetadataPortTypeClient
    Public metaSessionHeader As SessionHeader = New SessionHeader

    Public options As String = ""
    Public isWildcard As Boolean = False

    Public Sub Errorbox(ByVal msg As String)
        MessageBox.Show("Error", msg, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Module

Public Class Component
    Public Property component_type() As String
    Public Property name() As String
End Class
