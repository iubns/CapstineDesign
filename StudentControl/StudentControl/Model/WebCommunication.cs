using System.Diagnostics;
using System.IO;
using System.Net;

namespace StudentControl.Model
{
    static class WebCommunication
    {
        const string header_UA = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
        const string header_ConType = "application/x-www-form-urlencoded";

        public static bool GetLogin()
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
        
        public static void SetLogin(bool value)
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

        public static void GetUpdate()
        {
            string browserUrl = @"http://iubns.com/Capstone/pro/StudentControl.exe";

            FileInfo fileInfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            File.Move(Process.GetCurrentProcess().MainModule.FileName, fileInfo.Directory + @"\temp.exe");

            WebClient webClient = new WebClient();
            webClient.DownloadFile(browserUrl, Process.GetCurrentProcess().MainModule.FileName);
        }

        public static string GetVersion()
        {
            while (true)
            {
                string browserUrl = "http://iubns.com/Capstone/pro/version";

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
                    return result;
                }
                catch
                {

                }
            }
        }
    }
}
