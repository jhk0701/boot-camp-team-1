namespace project_TextRPG
{
    public class Unit
    {
        public String Name { get; protected set; }

        public float BasicAttack { get; protected set; }
        public virtual float EquipAttack { get; set; }
    

        public float Attack 
        { 
            get 
            { 
                return BasicAttack + EquipAttack; 
            } 
        }

        public float BasicDefense { get; protected set; }
        public virtual float EquipDefense { get; set; }
        public float Defense 
        { 
            get 
            { 
                return BasicDefense + EquipDefense;
            } 
        }

        public float MaxHealth { get; protected set; }
        public float EquipHealth { get; set; }

        float _health;
        public float Health 
        { 
            get { return _health; }
            protected set
            {
                if (isDead) return;

                _health = value;
                if(_health < 0f) // 사망
                {
                    _health = 0f;
                    isDead = true; 
                }

                if(_health > MaxHealth + EquipHealth) // 과회복
                    _health = MaxHealth + EquipHealth;
            }
        }

        public float MaxMana { get; protected set; }
        public float EquipMana { get; set; }

        float _mana;
        public float Mana 
        {
            get { return _mana; }
            protected set
            {
                _mana = value;

                if (_mana < 0f)
                    _mana = 0f;

                if (_mana > MaxMana + EquipMana) // 과회복
                    _mana = MaxMana + EquipMana;
            }
        }

        public bool isDead { get; set; }

        int _lv;
        public virtual int Level { get; set; }
        //{
        //    get { return _lv; }
        //    set 
        //    {
        //        if (value < _lv)
        //            return;

        //        _lv = value;
        //        QuestManager.GetInstance().PerformQuest(this as Character, _lv);

        //        Utility.WriteColorScript("레벨이 상승했습니다.", ConsoleColor.Blue);

        //        BasicAttack += 0.5f;
        //        BasicDefense += 1f;
        //    }
        //}
        //int _exp;
        public int Exp { get; set; }
        //{
        //    get { return _exp; }
        //    set 
        //    { 
        //        _exp = value;
        //        // 레벨업 데이터 반영 구간
        //        // 임시 반영
        //        //if(_exp >= Level * 5)
        //        //{
        //        //    Level++;
        //        //    _exp = 0;
        //        //}
        //    }
        //}


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name"></param>
        public Unit(string name) 
        { 
            Name = name;
            isDead = false;
        }

        public virtual float TakeDamage(float damage) 
        {
            if (damage <= 0f)
            {
                return Health;
            }

            Health -= damage;
            return Health;
        }

        public void Heal(float healAmount)
        {
            if (healAmount < 0f)
                return;

            Health += healAmount;
            Console.WriteLine($"{Name}은(는) {healAmount}의 체력을 회복했습니다. 현재 체력: {Health}");
        }
        
        public virtual float SetManaDrop(float usemana)
        {
            if (usemana <= 0f)
                return Mana;

            Mana -= usemana;
            return Mana;
        }

        public virtual void Rest()
        {
            Health = MaxHealth + EquipHealth;
            Mana = MaxMana + EquipMana;
        }

        public virtual void Revive()
        {
            isDead = false;
            Health = 1f;
            Mana = 1f;
        }
    }
}
