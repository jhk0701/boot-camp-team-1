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

    public enum EEquipBonus : int
    {
        ATK = 0,    // Attack
        DEF,    // Defense
        HP,     // Hp
        MP      // MP
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

    // 아이템 타입 분류할 필요 없이 아이템이 배틀아이템 / 힐아이템으로 나뉘어져서 삭제합니다.
    //public enum CItemType : int
    //{
    //    Potion = 0,
    //    DamageItem = 1
    //}


    public class DataDefinition
    {
        long _instanceId = 1;
        static DataDefinition _instance;

        public Item[] Items { get; set; }
        public Monster[] Monsters { get; set; }
        public Skill[] Skills { get; set; }

        public Item[] Items { get; set; }
        public Equipment[] Equipments { get; set; }
<<<<<<< Updated upstream
        public BattleItem[] BattleItems { get; set; }

=======
        public ClassInitData[] ClassInitDatas { get; private set; }
>>>>>>> Stashed changes

        private DataDefinition()
        {

<<<<<<< Updated upstream
            BattleItems = new BattleItem[] {
            };
=======
>>>>>>> Stashed changes
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


<<<<<<< Updated upstream



=======
    //    public enum EEquipType : int
    //    {
    //        Weapon = 0,
    //        Armor,
    //        Helmet,
    //        Accessary
    //    }

    //    public enum EEquipBonus : int
    //    {
    //        ATK = 0,    // Attack
    //        DEF,    // Defense
    //        HP,     // Hp
    //        MP      // MP
    //    }

    //    public enum ERank : int
    //    {
    //        Normal = 0,
    //        Rare,
    //        Epic,
    //        Legendary,
    //        Mythic
    //    }

        // Equipment(string itemName, string description, int itemPrice, EEquipType eType, ERank rank, float atkBonus, float defBonus, float maxHpBonus, float maxMpBonus)
        // HealItem(string itemName, string description, int itemPrice, int itemCount, float healAmount)
        // DamageItem(string itemName, string description, int itemPrice, int itemCount, float itemDamage)
        Items = new Item[]
            {
                //Weapon List
                new Equipment("회사 화장실 휴지", "| 무기 | 공격력 +10 | 닦을 때 따가운 화장실 휴지, 채찍처럼 휘두룰 수 있다.", 100, 0, ERank.Normal, 10f, 0, 0, 0),
                new Equipment("부러진 법인카드", "| 무기 | 공격력 +30 | 부도 직전 회사의 한도초과 카드, 가려운 곳 긁기에는 쓸만하다.", 500, 0, ERank.Rare, 30f, 0, 0, 0),
                new Equipment("연대의 확성기", "| 무기 | 공격력 +70 | 우리의 처절한 외침이 들리는가.", 2000, 0, ERank.Epic, 70f, 0, 0, 0),
                new Equipment("평화의 죽창", "| 무기 | 공격력 +100 | 회사 앞에 심어놓은 대나무를 뽑아서 만들었다.", 5000, 0, ERank.Legendary, 100f, 0, 0, 0),
                new Equipment("사장님의 신용카드", "| 무기 | 공격력 +200 | 사장님의 비자금이 숨겨진 카드. 돈쭐을 내줄 수 있다.", 10000, 0, ERank.Mythic, 200f, 0, 0, 0),

                new Equipment("시위용 조끼", "[아머] [공격력 +10] 닦을 때 따가운 화장실 휴지, 채찍처럼 휘두룰 수 있다.", 100, 1, ERank.Normal, 10f, 0, 0, 0),
                new Equipment("낡은 정장", "[아머] [공격력 +30] 부도 직전 회사의 한도초과 카드, 가려운 곳 긁기에는 쓸만하다.", 500, 0, ERank.Rare, 30f, 0, 0, 0),
                new Equipment("퇴사자의 넥타이", "[아머] [공격력 +70] 우리의 처절한 외침이 들리는가.", 2000, 0, ERank.Epic, 70f, 0, 0, 0),
                new Equipment("갑옷", "[아머] [공격력 +100] 회사 앞에 심어놓은 대나무를 뽑아서 만들었다.", 5000, 0, ERank.Legendary, 100f, 0, 0, 0),
                new Equipment("사장님의 신용카드", "[아머] [공격력 +200] 사장님의 비자금이 숨겨진 카드. 돈쭐을 내줄 수 있다.", 10000, 0, ERank.Mythic, 200f, 0, 0, 0),
                

                //HealItem List
                new HealItem("믹스 커피", "| 포션 | 회복량 50 HP | 아침 필수 도핑약", 10, 1, 50),
                new HealItem("박카스", "| 포션 | 회복량 100 HP | 풀려라 5천만, 풀려라 피로!", 30, 1, 100),
                new HealItem("정관장 홍삼", "| 포션 | 회복량 300 HP | 한국인의 힘, 홍삼", 100, 1, 300)


                
            };
>>>>>>> Stashed changes

        }




        public static DataDefinition GetInstance()
        {
            if(_instance == null)
                _instance = new DataDefinition();

            return _instance;
        }

        public long GetInstanceId()
        {
            return _instanceId++;
        }
    }
}
