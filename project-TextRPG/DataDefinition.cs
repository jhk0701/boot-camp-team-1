using System.Security.Cryptography;

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
    /// 장비 착용 시 적용 성능
    /// </summary>
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

    public struct ClassInitData
    {
        public string name;
        public float attack;
        public float defense;
        public float maxHealth;
        public float maxMana;

        public ClassInitData(string n, float atk, float def, float maxHp, float maxMp)
        {
            name = n;
            attack = atk;
            defense = def;
            maxHealth = maxHp;
            maxMana = maxMp;
        }
    }

    //public struct QuestRequestHunting

    public enum ERequestGoal
    {
        WinBattle,
        EnhanceItem,
        LevelUp,
    }

    public struct QuestReward
    {
        public int gold;
        public int exp;
        public int[] equips;
        public Dictionary<int, int> heals;

        public QuestReward(int g, int ex, int[] eq, Dictionary<int, int> hls)
        {
            gold = g; 
            exp = ex;
            equips = eq; 
            heals = hls;
        }
    }
   

    public class DataDefinition
    {
        static DataDefinition _instance;

        public Monster[] Monsters { get; set; }
        public Skill[] Skills { get; set; }

        public Skill[] UnionSkills { get; set; }  // 유니온 스킬테스트용 변수

        public Equipment[] Equipments { get; set; }
        public HealItem[] HealItems { get; set; }
        public ClassInitData[] ClassInitDatas { get; private set; }

        /// <summary>
        /// 퀘스트 정의 리스트. 레벨별 퀘스트
        /// </summary>
        public Quest[] QuestList { get; private set; }


        private DataDefinition()
        {
            ClassInitDatas = [
                new ClassInitData("노조 위원장", 10f, 1f, 50f, 50f),
                new ClassInitData("사무총장", 6f, 5f, 50f, 50f),
                new ClassInitData("조직 국장", 8f, 3f, 50f, 50f)
            ];

            Equipments = new Equipment[] 
            {
                new Equipment("정장", "[방어구] Manners, Maketh, Man.", 1000, EEquipType.Armor, ERank.Normal, 0f, 10f, 10f, 0f),
                new Equipment("서류 가방", "[무기] 사실은 총이 들어갑니다.", 2000, EEquipType.Weapon, ERank.Normal, 10f, 0f, 0f, 10f),
                //Weapon List
                new Equipment("회사 화장실 휴지", "[무기] [공격력 +10] 닦을 때 따가운 화장실 휴지, 엉덩이의 처우를 개선해달라.", 100, 0, ERank.Normal, 10f, 0, 0, 0),
                new Equipment("부러진 법인카드", "[무기] [공격력 +30] 부도 직전 회사의 한도초과 카드, 가려운 곳 긁기에는 쓸만하다.", 500, 0, ERank.Rare, 30f, 0, 0, 0),
                new Equipment("연대의 확성기", "[무기] [공격력 +70] 우리의 처절한 외침이 들리는가.", 2000, 0, ERank.Epic, 70f, 0, 0, 0),
                new Equipment("평화의 죽창", "[무기] [공격력 +100] 회사 앞에 심어놓은 대나무를 뽑아서 만들었다.", 5000, 0, ERank.Legendary, 100f, 0, 0, 0),
                new Equipment("사장님의 신용카드", "[무기] [공격력 +200] 사장님의 비자금이 숨겨진 카드. 돈쭐을 내줄 수 있다.", 10000, 0, ERank.Mythic, 200f, 0, 0, 0),

            };

            HealItems = new HealItem[]
            {
                //HealItem List
                new HealItem("믹스 커피", "아침 필수 도핑약 [HP + 50]", 10, 1, 50),
                new HealItem("야근의 핫식스", "야근을 버티게 하는 마법의  [HP + 100]", 30, 1, 100),
                new HealItem("박카스", "풀려라 5천만, 풀려라 피로! [HP + 300]", 100, 1, 300),
            };

            Monsters = new Monster[]
            {
                new Monster(0, "부당계약서", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("계약이행", 10f, 10f, 0, 10f)}),
                new Monster(1, "연장근무", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("업무의심연", 15f, 15f, 0, 10f)}),
                new Monster(2, "환영복지술사", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("덫없는환상", 12f, 12f, 0, 10f)}),
                new Monster(3, "월급루팡", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("훔치기", 5f, 5f, 0, 10f)}),
                new Monster(4, "인사고과망령", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("불공정평가", 8f, 8f, 0, 10f)}),
                new Monster(5, "노동착취자", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("착취", 20f, 20f, 0, 10f)}),
                new Monster(6, "과로골렘", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("압박", 25f, 25f, 0, 10f)}),
                new Monster(7, "해고의그림자", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("권고사직", 30f, 30f, 0, 10f)}),
                new Monster(8, "사장드래곤", 3f, 3f, 10f, 10f, 50, new Skill[]{ new Skill("최상위결정권", 35, 35f, 0, 10f)}),

            };


            //string name, float[] power, int requiredLv, float requiredMp)
            Skills = new Skill[]
            {
                new Skill("계약이행", 10f, 10f, 0, 10f),
                new Skill("업무의심연", 15f, 15f, 0, 10f),
                new Skill("덫없는환상", 12f, 12f, 0, 10f),
                new Skill("훔치기", 5f, 5f, 0, 10f),
                new Skill("불공정평가", 8f, 8f, 0, 10f),
                new Skill("착취", 20f, 20f, 0, 10f),
                new Skill("압박", 25f, 25f, 0, 10f),
                new Skill("권고사직", 30f, 30f, 0, 10f),
                new Skill("최상위결정권", 35, 35f, 0, 10f),
            };
            
            

            QuestList = [
                // 사냥 퀘스트
                new QuestModelHunting(0, 3, "부당 계약 적발", "사내에 완연한 부당계약서들을 적발하고 기강을 바로 세워주세요!", 
                    new QuestReward(100, 30, [], new Dictionary<int, int>{ })),
                new QuestModelHunting(1, 5, "과로의 원인", "비일비재한 연장근무에서 벗어나고 싶은 직장인들을 구해주세요.",
                    new QuestReward(500, 100, [2], new Dictionary<int, int>{ })),
                new QuestModelHunting(3, 5, "월급루팡", "일하는 손 따로 노는 손 따로.\n양심없는 루팡들을 처지해주세요!",
                    new QuestReward(2000, 150, [3], new Dictionary<int, int>{ })),

                // 달성 퀘스트
                new QuestModelGoal(ERequestGoal.WinBattle, 10, "근면성실", "사원의 미덕은 근면성실함이지!\n자네의 성실함을 보여주게나!",
                    new QuestReward(5000, 3000, [5], new Dictionary<int, int>{ })),
                new QuestModelGoal(ERequestGoal.EnhanceItem, 10, "장인도 도구를 가린다", "자네 이번달 실적이 너무 낮군.\n저기 가서 옷도 좀 수선하고 좋은 가방으로 리테일링이라도 하고 와.",
                    new QuestReward(5000, 3000, [5], new Dictionary<int, int>{ })),
            ];
        }

        public static DataDefinition GetInstance()
        {
            if (_instance == null)
                _instance = new DataDefinition();

            return _instance;
        }
    }

    public class InstanceManager
    {
        static InstanceManager _instance;
        long _instId = 0;

        private InstanceManager() { }

        public static InstanceManager GetInstacne()
        {
            if (_instance == null)
                _instance = new InstanceManager();

            return _instance;
        }

        public long GetId()
        {
            return ++_instId;
        }
    }
}
