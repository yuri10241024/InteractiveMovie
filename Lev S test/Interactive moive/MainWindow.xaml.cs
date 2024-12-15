using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Interactive_moive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string PathToProject = @"D:\_STUDIOS\VISUAL_STUDIO\Programming\Видео для программирования\Тест для ИФ123_1\Готовое\TestFilm2.0.json";
        public MainWindow()
        {
            SerealiseScene();
            InitializeComponent();
            //MessageBox.Show(System.Windows.SystemParameters.PrimaryScreenWidth.ToString());
            //MessageBox.Show(System.Windows.SystemParameters.PrimaryScreenHeight.ToString());
        }
        private void AboutUSBTN_Click(object sender, RoutedEventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.ShowDialog();
        }
        //if smth starts, close than open.
        private void BTNnewGame_Click(object sender, RoutedEventArgs e)
        {
            GameWindow test1 = new GameWindow();
            test1.parent = this;
            test1.ShowDialog();
            MessageBox.Show("Спасибо за внимание");
        }

        private void BTNDiscription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Settings are coming soon");
        }
    }
}
