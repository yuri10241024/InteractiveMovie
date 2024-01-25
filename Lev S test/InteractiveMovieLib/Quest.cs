using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Interactive_moive
{
    public class Quest
    {
        [JsonProperty("Path")]
        public string PathToVideoFolder { get; set; }

        [JsonProperty("Scenes")]
        public List<Scene> ListOfScenes { get; set; }

        public Quest()
        {
            ListOfScenes = new List<Scene>();
        }
        // string q = @"D:\_STUDIOS\VISUAL_STUDIO\Programming\Видео для программирования\Тест для ИФ123_1\Готовое\3.1.json"
        static public Quest GetQuest(string fileName)
        {
            Quest quest = JsonConvert.DeserializeObject<Quest>(File.ReadAllText(fileName));
            return quest;
        }
    }
}
