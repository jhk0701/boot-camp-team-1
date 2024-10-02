using Newtonsoft.Json;
using System.IO;

namespace project_TextRPG
{
    public struct GameData
    {
        public Character Player { get; set; }
        public QuestManager Quest { get; set; }
    }

    public class DataIO
    {
        static DataIO _instance;

        const string PATH = "./SaveData.dat";
        
        GameData _gameData;

        private DataIO() 
        {
            _gameData = new GameData();
        }

        public static DataIO GetInstance()
        {
            if(_instance == null)
                _instance = new DataIO();

            return _instance;
        }


        public void Save()
        {
            string data = JsonConvert.SerializeObject(_gameData, Formatting.Indented);
            File.WriteAllText(PATH, data);
        }

        public void Save(Character p = null, QuestManager qm = null)
        {
            if(p != null)
                _gameData.Player = p;
            if(qm != null)
                _gameData.Quest = qm;

            Save();
        }

        public bool Load()
        {
            return false;

            if(!File.Exists(PATH))
                return false;

            string data = File.ReadAllText(PATH);
            _gameData = JsonConvert.DeserializeObject<GameData>(data);

            return true;
        }

        public GameData GetLoadedData()
        {
            return _gameData;
        }
    }
}
