using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentControl.Model
{
    class WebCommunication
    {
        const string header_UA = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
        const string header_ConType = "application/x-www-form-urlencoded";

        public bool GetLogin()
        {
            while (true)
            {
                string browserUrl = "http://iubns.com/Capstone";

                HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(browserUrl);
                Hwr.Method = "POST";
                Hwr.UserAgent = header_UA;
                Hwr.ContentType = header_ConType;
                Hwr.SendChunked = false;
                Hwr.CookieContainer = new CookieContainer();

                Hwr.Timeout = 5000;
                try
                {
                    string result = (new StreamReader(((HttpWebResponse)Hwr.GetResponse()).GetResponseStream()).ReadToEnd());
                    Hwr.GetResponse().Close();
                    return bool.Parse(result);
                }
                catch
                {

                }
            }
        }
        
        public void SetLogin(bool value)
        {
            string browserUrl = "http://iubns.com/Capstone?value=";

            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(browserUrl + value.ToString());
            Hwr.Method = "POST";
            Hwr.UserAgent = header_UA;
            Hwr.ContentType = header_ConType;
            Hwr.SendChunked = false;
            Hwr.CookieContainer = new CookieContainer();

            Hwr.Timeout = 5000;
            string result = (new StreamReader(((HttpWebResponse)Hwr.GetResponse()).GetResponseStream()).ReadToEnd());
            Hwr.GetResponse().Close();
        }
    }
}
