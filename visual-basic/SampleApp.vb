Module SampleApp

    Sub Main()
        Try
            'initialize an array 
            Dim paytmParams As New Dictionary(Of String, String)

            'add parameters in Array
            paytmParams.Add("MID", "YOUR_MID_HERE")
            paytmParams.Add("ORDER_ID", "YOUR_ORDER_ID_HERE")

            'Generate checksum by parameters we have
            'Find your Merchant Key in your Paytm Dashboard at https: //dashboard.paytm.com/next/apikeys 

            Dim paytmChecksum As String = Paytm.Checksum.generateSignature(paytmParams, "YOUR_MERCHANT_KEY")
            Dim verifySignature As Boolean = Paytm.Checksum.verifySignature(paytmParams, "YOUR_MERCHANT_KEY", paytmChecksum)

            Console.WriteLine("generateSignature Returns: " & paytmChecksum)
            Console.WriteLine("verifySignature Returns: " & verifySignature)

            ' initialize JSON String 
            Dim body As String = "{\'mid\':\'YOUR_MID_HERE\',\'orderId\':\'YOUR_ORDER_ID_HERE\'}"


            'Generate checksum by parameters we have in body
            'Find your Merchant Key in your Paytm Dashboard at https: //dashboard.paytm.com/next/apikeys 

            paytmChecksum = Paytm.Checksum.generateSignature(body, "YOUR_MERCHANT_KEY")
            verifySignature = Paytm.Checksum.verifySignature(body, "YOUR_MERCHANT_KEY", paytmChecksum)

            Console.WriteLine("generateSignature Returns: " & paytmChecksum)
            Console.WriteLine("verifySignature Returns: " & verifySignature)
            Console.Read()
        Catch ex As Exception
            Console.WriteLine("Exception:" & ex.ToString())

        End Try

    End Sub

End Module
