Option Explicit On
Option Strict Off

Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Web.Script.Serialization

Module RESTAPI

    Public Function getConnectionInfo() As ConnectionInfo
        Dim describeResult As ConnectionInfo = New ConnectionInfo()
        Dim jss = New JavaScriptSerializer()

        Dim serviceUrl As String = id & "?version=latest"
        Dim json As String = CallREST("GET", serviceUrl)

        Try
            describeResult = jss.Deserialize(Of ConnectionInfo)(json)
        Catch ex As Exception
            MsgBox(ex.Message, Title:="getConnectionInfo Deserialize Exception")
            'Throw New Exception("getConnectionInfo Deserialize Exception" & vbCrLf & ex.Message)
        End Try

        Return describeResult
    End Function

    Private Function CallREST(ByVal method As String, ByVal url As String, Optional body As String = "",
                              Optional headers As Dictionary(Of String, String) = Nothing) As String
        Dim jsonstring As String = ""
        Dim client As HttpClient = New HttpClient()
        Dim request As HttpRequestMessage = New HttpRequestMessage(New HttpMethod(method.ToUpper()), url)
        Dim response As HttpResponseMessage = New HttpResponseMessage()

        Try
            client.Timeout = New TimeSpan(0, 0, 120)
            request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)

            If headers IsNot Nothing Then
                If headers.Count > 0 Then
                    Dim keys As Dictionary(Of String, String).KeyCollection = headers.Keys
                    For Each key As String In keys
                        Dim value As String = headers(key)
                        request.Headers.Add(key, value)
                    Next
                End If
            End If

            If method = "POST" Or method = "PATCH" Then
                Dim buffer As Byte() = Encoding.UTF8.GetBytes(body)
                Dim contentBody = New ByteArrayContent(buffer)
                contentBody.Headers.ContentType = New MediaTypeHeaderValue("application/json")
                request.Content = contentBody
            End If
        Catch ex As Exception
            Throw New Exception("CallREST Preperation Exception!! " & vbCrLf & ex.Message)
        End Try

        Try
            response = client.SendAsync(request).Result()

            If response.IsSuccessStatusCode Then
                Try
                    jsonstring = response.Content.ReadAsStringAsync().Result()
                Catch ex As Exception
                    Throw New Exception("ReadAsStringAsync Exception!!" & vbCrLf & ex.Message)
                    'MsgBox(ex.Message, Title:="Handle CallREST Response Exception")
                End Try
            Else
                'MsgBox(response.StatusCode & " (" & response.ReasonPhrase & ")", Title:="CallREST Failed!")
                Throw New Exception("SendAsync Error!! " & vbCrLf & response.StatusCode & " (" & response.ReasonPhrase & ")")
            End If
        Catch ex As Exception
            'MsgBox(ex.Message, Title:="CallREST Exception")
            Throw New Exception("CallREST Exception!! " & vbCrLf & ex.Message)
        End Try

        Return jsonstring
    End Function

End Module


Public Class ConnectionInfo
    Public Property id() As String
    Public Property asserted_user() As Boolean
    Public Property user_id() As String
    Public Property organization_id() As String
    Public Property username() As String
    Public Property nick_name() As String
    Public Property display_name() As String
    Public Property email() As String
    Public Property email_verified() As Boolean
    Public Property first_name() As String
    Public Property last_name() As String
    Public Property addr_street() As String
    Public Property addr_city() As String
    Public Property addr_state() As String
    Public Property addr_country() As String
    Public Property addr_zip() As String
    Public Property mobile_phone() As String
    Public Property mobile_phone_verified() As Boolean
    Public Property is_lighting_login_user() As Boolean
    Public Property active() As Boolean
    Public Property user_type() As String
    Public Property timezone() As String
    Public Property language() As String
    Public Property locale() As String
    Public Property utcOffset() As String
    Public Property last_modified_date() As String
    Public Property is_app_installed() As Boolean
    Public Property status() As ConnectionStatus
    Public Property photos() As PhotoInfo
    Public Property urls() As UrlInfo
End Class

Public Class ConnectionStatus
    Public Property created_date() As String
    Public Property body() As String
End Class

Public Class PhotoInfo
    Public Property picture() As String
    Public Property thumbnail() As String

End Class

Public Class UrlInfo
    Public Property enterprise() As String
    Public Property metadata() As String
    Public Property partner() As String
    Public Property rest() As String
    Public Property sobjects() As String
    Public Property search() As String
    Public Property query() As String
    Public Property recent() As String
    Public Property profile() As String
    Public Property feeds() As String
    Public Property groups() As String
    Public Property users() As String
    Public Property feed_items() As String
    Public Property feed_elements() As String
    Public Property tooling_soap() As String
    Public Property tooling_rest() As String
    Public Property custom_domain() As String
End Class

