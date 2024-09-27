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
            Exp = 5;
            BasicAttack = 3f;
            BasicDefense = 3f;
            MaxHealth = 10f;
            Health = MaxHealth;
            MaxMana = 10f;
            Mana = MaxMana;
        }
    }
    class Orc : Monster
    {
        public Orc(string name) : base(name)
        {

        }

    }
    class Troll : Monster
    {
        public Troll(string name) : base(name)
        {

        }

    }

    class Slime : Monster
    {
        public Slime(string name) : base(name)
        {

        }

    }


}
