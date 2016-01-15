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
                string paytmChecksum = "";
                foreach (string key in Request.Form.Keys)
                {
                    parameters.Add(key.Trim(), Request.Form[key].Trim());
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