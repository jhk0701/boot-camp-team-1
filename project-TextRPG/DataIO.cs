using Newtonsoft.Json;
using System.IO;

namespace project_TextRPG
{
    public struct GameData
    {
        public Character Player { get; set; }
        public Dictionary<int, int> Quests { get; set; }
        //public QuestManager Quest { get; set; }

        public long ItemId { get; set; }
        //public InstanceManager Instance { get; set; }
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


        void Save()
        {
            string data = JsonConvert.SerializeObject(_gameData, Formatting.Indented);
            File.WriteAllText(PATH, data);
        }

        public void Save(Character p = null, QuestManager qm = null)
        {
            if(p != null)
                _gameData.Player = p;
            if(qm != null)
                _gameData.Quests = qm.Quests;

            _gameData.ItemId = InstanceManager.GetInstance().CurId;

            Save();
        }

        public bool Load()
        {
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

        public void DebugData()
        {
            Character t = _gameData.Player;
            Console.WriteLine(t.Name);
            Console.WriteLine(t.CharClass.ToString());
            Console.WriteLine($"Lv : {t.Level}");
            Console.WriteLine($"Exp : {t.Exp}");

            Console.WriteLine($"BasicAttack : {t.BasicAttack}");
            Console.WriteLine($"BasicDefense : {t.BasicDefense}");
            Console.WriteLine($"MaxHealth : {t.MaxHealth}");
            Console.WriteLine($"Health : {t.Health}");
            Console.WriteLine($"MaxMana : {t.MaxMana}");
            Console.WriteLine($"Mana : {t.Mana}");

            Console.WriteLine($"Skills : {t.Skills.Length}");
            for (int i = 0; i < t.Skills.Length; i++)
            {
                Console.WriteLine(t.Skills[i].Name);
            }

            Console.WriteLine($"Inventory.Items : {t.Inventory.Items.Length}");
            Console.WriteLine($"Inventory.Equipments : {t.Inventory.Equipments.Count}");
            
            Console.WriteLine($"Quests : {_gameData.Quests.Count}");
            int[] keys = _gameData.Quests.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                Console.WriteLine($"{keys[i]} : {_gameData.Quests[keys[i]]}");
            }

            Console.WriteLine($"Item Id : {_gameData.ItemId}");
        }
    }
}
