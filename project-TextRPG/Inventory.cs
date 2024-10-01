namespace project_TextRPG
{
    class Inventory
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
                    RemoveBonus(prev.Bonus);
                }

                Equipments[item.type] = item;
            }
            else
            {
                // 장착
                Equipments.Add(item.type, item);
            }

            AdjustBonus(item.Bonus);
        }

        public void Unequip(Equipment item)
        {
            Equipment prev = Equipments[item.type];
            RemoveBonus(prev.Bonus);

            Equipments[item.type] = null;
        }

        void RemoveBonus(Dictionary<EEquipBonus, float> bonus)
        {
            foreach (KeyValuePair<EEquipBonus, float> i in bonus)
            {
                switch (i.Key)
                {
                    case EEquipBonus.ATK:
                        _player.EquipAttack -= i.Value;
                        break;
                    case EEquipBonus.DEF:
                        _player.EquipDefense -= i.Value;
                        break;
                    case EEquipBonus.HP:
                        _player.EquipHealth -= i.Value;
                        break;
                    case EEquipBonus.MP:
                        _player.EquipMana -= i.Value;
                        break;
                }
            }
        }

        void AdjustBonus(Dictionary<EEquipBonus, float> bonus)
        {
            foreach (KeyValuePair<EEquipBonus, float> i in bonus)
            {
                switch (i.Key)
                {
                    case EEquipBonus.ATK:
                        _player.EquipAttack += i.Value;
                        break;
                    case EEquipBonus.DEF:
                        _player.EquipDefense += i.Value;
                        break;
                    case EEquipBonus.HP:
                        _player.EquipHealth += i.Value;
                        break;
                    case EEquipBonus.MP:
                        _player.EquipMana += i.Value;
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
            return Items.Contains(item);
        }
    }
}
