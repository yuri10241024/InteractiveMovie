using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace Interactive_moive
{
    public class Quest
    {
        [JsonProperty("Path")]
        public string PathToVideoFolder { get; set; }

        [JsonProperty("Scenes")]
        public List<Scene> ListOfScenes { get; set; }

        [JsonIgnore]
        private int[] MaxXLvl;
        public Quest()
        {
            ListOfScenes = new List<Scene>();
        }
        static public Quest GetQuest(string fileName)
        {
            Quest quest = JsonConvert.DeserializeObject<Quest>(File.ReadAllText(fileName));
            return quest;
        }
        public void OrganizeScenesByLvl()
        {
            //1. Ищем корневую сцену
            //2. Находим все дочерние сцены
            //3. Для каждой дочерний сцены определяем номер
            //4. Для 2 и 3 п-ов будет происходить рекурсия) (вызывание функции самой себя)

            List<Scene> startScenes = GetStartScene();
            for(int i = 0; i < startScenes.Count; i++)
            {
                startScenes[i].Lvl = 0;
                SetID(startScenes[i]);
            }
        }
        private void SetX(Scene scene)
        {
            if (scene.IsFinalScene)
            {
                return;
            }
            for (int i = 0; i < scene.ListOfVariants.Count; i++)
            {
                Scene NextScene = ListOfScenes.Where(s => s.ID == scene.ListOfVariants[i].TargetID).FirstOrDefault();
                if (NextScene.IsDone)
                {
                    continue;
                }
            }
        }
        public void OrganizeScenesByX()
        {
            List<Scene> StartScenes = GetStartScene();
            for(int i = 0; i < StartScenes.Count; i++)
            {
                GetIzolateChildrenCount(StartScenes[i]);
            }
        }
        public void GetIzolateChildrenCount(Scene scene)
        {
            if (scene.IsFinalScene == false)
            {
                scene.ChildrenAmount = scene.IzolatedChildrenScenes.Count;
                for (int i = 0; i < scene.IzolatedChildrenScenes.Count; i++)
                {
                    GetIzolateChildrenCount(scene.IzolatedChildrenScenes[i]);
                    scene.ChildrenAmount += scene.IzolatedChildrenScenes[i].ChildrenAmount;
                }
            }
            else
            {
                scene.ChildrenAmount = 0;
                return;
            }
        }

        public void GetAllIzolatedChildrenCount()
        {
            List<Scene> scenes = GetStartScene();
            for(int i = 0; i < scenes.Count; i++)
            {
                GetIzolateChildrenCount(scenes[i]);
            }
        }
        
        public void FindIzolateChildren(Scene ParentScene)
        {
            if (ParentScene.IsFinalScene == false)
            {
                for (int i = 0; i < ParentScene.ListOfVariants.Count; i++)
                {
                    Scene scChild = ListOfScenes.Where(s => s.ID == ParentScene.ListOfVariants[i].TargetID).FirstOrDefault();
                    if (scChild != null && !scChild.IsDone)
                    {
                        ParentScene.IzolatedChildrenScenes.Add(scChild);
                        scChild.IsDone = true;
                    }
                    FindIzolateChildren(scChild);
                }
            }
        }
        public void FindAllIzolateChildren()
        {
            List<Scene> StartScenes = GetStartScene();
            for (int i = 0; i < StartScenes.Count; i++)
            {
                FindIzolateChildren(StartScenes[i]);
            }
        }
        private void SetID(Scene scene)
        {
            if (scene.IsFinalScene)
            {
                return;
            }
            for(int i = 0; i < scene.ListOfVariants.Count; i++)
            {
                Scene NextScene = ListOfScenes.Where(s => s.ID == scene.ListOfVariants[i].TargetID).FirstOrDefault();// ищем сцену на которую ведёт этот вариант
                if (NextScene.Lvl < scene.Lvl || NextScene.Lvl == 0)
                {
                    NextScene.Lvl = scene.Lvl + 1;
                }
                SetID(NextScene);//рекурсия
            }
        }
        private  List<Scene> GetStartScene()
        {
            List<Scene> scenes = new List<Scene>();
            scenes = ListOfScenes.Where(s => s.IsStartScene == true).ToList();
            return scenes;
        }
    }
}
/*
            for (int i = 0; i < ListOfScenes.Count; i++)
            {
                IDs.Add(ListOfScenes[i].ID);
            }
            for (int i = 0; i < ListOfScenes.Count; i++)
            {
                for (int j = 0; j < ListOfScenes[i].ListOfVariants.Count; j++)
                {
                    IDs.Remove(ListOfScenes[i].ListOfVariants[j].TargetID);//обращение через объекта варианта к номеру сцены к которму этот объект видёт, вкусно
                }
            }
            for (int i = 0; i < IDs.Count; i++)
            {
                Scene s = ListOfScenes.Where(sc => sc.ID == IDs[i]).FirstOrDefault();// пример linq запроса, like SQL принципы
                scenes.Add(s);
            }
            return scenes;*/
// string q = @"D:\_STUDIOS\VISUAL_STUDIO\Programming\Видео для программирования\Тест для ИФ123_1\Готовое\3.1.json"



//List<Scene> scenes = GetStartScene();
//int MaxLvl = -1;
//for (int i = 0; i < scenes.Count; i++)
//{
//    if (scenes[i].Lvl > MaxLvl)
//    {
//        MaxLvl = scenes[i].Lvl;
//    }
//}