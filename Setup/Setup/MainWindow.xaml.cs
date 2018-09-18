using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Setup
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string temp = Process.GetCurrentProcess().MainModule.FileName;
            try
            {
                string[] tempsr = temp.Split('\\');
                temp = "";
                foreach (string a in tempsr)
                {
                    if (a != "Setup.exe")
                    {
                        temp += a+"\\";
                    }
                }
                temp+= "ComputerControl.exe";
                
                RegistryKey reg = Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("Microsoft").CreateSubKey("Windows").CreateSubKey("CurrentVersion").CreateSubKey("Run");
                reg.SetValue("ComputerControl", temp);
                label.Content = "자동실행 등록 성공";
                Close();
                return;
            }
            catch
            {
                label.Content = "자동실행 등록 실패";
            }
        }
    }
}
