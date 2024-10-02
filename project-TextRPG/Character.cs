namespace project_TextRPG
{
    public class Character : Unit
    {
        public EClass CharClass { get; protected set; }
        public int Gold { get; set; }

        public override int Level 
        {
            get { return base.Level; } 
            set
            {
                if (value < base.Level)
                    return;
                
                base.Level = value;
                QuestManager.GetInstance().PerformQuest(this, base.Level);
                Utility.WriteColorScript("레벨이 상승했습니다.", ConsoleColor.Blue);

                BasicAttack += 0.5f;
                BasicDefense += 1f;
            } 
        }

        public Skill[] Skills { get; protected set; }
        public Inventory Inventory { get; protected set; }

        public int StageScore { get; set; }

        public Character(string name) : base(name)
        {
            Level = 1;
            Exp = 0;
            Inventory = new Inventory(this);
            StageScore = 1; 
        }
        public void UpdateStageScore()
        {
            StageScore ++;
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
                player.Exp -= needexp;
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
            Initialize(DataDefinition.GetInstance().ClassInitDatas[(int)CharClass]);
            Level = 50;

            Skills = [
                new Skill("파업","공격력의 120 ~ 150%의 데미지를 준다", 120f, 150f, 5, 10f),
                new Skill("단식 투쟁","공격력의 250%의 데미지를 준다.", 250f, 250f, 10, 30f),
                new Skill("트럭 시위","가챠에 사용한 횟수만큼 데미지를 준다.", 300f, 500f, 20, 50f),
                new Skill("투쟁의 불꽃","고통을 에너지로 바꿔 승리를 향해 나아가게 한다", 9999f, 9999f, 50, 60f),
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
                new Skill("언론 고발","공격력의 120 ~ 150%의 데미지를 준다", 120f, 150f, 5, 10f),
                new Skill("보이콧","공격력의 200%의 데미지를 준다.",200f, 200f, 10, 30f),
                new Skill("가스라이팅","공격력의 300~500%의 데미지를 준다.", 300f, 500f, 20, 50f),
                new Skill("국민 청원","연대의 힘으로 역사를 바꾸는 외침이다.", 9999f, 9999f, 50, 60f),
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
                new Skill("죽창","120% 데미지를 준다.",120f, 120f, 5, 10f),
                new Skill("화염병","200% 데미지를 준다.", 200f, 200f, 10, 30f),
                new Skill("프로파간다","단결의 의지를 상승시킨다", 0f, 0f, 20, 50f),
                new Skill("진정한 죽창","너도 한방 나도 한방", 9999f, 9999f, 50, 60f),
            ];
        }
    }

}