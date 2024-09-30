namespace project_TextRPG
{
    class Character : Unit
    {
        public EClass CharClass { get; protected set; }
        public int Gold { get; set; }
        public Skill[] Skills { get; protected set; }
        public Inventory Inventory { get; protected set; }

        public Character(string name) : base(name) 
        { 
            Inventory = new Inventory(this);
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

            Skills = [
                new Skill("죽창", [120f, 120f], 5, 10f),
                new Skill("화염병", [200f, 200f], 10, 30f),
                new Skill("프로파간다", [0f, 0f], 20, 50f),
                new Skill("진정한 죽창", [9999f, 9999f], 50, 60f),
            ];
        }
    }

}