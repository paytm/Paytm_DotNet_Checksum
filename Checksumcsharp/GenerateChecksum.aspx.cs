using System;
using System.Collections.Generic;
using paytm;
using PaytmContant;
public partial class GenerateChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.AllKeys.Length > 0)
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("MID", "");
                parameters.Add("ORDER_ID", "");
                parameters.Add("CUST_ID", "");
                parameters.Add("CHANNEL_ID", "");
                parameters.Add("INDUSTRY_TYPE_ID", "");
                parameters.Add("WEBSITE", "");
                parameters.Add("TXN_AMOUNT", "");
                
                string paytmChecksum = "";
                foreach (string key in Request.Form.Keys)
                {
                    // below code snippet is mandatory, so that no one can use your checksumgeneration url for other purpose .
                    if (Request.Form[key].Trim().ToUpper().Contains("REFUND"))
                    {
                        continue;
                    }

                    if ( parameters.ContainsKey(key.Trim()) ) {
                        parameters[key.Trim()] = Request.Form[key].Trim();
                    }else{
                        parameters.Add(key.Trim(), Request.Form[key].Trim());
                    }
                }
                
                paytmChecksum = CheckSum.generateCheckSum(PaytmConstants.MERCHANT_KEY, parameters);

                if (parameters.ContainsKey("ORDER_ID") && parameters.ContainsKey("MID"))
                {
                    Response.AddHeader("Content-type", "application/json");
                    Response.Write("{\"ORDER_ID\":\"" + parameters["ORDER_ID"] + "\",\"CHECKSUMHASH\":\""+paytmChecksum+"\",\"payt_STATUS\":\"1\"}");
                }
                else
                {
                    Response.AddHeader("Content-type", "application/json");
                    Response.Write("{\"ORDER_ID\":\"" + parameters["ORDER_ID"] + "\",\"CHECKSUMHASH\":\""+paytmChecksum+"\",\"payt_STATUS\":\"2\"}");
                }
            }
            catch (Exception)
            {
            }


        }
    }
}
