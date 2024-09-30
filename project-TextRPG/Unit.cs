namespace project_TextRPG
{
    public class Unit
    {
        public String Name { get; protected set; }

        public float BasicAttack { get; protected set; }
        public float EquipAttack { get; set; }
        public float Attack 
        { 
            get 
            { 
                return BasicAttack + EquipAttack; 
            } 
        }

        public float BasicDefense { get; protected set; }
        public float EquipDefense { get; set; }
        public float Defense 
        { 
            get 
            { 
                return BasicDefense + EquipDefense;
            } 
        }

        public float MaxHealth { get; protected set; }
        public float EquipHealth { get; set; }
        public float Health { get; protected set; }

        public float MaxMana { get; protected set; }
        public float EquipMana { get; set; }
        public float Mana { get; protected set; }
        public bool isDead { get; protected set; }

        int _lv;
        public int Level 
        {
            get { return _lv; }
            set 
            {
                if (value < _lv)
                    return;

                _lv = value;

                Console.WriteLine($"레벨이 상승했습니다.\nLv. {_lv - 1} -> {_lv}\n능력치가 소폭 상승합니다.");

                BasicAttack += 0.5f;
                BasicDefense += 1f;
            }
        }
        int _exp;
        public int Exp 
        {
            get { return _exp; }
            set 
            { 
                _exp = value;
                // 레벨업 데이터 반영 구간
                // 임시 반영
                if(_exp >= Level * 5)
                {
                    Level++;
                    _exp = 0;
                }
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name"></param>
        public Unit(string name) 
        { 
            Name = name;
        }


        public virtual float TakeDamage(float damage) 
        { 
            Health -= damage;
            return Health;
        }
    }
}
