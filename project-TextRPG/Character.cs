using Newtonsoft.Json;

namespace project_TextRPG
{
    public class Character : Unit
    {
        [JsonProperty]
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

                onLevelChanged?.Invoke();
            } 
        }

        public override int Exp 
        {
            get { return base.Exp; }
            set 
            {
                base.Exp = value;
                int needExp = LevelCalculator();

                while(base.Exp >= needExp)
                {
                    Level++;
                    base.Exp -= needExp;

                    needExp = LevelCalculator();
                }
            } 
        }

        [JsonProperty]
        public Skill[] Skills { get; protected set; }

        [JsonProperty]
        public Inventory Inventory { get; protected set; }

        public int StageScore { get; set; }

        Action onLevelChanged { get; set; }


        public Character(string name) : base(name)
        {
            Level = 1;
            Exp = 0;
            Inventory = new Inventory();
            StageScore = 1; 
        }

        public void UpdateStageScore(int clearedDungeon)
        {
            if (clearedDungeon < StageScore)
                return;

            StageScore++;
        }

        public int LevelCalculator()
        {
            int needexp;
            if (Level == 1)
            {
                needexp = 10;
            }
            else if (Level == 2)
            {
                needexp = 25;
            }
            else if (Level == 3)
            {
                needexp = 30;
            }
            else if (Level == 4)
            {
                needexp = 35;
            }
            else if (Level == 5)
            {
                needexp = 40;
            }
            else
            {
                needexp = Level * 10;
            }

            return needexp;

            if (Exp >= needexp)
            {
                Level += 1;
                Exp -= needexp;
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

        public void Start()
        {
            Inventory.Start(this);

            onLevelChanged = () =>
            {
                // 레벨업 시, 호출 이벤트
                // 관련 퀘스트 수행
                QuestManager.GetInstance().PerformQuest(this, base.Level);
                Utility.WriteColorScript("레벨이 상승했습니다.", ConsoleColor.Blue);

                BasicAttack += 0.5f;
                BasicDefense += 1f;
            };
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
            //Level = 50;

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