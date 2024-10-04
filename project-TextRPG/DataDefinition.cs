using Newtonsoft.Json;
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

    public struct ClassInitData
    {
        public string name;
        public string description;

        public float attack;
        public float defense;
        public float maxHealth;
        public float maxMana;

        public Skill[] skills;

        public ClassInitData(string n, float atk, float def, float maxHp, float maxMp, string desc ="", Skill[] sk = null)
        {
            name = n;
            attack = atk;
            defense = def;
            maxHealth = maxHp;
            maxMana = maxMp;

            description = desc;
            skills = sk;
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

        public BattleItem[] BattleItems { get; set; }

        public ClassInitData[] ClassInitDatas { get; set; }

        /// <summary>
        /// 퀘스트 정의 리스트. 레벨별 퀘스트
        /// </summary>
        public Quest[] QuestList { get; set; }


        private DataDefinition()
        {
            ClassInitDatas = [
                new ClassInitData("노조 위원장", 10f, 1f, 70f, 30f,
                    "\"투쟁의 길은 언제나 피투성이지.\"", 
                    [
                        new Skill("파업","공격력의 120 ~ 150%의 데미지를 준다", 120f, 150f, 5, 10f),
                        new Skill("단식 투쟁","공격력의 250%의 데미지를 준다.", 250f, 250f, 10, 30f),
                        new Skill("트럭 시위","가챠에 사용한 횟수만큼 데미지를 준다.", 300f, 500f, 20, 50f),
                        new Skill("투쟁의 불꽃","고통을 에너지로 바꿔 승리를 향해 나아가게 한다", 9999f, 9999f, 50, 60f)
                    ]),
                new ClassInitData("사무총장", 6f, 5f, 40f, 60f, 
                    "\"법대로 해봅시다.\"",
                    [
                        new Skill("언론 고발","공격력의 120 ~ 150%의 데미지를 준다", 120f, 150f, 5, 10f),
                        new Skill("보이콧","공격력의 200%의 데미지를 준다.",200f, 200f, 10, 30f),
                        new Skill("가스라이팅","공격력의 300~500%의 데미지를 준다.", 300f, 500f, 20, 50f),
                        new Skill("국민 청원","연대의 힘으로 역사를 바꾸는 외침이다.", 9999f, 9999f, 50, 60f),
                    ]),
                new ClassInitData("조직 국장", 8f, 3f, 60f, 40f,
                    "\"사람은 모두 평등하다.\"",
                    [
                        new Skill("죽창","120% 데미지를 준다.",120f, 120f, 5, 10f),
                        new Skill("화염병","200% 데미지를 준다.", 200f, 200f, 10, 30f),
                        new Skill("프로파간다","단결의 의지를 상승시킨다", 0f, 0f, 20, 50f),
                        new Skill("진정한 죽창","너도 한방 나도 한방", 9999f, 9999f, 50, 60f),
                    ])
            ];

            // (string itemName, string description, int itemPrice, EEquipType eType, ERank rank, float atkBonus, float defBonus, float maxHpBonus, float maxMpBonus)
            // 아이템명 / 설명 / 아이템가격 / 장비타입 / 랭크 / 어택보너스 / 방어보너스 / HP업 / MP업 /
            Equipments = new Equipment[] 
            {
                //Weapon List
                new Equipment("회사 화장실 휴지", "[무기] [일반] 닦을 때 따가운 화장실 휴지, 엉덩이의 처우를 개선해달라.", 100, EEquipType.Weapon, ERank.Normal, 10f, 0, 0, 0),
                new Equipment("부러진 법인카드", "[무기] [레어] 부도 직전 회사의 한도초과 카드, 가려운 곳 긁기에는 쓸만하다.", 500, EEquipType.Weapon, ERank.Rare, 30f, 0, 0, 0),
                new Equipment("연대의 확성기", "[무기] [에픽] 우리의 처절한 외침이 들리는가.", 2000, EEquipType.Weapon, ERank.Epic, 70f, 0, 0, 0),
                new Equipment("평화의 죽창", "[무기] [전설] 회사 앞에 심어놓은 대나무를 뽑아서 만들었다.", 5000, EEquipType.Weapon, ERank.Legendary, 100f, 0, 0, 0),
                new Equipment("사장님의 신용카드", "[무기] [신화] 사장님의 비자금이 숨겨진 카드. 돈쭐을 내줄 수 있다.", 10000, EEquipType.Weapon, ERank.Mythic, 200f, 0, 0, 0),

                //Armor List
                new Equipment("노동자의 조끼", "[갑옷] [일반] 땀에 젖은 노동자의 조끼.", 100, EEquipType.Armor, ERank.Normal, 0f, 10f, 0, 0),
                new Equipment("다단계 회사의 단체티", "[갑옷] [레어] 신념으로 똘똘 뭉친, 다단계 회사 직원들의 세미나복", 500, EEquipType.Armor, ERank.Rare, 0, 30f, 0, 0),
                new Equipment("가족같은 유니폼", "[갑옷] [에픽] 가족같은 분위기의 회사에서 지급해준 유니폼", 2000, EEquipType.Armor, ERank.Epic, 0, 70f, 200f, 0),
                new Equipment("구글 후드티", "[갑옷] [전설] 구글 본사 직원용 후드티. 방탄 소재.", 5000, EEquipType.Armor, ERank.Legendary, 0, 100f, 500f, 0),
                new Equipment("테슬라 우주 갑옷", "[갑옷] [신화] 일론머스크가 화성 전쟁을 대비한 우주 갑옷", 10000, EEquipType.Armor, ERank.Mythic, 0, 200f, 1000f, 0),

                //Accessary
                new Equipment("낡은 마스크", "[장신구] [일반] 얼굴을 가리고 당당히 목소리를 내자.", 100, EEquipType.Accessary, ERank.Normal, 5f, 0, 0, 50f),
                new Equipment("가족사진 목걸이", "[장신구] [레어] 가족 사진을 담은 목걸이. 힘을 내게 해준다.", 800, EEquipType.Accessary, ERank.Rare, 20f, 0, 0, 200f),
                new Equipment("프로파간다 깃발", "[장신구] [에픽] 우리의 목소리를 담은 깃발", 2000, EEquipType.Accessary, ERank.Epic, 50f, 0, 0, 500f),
                new Equipment("고용노동부 입장권", "[장신구] [전설] 소지하는 순간, 국가는 우리의 파트너가 된다.", 5000, EEquipType.Accessary, ERank.Legendary, 80f, 0, 0, 1000f),
                new Equipment("비리 의혹 녹취본", "[장신구] [신화] 비리를 녹취한 녹취본. 언론에 공개하는 순간 사장의 멘탈은 무너진다.", 10000, EEquipType.Accessary, ERank.Legendary, 150f, 0, 0, 2000f),

                //Helmet
                new Equipment("공사장 헬멧", "[투구] [일반] 얼굴을 가리고 당당히 목소리를 내다.", 100, EEquipType.Helmet, ERank.Normal, 0, 5f, 0, 50f),
                new Equipment("붉은 두건", "[투구] [에픽] 피로 물든 붉은 두건", 2000, EEquipType.Helmet, ERank.Epic, 0f, 30f, 0, 300f),
                new Equipment("투쟁의 삭발", "[투구] [신화] 그 어떤 헬멧보다 더 단단한, 삭발 투쟁의 결과물, 민머리", 10000, EEquipType.Helmet, ERank.Legendary, 0f, 60f, 0, 500f),
            };

            HealItems = new HealItem[]
            {
                new HealItem("믹스 커피", "[포션] [HP + 50] 아침 필수 도핑약", 10, 1, 50),
                new HealItem("야근의 핫식스", "[포션] [HP + 100] 야근을 버티게 한다.", 30, 1, 100),
                new HealItem("박카스", "[포션] [HP + 300] 풀려라 5천만, 풀려라 피로!", 100, 1, 300),
            };

            BattleItems = new BattleItem[]
            {
                new BattleItem("화염병", "[배틀아이템] [데미지 30] 목소리를 듣지 않는다면, 물리 공격이라도...", 100, 1, 30),
                new BattleItem("던지는 죽창", "[배틀아이템] [데미지 60] 1회용 원거리 죽창", 300, 1, 60),
                new BattleItem("마음의 촛불", "[배틀아이템] [데미지 100] 수백만의 마음이 담긴 촛불, 던진다.", 1000, 1, 100),
            };
            
            Monsters = new Monster[]
            {
                new Monster(0, 1, 1, "부당계약서", 3f, 3f, 10f, 10f, 40, new Skill[]{ new Skill("계약이행", "", 10f, 10f, 0, 10f)}),
                new Monster(1, 2, 3, "연장근무", 5f, 4f, 15f, 10f, 50, new Skill[]{ new Skill("업무의심연", "", 15f, 15f, 0, 10f)}),
                new Monster(2, 5, 5, "환영복지술사", 10f, 6f, 20f, 25f, 70, new Skill[]{ new Skill("덫없는환상","", 12f, 12f, 0, 10f)}),
                new Monster(3, 6, 7, "월급루팡", 9f, 8f, 35f, 30f, 100, new Skill[]{ new Skill("훔치기", "", 5f, 5f, 0, 10f)}),
                new Monster(4, 7, 10, "인사고과망령", 10f, 12f, 40f, 50f, 150, new Skill[]{ new Skill("불공정평가", "", 8f, 8f, 0, 10f)}),
                new Monster(5, 10, 12, "노동착취자", 25f, 20f, 60f, 50f, 170, new Skill[]{ new Skill("착취", "", 20f, 20f, 0, 10f)}),
                new Monster(6, 12, 15, "과로골렘", 20f, 40f, 100f, 20f, 250, new Skill[]{ new Skill("압박", "", 25f, 25f, 0, 10f)}),
                new Monster(7, 18, 25, "해고의그림자", 35f, 25f, 120f, 100f, 350, new Skill[]{ new Skill("권고사직", "", 30f, 30f, 0, 10f)}),
                new Monster(8, 25, 50, "사장드래곤", 40f, 40f, 150f, 100f, 500, new Skill[]{ new Skill("최상위결정권", "", 35, 35f, 0, 10f)}),

            };

            //string name, float[] power, int requiredLv, float requiredMp)
            Skills = new Skill[]
            {
                new Skill("계약이행","", 10f, 10f, 0, 10f),
                new Skill("업무의심연","", 15f, 15f, 0, 10f),
                new Skill("덫없는환상","", 12f, 12f, 0, 10f),
                new Skill("훔치기","", 5f, 5f, 0, 10f),
                new Skill("불공정평가","", 8f, 8f, 0, 10f),
                new Skill("착취","", 20f, 20f, 0, 10f),
                new Skill("압박","", 25f, 25f, 0, 10f),
                new Skill("권고사직","", 30f, 30f, 0, 10f),
                new Skill("최상위결정권","", 35, 35f, 0, 10f),
            };

            QuestList = [
                // 사냥 퀘스트
                new QuestModelHunting(0, 0, 3, "부당 계약 적발", "사내에 완연한 부당계약서들을 적발하고 기강을 바로 세워주세요!", 
                    new QuestReward(100, 30, [], new Dictionary<int, int>{ })),
                new QuestModelHunting(1, 1, 5, "과로의 원인", "비일비재한 연장근무에서 벗어나고 싶은 직장인들을 구해주세요.",
                    new QuestReward(500, 50, [2], new Dictionary<int, int>{ })),
                new QuestModelHunting(2, 3, 5, "월급루팡", "일하는 손 따로 노는 손 따로.\n양심없는 루팡들을 처지해주세요!",
                    new QuestReward(2000, 70, [3], new Dictionary<int, int>{ })),

                // 달성 퀘스트
                new QuestModelGoal(3, ERequestGoal.WinBattle, 10, "근면성실", "사원의 미덕은 근면성실함이지!\n자네의 성실함을 보여주게나!",
                    new QuestReward(5000, 100, [5], new Dictionary<int, int>{ })),
                new QuestModelGoal(4, ERequestGoal.EnhanceItem, 10, "장인도 도구를 가린다", "자네 이번달 실적이 너무 낮군.\n저기 가서 옷도 좀 수선하고 좋은 가방으로 리테일링이라도 하고 와.",
                    new QuestReward(5000, 100, [5], new Dictionary<int, int>{ })),
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

        [JsonProperty]
        public long CurId = 0;

        private InstanceManager() { }

        public static InstanceManager GetInstance()
        {
            if(_instance == null)
                _instance = new InstanceManager();

            return _instance;
        }

        public void Load(long data)
        {
            CurId = data;
        }

        public long GetId()
        {
            return ++CurId;
        }
    }
}
