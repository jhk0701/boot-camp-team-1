using System.Diagnostics;

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
    


    class Goblin : Monster
    {
        public Goblin(string name) : base(name)
        {
            Exp = 3;
            BasicAttack = 5f;
            BasicDefense = 5f;
            MaxHealth = 12f;
            Health = MaxHealth;
            MaxMana = 12f;
            Mana = MaxMana;
            //BasicAttack = basicattack;
            //BasicDefense = basicdefence;
            //MaxHealth = maxhealth;
            //Health = MaxHealth;
            //MaxMana = maxmana;
            //Mana = MaxMana;Gold = gold;
            //Skills = skills;
        }
    }
    
    class Orc : Monster
    {
        public Orc(string name) : base(name)
        {
            Exp = 4;
            BasicAttack = 5f;
            BasicDefense = 5f;
            MaxHealth = 12f;
            Health = MaxHealth;
            MaxMana = 12f;
            Mana = MaxMana;   
            //Skills = [
            //    new Skill()
            //    ]
        }

    }
    class Troll : Monster
    {
        public Troll(string name) : base(name)
        {
            Exp = 5;
            BasicAttack = 7f;
            BasicDefense = 7f;
            MaxHealth = 14f;
            Health = MaxHealth;
            MaxMana = 14f;
            Mana = MaxMana;
            //Skills = [
            //    new Skill()
            //];
        }

    }
    class Slime : Monster
    {
        public Slime(string name) : base(name)
        {
            Exp = 6;
            BasicAttack = 9f;
            BasicDefense = 9f;
            MaxHealth = 16f;
            Health = MaxHealth;
            MaxMana = 16f;
            Mana = MaxMana;
            //Skills = [
            //    new Skill()
            //];
        }

    }
    class Dragon: Monster
    {
        public Dragon(string name) : base(name)
        {
            Exp = 7;
            BasicAttack = 3f;
            BasicDefense = 3f;
            MaxHealth = 10f;
            Health = MaxHealth;
            MaxMana = 10f;
            Mana = MaxMana;
            //Skills = [
            //    new Skill()
            //];

        }

    }


}