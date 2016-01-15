using System;
using System.Collections.Generic;
using paytm;
using PaytmContant;
using System.Web.Script.Serialization;
public partial class VerifyChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.AllKeys.Length > 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {                
                string paytmChecksum = "", responseString = "";

                foreach (string key in Request.Form.Keys)
                {
                    parameters.Add(key.Trim(), Request.Form[key].Trim());
                }


                if (parameters.ContainsKey("CHECKSUMHASH"))
                {
                    paytmChecksum = parameters["CHECKSUMHASH"];
                    parameters.Remove("CHECKSUMHASH");
                }

                if (CheckSum.verifyCheckSum(PaytmConstants.MERCHANT_KEY, parameters, paytmChecksum))
                {
                    parameters.Add("IS_CHECKSUM_VALID", "Y");
                }
                else
                {
                    parameters.Add("IS_CHECKSUM_VALID", "N");
                }

                
            }
            catch (Exception ex)
            {
                parameters.Add("IS_CHECKSUM_VALID", "N");
                
                
            }
						Response.AddHeader("Content-type", "text/html");
						string outputHTML="<html>";
outputHTML +="<head>";
							outputHTML +="<meta http-equiv='Content-Type' content='text/html;charset=ISO-8859-I'>";
							outputHTML +="<title>Paytm</title>";
							outputHTML +="<script type='text/javascript'>";
							outputHTML +="function response(){";
							outputHTML +="return document.getElementById('response').value;";
							outputHTML +="}";
							outputHTML +="</script>";
							outputHTML +="</head>";
							outputHTML +="<body>";
							outputHTML +="Redirect back to the app<br>";
outputHTML +="<form name='frm' method='post'>";
							outputHTML +="<input type='hidden' id='response' name='responseField' value='" + new JavaScriptSerializer().Serialize(parameters) +"'>";
							outputHTML +="</form>";
							outputHTML +="</body>";
							outputHTML +="</html>";
							
							Response.Write(outputHTML);
        }
    }
}