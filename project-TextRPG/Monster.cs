using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace project_TextRPG
{
    public class Monster : Unit
    {
        public int Id { get; set; }
        public int Gold { get; set; }
        public Skill[] Skills { get; set; }

        public Monster(string name) : base(name) { }
        public Monster(string name, int id, float atk, float def, float hp = 10f, float mp = 10f) : base(name) 
        { 
            Id = id;

            BasicAttack = atk;
            BasicDefense = def;
            MaxHealth = hp;
            Health = MaxHealth;
            MaxMana = mp;
            Mana = MaxMana;

            isDead = false;
        }
        public Monster(string name, float basicattack, float basicdefence, float maxhealth, float maxmana, int gold, Skill[] skills) : base(name)
        {
            Name = name;
            BasicAttack = basicattack;
            BasicDefense = basicdefence;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            MaxMana = maxmana;
            Mana = MaxMana;
        }

    }
    
    class Goblin : Monster
    {
        public Goblin(string name) : base(name)
        {
            Name = name;
            BasicAttack = basicattack;
            BasicDefense = basicdefence;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            MaxMana = maxmana;
            Mana = MaxMana;Gold = gold;
            Skills = skills;
            isDead = false;
        }
    }
    
    class Orc : Monster
    {
        public Orc(string name) : base(name)
        {
            Name = name;
            Exp = 5;
            BasicAttack = 5f;
            BasicDefense = 5f;
            MaxHealth = 12f;
            Health = MaxHealth;
            MaxMana = 12f;
            Mana = MaxMana;   
            isDead = false;
            //Skills = [
            //    new Skill()
            //    ]
        }

    }
    class Troll : Monster
    {
        public Troll(string name) : base(name)
        {
            Name = name;
            Exp = 5;
            BasicAttack = 7f;
            BasicDefense = 7f;
            MaxHealth = 14f;
            Health = MaxHealth;
            MaxMana = 14f;
            Mana = MaxMana;
            isDead = false;
            //Skills = [
            //    new Skill()
            //];
        }

    }
    class Slime : Monster
    {
        public Slime(string name) : base(name)
        {
            Name = name;
            Exp = 5;
            BasicAttack = 9f;
            BasicDefense = 9f;
            MaxHealth = 16f;
            Health = MaxHealth;
            MaxMana = 16f;
            Mana = MaxMana;
            isDead = false;
            //Skills = [
            //    new Skill()
            //];
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
            isDead = false;
            //Skills = [
            //    new Skill()
            //];

        }

    }
}