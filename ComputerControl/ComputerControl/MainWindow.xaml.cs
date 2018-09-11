using CapstoneDesign;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Hide();
            Load();
        }

        private static async void Load()
        {
            SocketObject server = new SocketObject();
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
                        break;
                    case "TurnOffComputer":
                        TrunOffSomputer();
                        break;
                }
            }
        }

        private static void TrunOffSomputer()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        private static void FillScreen()
        {

        }

        private static void KillGame()
        {
            string[] programList = { /*"MapleStory", "KakaoTalk",*/"LeagueClient" , "chrome" };
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
