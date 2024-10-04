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

            Skills = initData.skills;
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
        }
    }

}