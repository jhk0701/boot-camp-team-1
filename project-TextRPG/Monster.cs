using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace project_TextRPG
{
    class Monster : Unit
    {
        public int Gold { get; set; }
        public Skill[] Skills { get; set; }

        public Monster(string name) : base(name) { }
    }

    class Goblin : Monster
    {
        public Goblin(string name) : base(name)
        {
            Name = name;
            BasicAttack = 3f;
            BasicDefense = 3f;
            MaxHealth = 10f;
            Health = MaxHealth;
            MaxMana = 10f;
            Mana = MaxMana;
            Skills = [
                new Skill()
                ]
        }
    }
    class Orc : Monster
    {
        public Orc(string name) : base(name)
        {
            Name = name;
            BasicAttack = 5f;
            BasicDefense = 5f;
            MaxHealth = 12f;
            Health = MaxHealth;
            MaxMana = 12f;
            Mana = MaxMana;
            Skills = [
                new Skill()
                ]
        }

    }
    class Troll : Monster
    {
        public Troll(string name) : base(name)
        {
            Name = name;
            BasicAttack = 7f;
            BasicDefense = 7f;
            MaxHealth = 14f;
            Health = MaxHealth;
            MaxMana = 14f;
            Mana = MaxMana;
            Skills = [
                new Skill()
                ]
        }

    }

    class Slime : Monster
    {
        public Slime(string name) : base(name)
        {
            Name = name;
            BasicAttack = 9f;
            BasicDefense = 9f;
            MaxHealth = 16f;
            Health = MaxHealth;
            MaxMana = 16f;
            Mana = MaxMana;
            Skills = [
                new Skill()
                ]
        }

    }

    class Dragon: Monster
    {
        public Dragon(string name) : base(name)
        {
            Name = name;
            BasicAttack = 3f;
            BasicDefense = 3f;
            MaxHealth = 10f;
            Health = MaxHealth;
            MaxMana = 10f;
            Mana = MaxMana;
            Skills = [
                new Skill()
                ]

        }

    }
}
