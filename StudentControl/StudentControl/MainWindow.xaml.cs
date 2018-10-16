using Microsoft.Win32;
using StudentControl.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace StudentControl
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        SocketObject socket;
        public MainWindow()
        {
            InitializeComponent();
            resistAutoStart();
            IPAddress ipAddress = IPAddress.Parse("121.65.76.85");
            IPEndPoint ipep = new IPEndPoint(ipAddress, 9001);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipep);

            socket = new SocketObject(server);
            socket.Send("pro");

            makeButton(GameOffButton, "GAME_OFF.png");
            makeButton(ScreenOffButton, "SCREEN_OFF.png");
            makeButton(ScreenOnButton, "SCREEN_ON.png");
            makeButton(PowerOffButton, "POWER_OFF.png");

            LoginButton.Content = (WebCommunication.GetLogin())? "Login OFF" : "Login ON";
            Thread thread = new Thread(Recive);
            thread.Start();

            if (WebCommunication.GetVersion() != "1.0.0")
            {
                WebCommunication.GetUpdate();
            }

            Closed += new EventHandler((o, e) => { Window_Closed(); });
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

        private void makeButton(Button button, string url)
        {
            button.Background = new ImageBrush() { ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("pack://application:,,/img/" + url) };
            button.BorderThickness = new Thickness(0.0);
            button.BorderBrush = new SolidColorBrush(Colors.Transparent);
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

        public void GameOff(object o, EventArgs e)
        {
            socket.Send("TurnOffGame");
        }

        public void ScreenOff(object o, EventArgs e)
        {
            socket.Send("TurnOffScreen");
        }

        public void PowerOff(object o, EventArgs e)
        {
            socket.Send("TurnOffComputer");
        }

        public void ScreenOn(object o, EventArgs e)
        {
            socket.Send("TurnOnScreen");
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

        private void GameOffButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void GameOffButton_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void GameOffButton_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }

    internal class MouseEnter
    {
    }
}
