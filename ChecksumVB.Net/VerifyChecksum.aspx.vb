Imports System
Imports paytm
Imports PaytmContant
Imports System.Web.Script.Serialization
Partial Public Class VerifyChecksum
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Request.Form.AllKeys.Length > 0 Then
            Dim parameters As New Dictionary(Of String, String)()
            Try
                Dim paytmChecksum As String = "", responseString As String = ""

                For Each key As String In Request.Form.Keys
                    parameters.Add(key.Trim(), Request.Form(key).Trim())
                Next


                If parameters.ContainsKey("CHECKSUMHASH") Then
                    paytmChecksum = parameters("CHECKSUMHASH")
                    parameters.Remove("CHECKSUMHASH")
                End If

                If CheckSum.verifyCheckSum(PaytmConstants.MERCHANT_KEY, parameters, paytmChecksum) Then
                    parameters.Add("IS_CHECKSUM_VALID", "Y")
                Else
                    parameters.Add("IS_CHECKSUM_VALID", "N")
                End If
           Catch ex As Exception
                parameters.Add("IS_CHECKSUM_VALID", "N")
             
								
								                
            End Try
					   Response.AddHeader("Content-type", "text/html")	
          Dim outputHTML As String = "<html>"
          outputHTML += "<head>"
          outputHTML += "<meta http-equiv='Content-Type' content='text/html;charset=ISO-8859-I'>"
          outputHTML += "<title>Paytm</title>"
          outputHTML += "<script type='text/javascript'>"
          outputHTML += "function response(){"
          outputHTML += "return document.getElementById('response').value;"
          outputHTML += "}"
          outputHTML += "</script>"
          outputHTML += "</head>"
          outputHTML += "<body>"
          outputHTML += "Redirect back to the app<br>"
          outputHTML += "<form name='frm' method='post'>"
          outputHTML += "<input type='hidden' id='response' name='responseField' value='" + New JavaScriptSerializer().Serialize(parameters) + "'>"
          outputHTML += "</form>"
          outputHTML += "</body>"
          outputHTML += "</html>"
          
          Response.Write(outputHTML)


        End If
    End Sub
End Class
