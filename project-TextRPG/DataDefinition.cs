namespace project_TextRPG
{
    /// <summary>
    /// 플레이어 캐릭터 직업 타입
    /// </summary>
    public enum EClass : int
    {
        /// <summary>
        /// 노조 위원장 : 전사
        /// </summary>
        ChairmanOfUnion = 0,
        /// <summary>
        /// 사무 국장(총장) : 마법사
        /// </summary>
        SecretaryGeneral,
        /// <summary>
        /// 조직 국장 : 도적
        /// </summary>
        DirectorOfUnion
    }

    /// <summary>
    /// 착용 장비의 종류
    /// </summary>
    public enum EEquipType : int
    {
        Weapon = 0,
        Armor,
        Helmet,
        Accessary
    }

    public enum EEquipBonus : int
    {
        ATK = 0,    // Attack
        DEF,    // Defense
        HP,     // Hp
        MP      // MP
    }

    /// <summary>
    /// 등급 체계
    /// </summary>
    public enum ERank : int 
    {
        Normal = 0,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    public enum BattleItemType : int
    {
        Potion = 0,
        DamageItem = 1
    }


    public class DataDefinition
    {
        static DataDefinition _instance;

        public Equipment[] Equipments { get; set; }
        public BattleItem[] BattleItems { get; set; }


        private DataDefinition()
        {
            Equipments = new Equipment[] {
                new Equipment("정장", "Manners, Maketh, Man.", 1000, EEquipType.Armor, ERank.Normal, 0f, 10f, 10f, 0f),
                new Equipment("서류 가방", "사실은 총이 들어갑니다.", 2000, EEquipType.Weapon, ERank.Normal, 10f, 0f, 0f, 10f),
            };

            BattleItems = new BattleItem[] {
            };
        }

        public static DataDefinition GetInstance()
        {
            if(_instance == null)
                _instance = new DataDefinition();

            return _instance;
        }

    }
}
