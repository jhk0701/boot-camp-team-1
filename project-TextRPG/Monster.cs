namespace project_TextRPG
{
    public class Monster : Unit
    {
        public int Id { get; set; }
        public int Gold { get; set; }
        public Skill[] Skills { get; set; }

        public Monster(string name) : base(name) { }
        public Monster(int id, string name, float basicattack, float basicdefence, float maxhealth, float maxmana, int gold, Skill[] skills) : base(name)
        {
            Id = id;
            BasicAttack = basicattack;
            BasicDefense = basicdefence;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            MaxMana = maxmana;
            Mana = MaxMana;
        }

        public Monster Copy() // 프로토타입
        {
            Monster copy = new Monster(
                Id, Name, BasicAttack, BasicDefense, MaxHealth, MaxMana, Gold, Skills
            );
            return copy;
        }

    }

}