using CapstoneDesign;
using ComputerControl.Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
            //로그인 완료시

            Thread thread = new Thread(Load);
            thread.Start();
            thread = new Thread(r);
            thread.Start();
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
                }
            }
        }

        string[] programList = { /*"MapleStory", "KakaoTalk",*/"LeagueClient", "chrome", "Battle.net", "KartRider", "Hearthstone", "fifa4zf", "DNF", "SC2_x64", "suddenattack" };
        private void r()
        {
            while (true)
            {
                Thread.Sleep(1000);
                foreach (string gameName in programList)
                {
                    if (1 <= Process.GetProcessesByName(gameName).Length)
                    {
                        server.Send("게임 감지\n");
                        break;
                    }
                }
            }
        }

        private void TrunOffSomputer()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        private void TurnOffScreen()
        {
            Show();
            Background = new SolidColorBrush(Colors.Black);
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
