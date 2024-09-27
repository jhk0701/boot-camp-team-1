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
    }

    public class Equipment : Item
    {
        public bool IsEquipped { get; set; } = false;    //장착 여부
        public EqType eqType { get; set; }   //장비 유형
        public EqRank eqRank { get; set; }   //아이템 등급
        public float AtkBonus { get; set; }  //공격력 보너스
        public float DefBonus { get; set; }  //방어력 보너스
        public float MaxHpBonus { get; set; }   //최대 HP보너스
        public float MaxMpBonus { get; set; }   //최대 MP보너스

        public Equipment(string itemName, string description, int itemPrice, bool isPossessed, EqType eqType, EqRank eqRank, float atkBonus, float defBonus, float maxHpBonus, float maxMpBonus) : base(itemName, description, itemPrice, isPossessed)
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
    }


    //아이템 랭크
    public enum EqRank : int
    {
        Normal = 0,
        Rare = 1,
        Epic = 2,
        Legendary = 3
    }
    public enum EqType : int
    {
        Weapon = 0,
        Armor = 1,
        Helmet = 2,
        Accessary = 3,
    }
    public enum BattleItemType : int
    {
        Potion = 0,
        DamageItem = 1
    }



}
