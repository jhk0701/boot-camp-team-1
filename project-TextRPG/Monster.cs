using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace project_TextRPG
{
    public class Monster : Unit
    {
        public int Gold { get; set; }
        public Skill[] Skills { get; set; }

        public Monster(string name, float basicattack, float basicdefence, float maxhealth, float maxmana, int gold, Skill[] skills) : base(name)
        {
            Name = name;
            BasicAttack = basicattack;
            BasicDefense = basicdefence;
            MaxHealth = maxhealth;
            Health = MaxHealth;
            MaxMana = maxmana;
            Mana = MaxMana;
            Gold = gold;
            Skills = skills;
            isDead = false;
        }

    }


    }
