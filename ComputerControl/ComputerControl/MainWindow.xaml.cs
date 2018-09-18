using CapstoneDesign;
using ComputerControl.Model;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ComputerControl
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;
            this.Closing += MainWindow_Closing;
            new Thread(Load).Start();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
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
       
        SocketObject server = new SocketObject();
        private void Load()
        {
            server.Send("student");
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
                        }));
                        break;

                }
            }
        }

        string[] programList = { /*"MapleStory,"*/ "KakaoTalk", "LeagueClient", "chrome", "Battle.net", "KartRider", "Hearthstone", "fifa4zf", "DNF", "SC2_x64", "suddenattack" };
        private void seachingGame()
        {
            while (true)
            {
                Thread.Sleep(1000);
                foreach (string gameName in programList)
                {
                    if (1 <= Process.GetProcessesByName(gameName).Length)
                    {
                        if (userName == "error")
                        {
                            server.Send("게임 감지\n");
                        }
                        else
                        {
                            server.Send($"{userName} 게임 감지\n");
                        }
                        break;
                    }
                }
            }
        }

        string userName = "";
        private void login(object sender, RoutedEventArgs e)
        {
            WebConnection web = new WebConnection();
            userName = web.GetUserName(inputID.Text,inputPW.Password);
            if(userName != "error")
            {
               new Thread(seachingGame).Start();
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
            foreach (string program in programList)
            {
                for (int i = 0; i < 20; i++)
                {
                    var info = Process.GetProcessesByName(program).FirstOrDefault();

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
