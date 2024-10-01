using System.Text;
using System.Collections.Generic;

namespace project_TextRPG
{

    //public enum ERank { Normal, Rare, Epic, Legendary }
    //public enum EEquipType { Weapon, Armor, Accessory }
    //public enum EEquipBonus { ATK, DEF, HP, MP }
    //public enum BattleItemType { Potion, Damage }

    public class Item
    {
        long _id;

        public string Name { get; protected set; }
        public string Description { get; protected set; } //아이템 설명
        public int Price { get; protected set; } // 아이템 가격

        // 아이템 소지여부
        // 소지 여부의 경우 플레이어가 가지고 있는지 여부를 따지면 돼서
        // 플레이어(주체)가 가진 인벤토리에서 이 아이템이 있는지 등의 방식으로 확인하면 될 것 같습니다.
        // public bool IsPossessed { get; set; } = false;

        public ERank Rank { get; set; }
        
        public Item(string itemName, string description, int itemPrice, ERank rank)
        {
            Name = itemName;
            Description = description;
            Price = itemPrice;
            Rank = rank;
            //IsPossessed = isPossessed;

            //_id = DataDefinition.GetInstance().GetInstanceId();
        }
  }

    public class Equipment : Item, ICopyable<Equipment>
    {
        // 장착 여부
        // 소지 여부 때와 마찬가지로
        // 플레이어(주체)가 이 아이템을 착용하고 있는지 등의 방식으로 확인할 것 같습니다.
        //public bool IsEquipped { get; set; } = false;    //장착 여부
        public EEquipType type { get; set; }   // 장비 유형

        // 아이템 등급
        // 아이템의 등급 같은 경우는 대체로 아이템 자체의 공통적인 등급으로 사용할 듯하여
        // 상위 클래스로 옮겼습니다.
        // public ERank eqRank { get; set; }

        // 추측해보기로는 착용한 장비에서 더 다양한 추가 보너스를 얻도록 설계하신 것 같습니다.
        // 아래처럼 해주어도 되지만
        // 개인적으로 추천드리는 방법은 배열이나 Dictionary 등의 컬렉션을 이용해서 Bonus라는 변수로 묶는 것입니다.
        //public float AtkBonus { get; set; }  //공격력 보너스
        //public float DefBonus { get; set; }  //방어력 보너스
        //public float MaxHpBonus { get; set; }   //최대 HP보너스
        //public float MaxMpBonus { get; set; }   //최대 MP보너스

        // 컬렉션을 추천드린 이유
        // 1. 보너스 모두 float형의 자료라는 공통점
        // 2. 보너스라는 목적을 지녔지만 타켓이 달라 서로 다른 변수로 선언하면
        //    각 보너스를 적용하기 위해서 각각의 변수를 if문으로 하나하나 검사해주어야 합니다.
        //    만약 위 변수들을 컬렉션을 이용한 방법으로 묶는다면 for문을 이용해서 각각의 if문을 사용하는 아니라 
        //    컬렉션의 요소들이 값이 0보다 크다면 적용한다 라는 방법으로 적용할 수 있습니다.

        // 아래는 위의 설명을 한번 구현해본 것입니다.
        // 보너스 대상 : 보너스양
        public Dictionary<EEquipBonus, float> Bonus { get; set; }

        public Equipment(string itemName, string description, int itemPrice, EEquipType eType, ERank rank, float atkBonus, float defBonus, float maxHpBonus, float maxMpBonus) : base(itemName, description, itemPrice, rank)
        {
            type = eType;
            Rank = rank;
            
            Bonus = new Dictionary<EEquipBonus, float>();
            if(atkBonus != 0f)
                Bonus.Add(EEquipBonus.ATK, atkBonus);
            if (defBonus != 0f)
                Bonus.Add(EEquipBonus.DEF, defBonus);
            if (maxHpBonus != 0f)
                Bonus.Add(EEquipBonus.HP, maxHpBonus);
            if (maxMpBonus != 0f)
                Bonus.Add(EEquipBonus.MP, maxMpBonus);
        }

        public string GetBonusSpec()
        {
            StringBuilder sb = new StringBuilder();

            EEquipBonus[] type = Bonus.Keys.ToArray();
            for(int i = 0; i < type.Length; i++)
            {
                if (Bonus[type[i]] != 0f)
                    sb.Append($"{type[i].ToString()} {(Bonus[type[i]] > 0 ? $"+{Bonus[type[i]]}" : $"{Bonus[type[i]]}")}");

                if (i < type.Length - 1)
                    sb.Append(", ");
            }

            return sb.ToString();
        }

        public Equipment Copy()
        {
            Equipment copy = new Equipment(
                Name, Description, Price, type, Rank, 
                Bonus[EEquipBonus.ATK],
                Bonus[EEquipBonus.DEF],
                Bonus[EEquipBonus.HP],
                Bonus[EEquipBonus.MP]
            );
            return copy;
        }
    }

    public class BattleItem : Item
    {

        public int ItemCount { get; set; } //아이템 개수

        public BattleItemType BattleItemType { get; set; } // 0: 포션, 1: 데미지아이템

        public BattleItem(string itemName, string description, int itemPrice, int itemCount, BattleItemType BattleItemType) : base(itemName, description, itemPrice, ERank.Normal)
        {
            ItemCount = itemCount;
            this.BattleItemType = BattleItemType;
        }


        public virtual void Use()
        {
            ItemCount--;
            Console.WriteLine($"플레이어는 {Name}을(를) 사용했다!");
        }
    }

<<<<<<< Updated upstream
    class Program
    {
        public static void Main(string[] args)
        {
            BattleItem potion = new BattleItem("Healing Potion", "회복 포션", 50, true, 3, 0);

            potion.Use();
=======
    class NewProgram
    {
        static void Main(string[] args)
        {
            List<BattleItem> battleitems = new List<BattleItem>
            {
                new BattleItem("박카스", "박카스는 노조를 응원합니다. 힘내세요! HP + 100", 100, 1, 1),
                new BattleItem("화염병", "화염병을 투척한다. 데미지 200", 10, 1, 0),
            };
>>>>>>> Stashed changes
        }
    }

}
