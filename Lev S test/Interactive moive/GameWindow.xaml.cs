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
using System.Windows.Shapes;
using Newtonsoft.Json;
using Interactive_moive;
using System.IO;

namespace Interactive_moive
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>  
    public partial class GameWindow : Window
    {
        public Window parent = null;//инфа о любом окне.

        Scene CurrentScene;
        public bool IsMain;
        Quest q  = new Quest();

        public GameWindow()
        {
            InitializeComponent();
            MainPlayer.MediaEnded += EndVideo;

            q = Quest.GetQuest(@"D:\_STUDIOS\VISUAL_STUDIO\Programming\Видео для программирования\Тест для ИФ123_1\Готовое\3.1.json");
            
            ShowScene(GetScene(0));

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (parent != null)
            {
                parent.Visibility = Visibility.Hidden;
            }
        }

        void ShowScene(Scene scene)
        {
            BTNSkip.Visibility = Visibility.Visible;

            IsMain = true;
            CurrentScene = scene;

            BSelected1.Visibility = Visibility.Collapsed;
            BSelected2.Visibility = Visibility.Collapsed;
            BSelected3.Visibility = Visibility.Collapsed;

            if (scene.ListOfVariants.Count > 0) 
            {
                TBSelect1.Text = scene.ListOfVariants[0].Description;
            }
            if(scene.ListOfVariants.Count > 1)
            {
                TBSelect2.Text = scene.ListOfVariants[1].Description;
            }
            else
            {
                TBSelect2.Text = "";
                TBSelect3.Text = "";
            }
            if (scene.ListOfVariants.Count > 2)
            {
                TBSelect3.Text = scene.ListOfVariants[2].Description;
            }
            else
            {
                TBSelect3.Text = "";
            }
                
            MainPlayer.Source = new Uri (scene.pathToVideo);
            MainPlayer.Play();
        }
        
        Scene GetScene(int Num)
        {
            Scene S = q.ListOfScenes.Where(s => s.countScene == Num).FirstOrDefault();//Что-то на страшном
            return S;
        }

        private void EndVideo(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(CurrentScene.IntermediateVideo))
            {
                Close();
                return;
            }

            BTNSkip.Visibility = Visibility.Collapsed;

            Uri U = new Uri(CurrentScene.IntermediateVideo);
            MainPlayer.Source = new Uri(CurrentScene.IntermediateVideo);
            MainPlayer.Play();

            if (!string.IsNullOrEmpty(TBSelect2.Text))
            {
                BSelected2.Visibility = Visibility.Visible;
            }
            if (!string.IsNullOrEmpty(TBSelect3.Text))
            {
                BSelected3.Visibility = Visibility.Visible;
            }

            BSelected1.Visibility = Visibility.Visible;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainPlayer.Stop();
            ShowScene(GetScene(CurrentScene.ListOfVariants[0].TargetID));
        }

        private void Border_MouseDown2(object sender, MouseButtonEventArgs e)
        {
            MainPlayer.Stop();
            ShowScene(GetScene(CurrentScene.ListOfVariants[1].TargetID));
        }

        private void Border_MouseDown3(object sender, MouseButtonEventArgs e)
        {
            MainPlayer.Stop();
            ShowScene(GetScene(CurrentScene.ListOfVariants[2].TargetID));
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            parent.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            BTNSkip.Opacity = 0.9;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            BTNSkip.Opacity = 0;
        }

        private void BTNSkip_Click(object sender, RoutedEventArgs e)
        {
            EndVideo(null, null);
        }

        private void BTNSkip_MouseEnter(object sender, MouseEventArgs e)
        {
            BTNSkip.Opacity = 0.9;
        }

        private void BTNSkip_MouseLeave(object sender, MouseEventArgs e)
        {
            BTNSkip.Opacity = 0;
        }
    }
}
//TODO: Починить 2-ую кнопку.