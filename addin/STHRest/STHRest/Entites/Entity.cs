using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace STHRest.Entites
{

    public class Entity
    {
         public string GetData(string url){
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }


         public string PostData(string url , Dictionary<String, String> data)
         {
             using (var client = new WebClient())
             {
                 var values = new NameValueCollection();

                 foreach (var param in data)
                 {
                     values.Add(param.Key, param.Value);
                 }


                 var response = client.UploadValues(url, values);

                 var responseString = Encoding.Default.GetString(response);
                 return responseString;
             }
         }

    }
}
