using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace ComputerControl.Model
{
    static class WebConnection
    {
        const string header_UA = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";
        const string header_ConType = "application/x-www-form-urlencoded";

        public static string GetUserName(string id, string pw)
        {
            string browserUrl = "http://tis.tw.ac.kr/sys.Login.servj";
            string reURl = "http://tis.tw.ac.kr/sys.Menu.doj";

            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(browserUrl);
            Hwr.Method = "POST";
            Hwr.UserAgent = header_UA;
            Hwr.ContentType = header_ConType;
            Hwr.SendChunked = false;
            Hwr.CookieContainer = new CookieContainer();

            Hwr.Timeout = 5000;
            Stream str = Hwr.GetRequestStream();
            StreamWriter stwr = new StreamWriter(str);
            stwr.Write("strLoginId=" + id + "&strLoginPw=" + pw + "&strCommand=LOGIN&strTarget=MAIN", Encoding.Default);
            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();
            string result = (new StreamReader(((HttpWebResponse)Hwr.GetResponse()).GetResponseStream()).ReadToEnd());
            CookieContainer container = Hwr.CookieContainer;
            Hwr.GetResponse().Close();

            Hwr = (HttpWebRequest)WebRequest.Create(reURl);
            Hwr.Method = "POST";
            Hwr.UserAgent = header_UA;
            Hwr.ContentType = header_ConType;
            Hwr.SendChunked = false;
            Hwr.CookieContainer = container;

            Hwr.Timeout = 5000;
            str = Hwr.GetRequestStream();
            stwr = new StreamWriter(str);
            stwr.Flush(); stwr.Close(); stwr.Dispose();
            str.Flush(); str.Close(); str.Dispose();
            Encoding euckr = Encoding.GetEncoding(51949);
            result = (new StreamReader(((HttpWebResponse)Hwr.GetResponse()).GetResponseStream(), euckr).ReadToEnd());
            try
            {
                result = result.Split(new string[] { "font-size: 9pt;padding-left: 5px;height: 23px;color: #FFFFFF;\">" }, StringSplitOptions.None)[1];
                result = result.Split(new string[] { "</b>" }, StringSplitOptions.None)[0];
                result = result.Split('>')[1];
                result = result.Split('&')[0];
            }
            catch
            {
                result = "Error";
            }
            return result;
        }

        public static void GetUpdate()
        {
            string browserUrl = @"http://iubns.com/Capstone/ComputerControl.exe";

            FileInfo fileInfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            File.Move(Process.GetCurrentProcess().MainModule.FileName, fileInfo.Directory+@"\temp.exe");

            WebClient webClient = new WebClient();
            webClient.DownloadFile(browserUrl, Process.GetCurrentProcess().MainModule.FileName);
        }

        public static bool GetLogin()
        {
            while (true)
            {
                string browserUrl = "http://iubns.com/Capstone";
                string result = GetResult(browserUrl);
                if (result != string.Empty)
                {
                    return bool.Parse(GetResult(browserUrl));
                }
            }
        }

        private static string GetResult(string url)
        {
            HttpWebRequest Hwr = (HttpWebRequest)WebRequest.Create(url);
            Hwr.Method = "POST";
            Hwr.UserAgent = header_UA;
            Hwr.ContentType = header_ConType;
            Hwr.SendChunked = false;
            Hwr.CookieContainer = new CookieContainer();

            Hwr.Timeout = 5000;
            using (StreamReader streamReader = new StreamReader(Hwr.GetResponse().GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static string GetVersion()
        {
            while (true)
            {
                string browserUrl = "http://iubns.com/Capstone/version";
                string result = GetResult(browserUrl);
                if(result != string.Empty)
                {
                    return result;
                }
            }
        }
    }
}
