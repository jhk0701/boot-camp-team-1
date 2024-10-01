namespace project_TextRPG
{
    public class Inventory
    {
        Character _player;
        
        /// <summary>
        /// 인벤토리에 있는 아이템들
        /// </summary>
        public Equipment[] Items { get; protected set; }

        /// <summary>
        /// 착용한 아이템들
        /// </summary>
        public Dictionary<EEquipType, Equipment> Equipments { get; protected set; }

        public Inventory(Character owner)
        {
            _player = owner;
            Items = new Equipment[0];
            Equipments = new Dictionary<EEquipType, Equipment>();
        }

        public void Add(Equipment item)
        {
            List<Equipment> list = Items.ToList();
            list.Add(item);
            Items = list.ToArray();
        }

        public void Remove(Equipment item)
        {
            // 나중에 중복 아이템 등의 경우에 방법 달라 질 수 있으니 유의할 것.
            List<Equipment> list = Items.ToList();
            list.Remove(item);
            Items = list.ToArray();

            //장착 중 장비일 때
            if (IsEquipped(item))
                Unequip(item);
        }

        public void Equip(Equipment item) 
        {
            if (Equipments.ContainsKey(item.type))
            {
                // 교체
                if (Equipments[item.type] != null)
                {
                    Equipment prev = Equipments[item.type];
                    AdjustBonus(prev, -1);
                }

                Equipments[item.type] = item;
            }
            else
            {
                // 장착
                Equipments.Add(item.type, item);
            }

            AdjustBonus(item);
        }

        public void Unequip(Equipment item)
        {
            Equipment prev = Equipments[item.type];
            AdjustBonus(prev, -1);

            Equipments[item.type] = null;
        }

        void AdjustBonus(Equipment e, int dir = 1)
        {
            float enhance = 1f + e.Enhancements.Sum() * 0.01f;

            foreach (KeyValuePair<EEquipBonus, float> i in e.Bonus)
            {
                float val = i.Value * enhance * (float)dir;

                switch (i.Key)
                {
                    case EEquipBonus.ATK:
                        _player.EquipAttack += val;
                        break;
                    case EEquipBonus.DEF:
                        _player.EquipDefense += val;
                        break;
                    case EEquipBonus.HP:
                        _player.EquipHealth += val;
                        break;
                    case EEquipBonus.MP:
                        _player.EquipMana += val;
                        break;
                }
            }
        }

        public bool IsEquipped(Equipment item)
        {
            return Equipments.ContainsKey(item.type) && Equipments[item.type] == item;
        }

        public bool HasItem(Equipment item)
        {
            //return Items.Contains(item);
            return Items.Any(n => n.Name.Equals(item.Name));
        }
    }
}
