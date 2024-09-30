using project_TextRPG;
using System.Xml.Linq;

namespace project_TextRPG
{
    /// <summary>
    /// 플레이어 캐릭터 직업 타입
    /// </summary>
    public enum EClass : int
    {
        /// <summary>
        /// 노조 위원장 : 전사
        /// </summary>
        ChairmanOfUnion = 0,
        /// <summary>
        /// 사무 국장(총장) : 마법사
        /// </summary>
        SecretaryGeneral,
        /// <summary>
        /// 조직 국장 : 도적
        /// </summary>
        DirectorOfUnion
    }

    /// <summary>
    /// 착용 장비의 종류
    /// </summary>
    public enum EEquipType : int
    {
        Weapon = 0,
        Armor,
        Helmet,
        Accessary
    }

    /// <summary>
    /// 등급 체계
    /// </summary>
    public enum ERank : int 
    {
        Normal = 0,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public enum BattleItemType : int
    {
        Potion = 0,
        DamageItem = 1
    }



    public class DataDefinition
    {

        static DataDefinition _instance;

        public Item[] Items { get; set; }
        public Monster[] Monsters { get; set; }
        public Skill[] Skills { get; set; }

        private DataDefinition()
        {
            Items = new Item[] {
                //new Item("Item 1", 10f),
                //new Item("Item 2", 10f),
                //new Item("Item 3", 10f),
                //new Item("Item 4", 10f),
                //new Item("Item 5", 10f),
            };
            //string name, float basicattack, float basicdefence, float maxhealth, float maxmana, int gold, Skill[] skills) : base(name)
            Monsters = new Monster[]
            {
                new Monster("부당계약서", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("계약이행", new float[]{10f}, 0, 10f)}),
                new Monster("연장근무", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("업무의심연", new float[]{15f}, 0, 10f)}),
                new Monster("환영복지술사", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("덫없는환상", new float[]{12f}, 0, 10f)}),
                new Monster("월급루팡", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("훔치기", new float[]{5f}, 0, 10f)}),
                new Monster("인사고과망령", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("불공정평가", new float[]{8f}, 0, 10f)}),
                new Monster("노동착취자", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("착취", new float[]{20f}, 0, 10f)}),
                new Monster("과로골렘", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("압박", new float[]{25f}, 0, 10f)}),
                new Monster("해고의그림자", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("권고사직", new float[]{30f}, 0, 10f)}),
                new Monster("사장드래곤", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("최상위결정권", new float[]{35f}, 0, 10f)}),

            };



            //string name, float[] power, int requiredLv, float requiredMp)
            Skills = new Skill[] 
            {
                new Skill("계약이행", new float[]{10f}, 0, 10f),
                new Skill("업무의심연", new float[]{15f}, 0, 10f),
                new Skill("덫없는환상", new float[]{12f}, 0, 10f),
                new Skill("훔치기", new float[]{5f}, 0, 10f),
                new Skill("불공정평가", new float[]{8f}, 0, 10f),
                new Skill("착취", new float[]{20f}, 0, 10f),
                new Skill("압박", new float[]{25f}, 0, 10f),
                new Skill("권고사직", new float[]{30f}, 0, 10f),
                new Skill("최상위결정권", new float[]{35f}, 0, 10f),
            };






        }


        

        public static DataDefinition GetInstance()
        {
            if(_instance == null)
                _instance = new DataDefinition();

            return _instance;
        }

    }
}
