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
                string result = GerResult(browserUrl);
                if (result != string.Empty)
                {
                    return bool.Parse(GerResult(browserUrl));
                }
            }
        }
        
        public static void SetLogin(bool value)
        {
            string browserUrl = "http://iubns.com/Capstone?value=";
            string result = GerResult(browserUrl + value.ToString());
            if (result != string.Empty)
            {
                GerResult(browserUrl);
            }
        }

        public static void GetUpdate()
        {
            string browserUrl = @"http://iubns.com/Capstone/pro/StudentControl.exe";

            FileInfo fileInfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            File.Move(Process.GetCurrentProcess().MainModule.FileName, fileInfo.Directory + @"\temp.exe");

            WebClient webClient = new WebClient();
            webClient.DownloadFile(browserUrl, Process.GetCurrentProcess().MainModule.FileName);
        }

        private static string GerResult(string url)
        {
            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(url);
            Hwr.Method = "POST";
            Hwr.UserAgent = header_UA;
            Hwr.ContentType = header_ConType;
            Hwr.Timeout = 5000;

            try
            {
                using (StreamReader streamReader = new StreamReader(Hwr.GetResponse().GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetVersion()
        {
            while (true)
            {
                string browserUrl = "http://iubns.com/Capstone/pro/version";
                string result = GerResult(browserUrl);
                if (result != string.Empty)
                {
                    return GerResult(browserUrl);
                }
            }
        }
    }
}
