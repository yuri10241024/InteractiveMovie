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
using System.IO;
using Interactive_moive;


namespace IncredibleVisualizer
{
    public partial class MainWindow : Window
    {
        List<SceneBlock> Blocks = new List<SceneBlock>();

        public int MaxX = 0;

        Quest quest;

        public MainWindow()
        {
            InitializeComponent();
            quest = Quest.GetQuest(@"D:\_STUDIOS\VISUAL_STUDIO\Programming\Видео для программирования\Тест для ИФ123_1\Готовое\Biggest project.json");

            quest.OrganizeScenesByLvl();
            quest.FindAllIzolateChildren();
            quest.OrganizeScenesByX();
            ShowQuest(quest);// Да будет шоу!!!
        }
        private void Vizualize()
        {



        }
        void ShowQuest(Quest quest)
        {
            List<Scene> scenes;
            int CurLvl = 0;
            while (true)
            {
                scenes = quest.ListOfScenes.Where(s => s.Lvl == CurLvl).ToList();// Преобразование в список(возвращает)
                if (scenes.Count == 0)
                {
                    break;
                }
                int left = 0;
                for (int i = 0; i < scenes.Count; i++)
                {
                    MakeBlock(scenes[i], left, CurLvl * 175);
                    left += 250;
                }
                CurLvl++;
            }
            MakeLines();
        }
        private void DrawVertiLine(SceneBlock sceneBlock1, SceneBlock sceneBlock2)
        {
            double Top1 = Canvas.GetTop(sceneBlock1);
            double Left1 = Canvas.GetLeft(sceneBlock1);
            double Top2 = Canvas.GetTop(sceneBlock2);
            double Left2 = Canvas.GetLeft(sceneBlock2);
            double y1 = Top1 + 120;
            double y2 = Top2;
            double x1 = Left1 + 120;
            double x2 = Left2 + 106;
            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            MainCanvas.Children.Add(line);  
            Panel.SetZIndex(line, -5);
        }
        private void MakeLines()// эта функци знает всё!
        {
            List<Scene> scenes = quest.ListOfScenes.Where(s => s.IsStartScene).ToList();
            for(int i = 0; i < scenes.Count; i++)
            {
                LinkBlocks(scenes[i]);
            }
        }

        private void LinkBlocks(Scene scene)
        {
            if (scene.IsFinalScene)
            {
                return;
            }
            SceneBlock sbParent = Blocks.Where(s => s.SceneID == scene.ID).FirstOrDefault();

            for (int i = 0; i < scene.ListOfVariants.Count; i++)
            {
                Scene NextScene = quest.ListOfScenes.Where(s => s.ID == scene.ListOfVariants[i].TargetID).FirstOrDefault();
                SceneBlock sbChild = Blocks.Where(s => s.SceneID == NextScene.ID).FirstOrDefault();
                DrawVertiLine(sbParent, sbChild);
                LinkBlocks(NextScene);
            }
        }

        private void MakeBlock(Scene scene, int left, int top)
        {
            SceneBlock sb = new SceneBlock();
            sb.SceneID = scene.ID;
            Blocks.Add(sb);
            sb.TBSceneDescription.Text = scene.Title;
            sb.TBSceneID.Text = scene.ID.ToString();
            if (scene.ListOfVariants.Count != 0)
            {
                sb.TBVar1.Text = scene.ListOfVariants[0].Description;
            }
            else
            {
                sb.TBVar1.Visibility = Visibility.Hidden;
                sb.TBVar2.Visibility = Visibility.Hidden;
                sb.TBVar3.Visibility = Visibility.Hidden;
            }
            if (scene.ListOfVariants.Count > 1)
            {
                sb.TBVar2.Text = scene.ListOfVariants[1].Description;
            }
            else
            {
                sb.TBVar2.Visibility = Visibility.Hidden;
                sb.TBVar3.Visibility = Visibility.Hidden;
            }
            if (scene.ListOfVariants.Count > 2)
            {
                sb.TBVar3.Text = scene.ListOfVariants[2].Description;
            }
            else
            {
                sb.TBVar3.Visibility = Visibility.Hidden;
            }
            MainCanvas.Children.Add(sb);
            Canvas.SetLeft(sb, left);
            Canvas.SetTop(sb, top);
        }

        private void ConnectBlocks(SceneBlock s1, SceneBlock s2)
        {
            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            line.Y1 = Canvas.GetTop(s1) + 120;
            line.X1 = Canvas.GetLeft(s1) + 106;
            line.Y2 = Canvas.GetTop(s2);
            line.X2 = Canvas.GetLeft(s2) + 106;
            MainCanvas.Children.Add(line);
            Panel.SetZIndex(line, -5);
        }
        //private void 
    }
}

//quest.ListOfScenes[0].
//GetStartScene();
//SceneBlock SB = new SceneBlock();
//MainCanvas.Children.Add(SB);
//Canvas.SetLeft(SB, 500);
//Canvas.SetTop(SB, 100);
//SB.MainGrid.Width = 600;
/*private void DrawHorizLine(SceneBlock sceneBlock1, SceneBlock sceneBlock2)
        {
            double Top1 = Canvas.GetTop(sceneBlock1);
            double Left1 = Canvas.GetLeft(sceneBlock1);
            double Top2 = Canvas.GetTop(sceneBlock2);
            double Left2 = Canvas.GetLeft(sceneBlock2);
            double y1 = Top1 + 60;
            double y2 = Top2 + 60;
            double x1 = Left1 + 212;
            double x2 = Left2;
            Line line = new Line();
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Black;
            line.X1 = x1;
            line.X2 = x2;
            line.Y1 = y1;
            line.Y2 = y2;
            MainCanvas.Children.Add(line);
            Panel.SetZIndex(line, -5);
        //}*/
