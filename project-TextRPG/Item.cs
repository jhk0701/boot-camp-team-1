namespace project_TextRPG
{
    public class Item
    {        
        public string Name { get; set; }
        
        string Description { get; set; } //아이템 설명
        public int Price { get; set; } // 아이템 가격
        public bool IsPossessed { get; set; } = false; //아이템 소지여부

        public ERank eRank { get; set; }
        
        public Item(string itemName, string description, int itemPrice, bool isPossessed)
        {
            Name = itemName;
            Description = description;
            Price = itemPrice;
            IsPossessed = isPossessed;
        }

        public string GetName() //
        {
            return Name;
        }
    }

    public class Equipment : Item
    {
        public bool IsEquipped { get; set; } = false;    //장착 여부
        public EEquipType eqType { get; set; }   //장비 유형
        public ERank eqRank { get; set; }   //아이템 등급
        public float AtkBonus { get; set; }  //공격력 보너스
        public float DefBonus { get; set; }  //방어력 보너스
        public float MaxHpBonus { get; set; }   //최대 HP보너스
        public float MaxMpBonus { get; set; }   //최대 MP보너스

        public Equipment(string itemName, string description, int itemPrice, bool isPossessed, EEquipType eqType, ERank eqRank, float atkBonus, float defBonus, float maxHpBonus, float maxMpBonus) : base(itemName, description, itemPrice, isPossessed)
        {
            IsEquipped = false;
            this.eqType = eqType;
            this.eqRank = eqRank;
            this.AtkBonus = atkBonus;
            this.DefBonus = defBonus;
            this.MaxHpBonus = maxHpBonus;
            this.MaxMpBonus = maxMpBonus;
        }
    }

    public class BattleItem : Item
    {

        public int ItemCount { get; set; } //아이템 개수


        public BattleItemType battleItemType { get; set; } // 0: 포션, 1: 데미지아이템

        public BattleItem(string itemName, string description, int itemPrice, bool isPossessed, int itemCount, BattleItemType battleItemType) : base(itemName, description, itemPrice, isPossessed)
        {
            this.ItemCount = itemCount;
            this.battleItemType = battleItemType;
        }
        public void Use()
        {
            ItemCount--;
            Console.WriteLine($"플레이어는 {Name}을(를) 사용했다!");
            Console.WriteLine($"{Name}은 {ItemCount}개 남았습니다.");
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            BattleItem potion = new BattleItem("Healing Potion", "회복 포션", 50, true, 3, 0);

            potion.Use();
        }
    }

}
