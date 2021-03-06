using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo.SDK3._1
{
    /// <summary>
    /// 3.1 tested everything fine  
    /// </summary>
    class Program
    {
        static  void Main(string[] args)
        {
            var client = new HttpClient();
            var resp = client.GetAsync("https://sfapi-sbox.sf-express.com/std/service").Result;
            //var resp = client.GetAsync("https://demo.identityserver.io/.well-known/openid-configuration").Result;
            Console.WriteLine(resp.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
        }
        private static string callSfExpressServiceByCSIM(string reqURL, string requestID)
        {
            String result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(reqURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            Dictionary<string, string> content = new Dictionary<string, string>();
            content["requestID"] = requestID;
            if (!(content == null || content.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in content.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, content[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, content[key]);
                    }
                    i++;
                }

                byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
