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
                For Each key As String In Request.Form.Keys
                    parameters.Add(key.Trim(), Request.Form(key).Trim())
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

