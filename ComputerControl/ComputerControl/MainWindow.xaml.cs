using CapstoneDesign;
using ComputerControl.Model;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ComputerControl
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RemoveTemp();
            InitializeComponent();

            resistAutoStart();
            
            Topmost = true;
            KeyDown += MainWindow_KeyDown;
            Closing += MainWindow_Closing;
            
            server = new SocketObject();
            if(! WebConnection.GetLogin())
            {
                TurnOnScreen();
                Task.Run(() => Load());
            }
            CheckVersion();
        }

        private void CheckVersion()
        {
            string version = "1.0.2";
            if (WebConnection.GetVersion() != version)
            {
                WebConnection.GetUpdate();
            }
            versionLabel.Content = $"V {version}";
        }

        private void RemoveTemp()
        {
            try {
                FileInfo fileInfo = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
                File.Delete(fileInfo.Directory + @"\temp.exe");
            }
            catch
            {

            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
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

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt))
                  && Keyboard.IsKeyDown(Key.F4)) { 
            }
            if(Keyboard.IsKeyDown(Key.LWin) || Keyboard.IsKeyDown(Key.RWin))
            {
                e.Handled = true;
            }
        }

        private string GetIpAdress()
        {
            var ipArray = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            return ipArray[1].ToString();
        }

        SocketObject server = null;
        private void Load()
        {
            server.Send(GetIpAdress()); //자신의 ip 보내기, 서버쪽 처리 필요
            Task.Delay(500);
            server.Send("student");
            Task.Run(() => seachingGame());
            while (true)
            {
                string temp = server.Receive();
                switch (temp)
                {
                    case "TurnOffGame":
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            KillGame();
                        }));
                        break;
                    case "TurnOffScreen":
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            TurnOffScreen();
                        }));
                        break;
                    case "TurnOnScreen":
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            TurnOnScreen();
                        }));
                        break;
                    case "TurnOffComputer":
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            TrunOffSomputer();
                        }));
                        break;
                    case "TurnOffLogin":
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            TurnOnScreen();
                            userName = "Error";
                        }));
                        break;
                }
            }
        }
        
        private async void seachingGame()
        {
            if(server.Receive() == "startGameSearching")
            while (true)
            {
                foreach (Game game in Game.GetGames())
                {
                    foreach (string processName in game.gameId)
                    {
                        if (1 <= Process.GetProcessesByName(processName).Length)
                        {
                            if (userName == "Error")
                            {
                                server.Send($"게임 감지 : {game.gameNameKr} \n");
                            }
                            else
                            {
                                server.Send($"{userName} 게임 감지 : {game.gameNameKr} \n");
                            }
                            break;
                        }
                    }
                }
                await Task.Delay(1000 * 60 * 5);
            }
        }

        string userName = "Error";
        private void login(object sender, RoutedEventArgs e)
        {
            userName = WebConnection.GetUserName(inputID.Text,inputPW.Password);
            if(userName != "Error")
            {
                Task.Run(() => Load());
                TurnOnScreen();
            }
        }
        
        private void TrunOffSomputer()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        private void TurnOffScreen()
        {
            Show();
            BasicLayout.Children.Clear();
            BasicLayout.Background = new SolidColorBrush(Colors.Black);
            Topmost = true;
        }

        private void TurnOnScreen()
        {
            Hide();
            Topmost = false;
        }

        private void KillGame()
        {
            foreach (Game game in Game.GetGames())
            {
                for (int i = 0; i < 20; i++)
                {
                    foreach (string processName in game.gameId)
                    {
                        var info = Process.GetProcessesByName(processName).FirstOrDefault();

                        if (info != null)
                        {
                            try
                            {
                                info.Kill();
                                info.Close();
                                info.Dispose();
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
        }
    }
}
