namespace project_TextRPG
{
    public class Character : Unit
    {
        public EClass CharClass { get; protected set; }
        public int Gold { get; set; }
        public Skill[] Skills { get; protected set; }
        public Inventory Inventory { get; protected set; }

        public Character(string name) : base(name) 
        { 
            Inventory = new Inventory(this);
        }

        public void LevelCalculator(Character player)
        {
            int needexp;
            if (player.Level == 1)
            {
                needexp = 10;
            }
            else if (player.Level == 2)
            {
                needexp = 25;
            }
            else if (player.Level == 3)
            {
                needexp = 30;
            }
            else if (player.Level == 4)
            {
                needexp = 35;
            }
            else if (player.Level == 5)
            {
                needexp = 40;
            }
            else
            {
                needexp = 45;
            }
            if (player.Exp >= needexp)
            {
                player.Level += 1;
            }
        }

        public void Initialize(ClassInitData initData)
        {
            BasicAttack = initData.attack;
            BasicDefense = initData.defense;
            MaxHealth = initData.maxHealth;
            MaxMana = initData.maxMana;
            Health = MaxHealth;
            Mana = MaxMana;
        }
    }

    /// <summary>
    /// 노조 위원장 : 전사 포지션
    /// </summary>
    class ChairmanOfUnion : Character
    {
        public ChairmanOfUnion(string name) : base(name)
        {
            CharClass = EClass.ChairmanOfUnion;
            //Name = name;
            //isDead = false;

            //Level = 5;
            //BasicAttack = 10f;
            //BasicDefense = 1f;
            //MaxHealth = 50f;
            //Health = MaxHealth;
            //MaxMana = 50f;
            //Mana = MaxMana;
            Initialize(DataDefinition.GetInstance().ClassInitDatas[(int)CharClass]);

            Skills = [
                new Skill("파업", [120f, 150f], 5, 10f),
                new Skill("단식 투쟁", [250f, 250f], 10, 30f),
                new Skill("트럭 시위", [300f, 500f], 20, 50f),
                new Skill("투쟁의 불꽃", [9999f, 9999f], 50, 60f),
            ];
        }
    }

    /// <summary>
    /// 사무국장 : 마법사 포지션
    /// </summary>
    class SecretaryGeneral : Character
    {
        public SecretaryGeneral(string name) : base(name)
        {
            CharClass = EClass.SecretaryGeneral;
            Initialize(DataDefinition.GetInstance().ClassInitDatas[(int)CharClass]);

            Skills = [
                new Skill("언론 고발", [120f, 150f], 5, 10f),
                new Skill("보이콧", [200f, 200f], 10, 30f),
                new Skill("가스라이팅", [300f, 500f], 20, 50f),
                new Skill("국민 청원", [9999f, 9999f], 50, 60f),
            ];
        }
    }

    /// <summary>
    /// 조직위원장 : 도적 포지션
    /// </summary>
    class DirectorOfUnion : Character
    {
        public DirectorOfUnion(string name) : base(name)
        {
            CharClass = EClass.DirectorOfUnion;
            Initialize(DataDefinition.GetInstance().ClassInitDatas[(int)CharClass]);

            Skills = [
                new Skill("죽창", [120f, 120f], 5, 10f),
                new Skill("화염병", [200f, 200f], 10, 30f),
                new Skill("프로파간다", [0f, 0f], 20, 50f),
                new Skill("진정한 죽창", [9999f, 9999f], 50, 60f),
            ];
        }
    }

}