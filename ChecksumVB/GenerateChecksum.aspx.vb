Imports System
Imports paytm
Imports PaytmContant
Partial Public Class GenerateChecksum
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Request.Form.AllKeys.Length > 0 Then
            Try
                
                Dim parameters As New Dictionary(Of String, String)()
                Dim paytmChecksum As String = ""
                parameters.Add("MID", "xxxxxxx") 'Provided by Paytm
                parameters.Add("ORDER_ID", "ORDER000001") 'unique OrderId For every request
                parameters.Add("CUST_ID", "CUST00001") ' unique customer identifier 
                parameters.Add("CHANNEL_ID", "WAP") 'Provided by Paytm
                parameters.Add("INDUSTRY_TYPE_ID", "xxxxxxx") 'Provided by Paytm
                parameters.Add("WEBSITE", "xxxxxxx") 'Provided by Paytm
                parameters.Add("TXN_AMOUNT", "1.00") 'transaction amount
                parameters.Add("CALLBACK_URL", "https://xxxx.xx/paytmCallback.jsp") 'Provided by Paytm
                parameters.Add("EMAIL", "abc@gmail.com") 'customer email id
                parameters.Add("MOBILE_NO", "9999999999") 'customer 10 digit mobile no.

                For Each key As String In parameters.Keys
                    If parameters(key).Contains("|") Or parameters(key).ToUpper().Contains("REFUND") Then
                        parameters(key) = ""
                    End If
                Next

                paytmChecksum = CheckSum.generateCheckSum(PaytmConstants.MERCHANT_KEY, parameters)

                If parameters.ContainsKey("ORDER_ID") AndAlso parameters.ContainsKey("MID") AndAlso parameters("MID") = PaytmConstants.MID Then
                    Response.AddHeader("Content-type", "application/json")
                    Response.Write((Convert.ToString("{""ORDER_ID"":""" + parameters("ORDER_ID") + """,""CHECKSUMHASH"":""") & paytmChecksum) + """,""payt_STATUS"":""1""}")
                Else
                    Response.AddHeader("Content-type", "application/json")
                    Response.Write((Convert.ToString("{""ORDER_ID"":""" + parameters("ORDER_ID") + """,""CHECKSUMHASH"":""") & paytmChecksum) + """,""payt_STATUS"":""2""}")
                End If
            Catch generatedExceptionName As Exception


            End Try
        End If
    End Sub
End Class

