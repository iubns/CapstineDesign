using CapstoneDesign;
using ComputerControl.Model;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            WebConnection web = new WebConnection();
            web.GetUserName("201702040", "");
            Background = new SolidColorBrush(Colors.Black);
            Hide();
            Load();
        }
        
        SocketObject server = new SocketObject();
        private async void Load()
        {
            server.Send("student");
            while (true)
            {
                await Task.Delay(1);
                string temp = server.Receive();
                switch (temp)
                {
                    case "TurnOffGame":
                        KillGame();
                        break;
                    case "TurnOffScreen":
                        TurnOffScreen();
                        break;
                    case "TurnOnScreen":
                        TurnOnScreen();
                        break;
                    case "TurnOffComputer":
                        TrunOffSomputer();
                        break;
                }
            }
        }

        string[] programList = { /*"MapleStory", "KakaoTalk",*/"LeagueClient", "chrome", "Battle.net", "KartRider", "Hearthstone", "fifa4zf", "DNF", "SC2_x64", "suddenattack" };
        private async Task r()
        {
            await Task.Delay(1000);
            foreach(string gameName in programList)
                if (1 < Process.GetProcessesByName(gameName).Length)
                {
                    server.Send("게임 감지");
                }

        } 

        private void TrunOffSomputer()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        private void TurnOffScreen()
        {
            Show();
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
