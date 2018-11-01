using Microsoft.Win32;
using StudentControl.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace StudentControl
{
    public partial class MainWindow : Window
    {
        string version = "1.0.1";
        SocketObject socket;
        public MainWindow()
        {
            RemoveTemp();
            IPAddress ipAddress = IPAddress.Parse("121.65.76.85");
            IPEndPoint ipep = new IPEndPoint(ipAddress, 9001);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            while (!server.Connected)
            {
                Task.Delay(1000);
                try
                {
                    server.Connect(ipep);
                }
                catch
                {

                }
            }

            socket = new SocketObject(server);
            socket.Send(GetIpAdress());
            Task.Delay(500);
            socket.Send("professor");

            InitializeComponent();
            resistAutoStart();

            makeButton(GameOffButton, "GAME_OFF");
            makeButton(ScreenOffButton, "SCREEN_OFF");
            makeButton(ScreenOnButton, "SCREEN_ON");
            makeButton(PowerOffButton, "POWER_OFF");

            LoginButton.Content = (WebCommunication.GetLogin())? "Login OFF" : "Login ON";

            Task.Run(() => Recive());
            
            if (WebCommunication.GetVersion() != version)
            {
                WebCommunication.GetUpdate();
            }

            Closed += new EventHandler((o, e) => { Window_Closed(); });
            CheckVersion();
        }

        private void CheckVersion()
        {
            if (WebCommunication.GetVersion() != version)
            {
                WebCommunication.GetUpdate();
            }
            ver.Content = $"V {version}";
        }

        private void RemoveTemp()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
                File.Delete(fileInfo.Directory + @"\temp.exe");
            }
            catch
            {

            }
        }

        private void resistAutoStart()
        {
            try
            {
                RegistryKey reg = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Run");
                reg.SetValue("CompterControl", Process.GetCurrentProcess().MainModule.FileName);
                MessageBox.Show("자동 실행 등록 성공");
                return;
            }
            catch
            {

            }
        }

        public void SetLogin(object o, EventArgs e) {
            Button setLoginButton = (Button)o;
            if ((string)setLoginButton.Content == "Login OFF")
            {
                WebCommunication.SetLogin(false);
                setLoginButton.Content = "Login ON";
            }
            else
            {
                WebCommunication.SetLogin(true);
                setLoginButton.Content = "Login OFF";
            }
        }

        private void makeButton(Image image, string url)
        {
            image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString($"pack://application:,,/img/{url}.png");
            image.MouseDown += (sender, e) => {
                image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString($"pack://application:,,/img/{url}_CLICK.png");
                buttonClick(url);
            };
            image.MouseUp += (sender, e) => { image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString($"pack://application:,,/img/{url}.png"); };
        }

        private void Recive()
        {
            while (true)
            {
                string temp = socket.Receive();
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    log.Text += temp;
                }));
            }
        }

        private string GetIpAdress()
        {
            var ipArray = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            return ipArray[1].ToString();
        }

        private void buttonClick(string buttonUrl)
        {
            switch (buttonUrl)
            {
                case "GAME_OFF":
                    socket.Send("TurnOffGame");
                    break;
                case "POWER_OFF":
                    socket.Send("TurnOffComputer");
                    break;
                case "SCREEN_ON":
                    socket.Send("TurnOnScreen");
                    break;
                case "SCREEN_OFF":
                    socket.Send("TurnOffScreen");
                    break;
            }
        }
        
        private void Window_Closed()
        {
            var info = Process.GetCurrentProcess();

            if (info != null)
            {
                Console.WriteLine(info.MainModule.FileVersionInfo.ProductName);
                Console.WriteLine(info.MainModule.FileVersionInfo.FileDescription);
                info.Kill();
            }
        }
    }
}
