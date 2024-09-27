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
        Armor
    }

    /// <summary>
    /// 등급 체계
    /// </summary>
    public enum ERank : int 
    {
        Normal = 0,
        Rare,
        Heroic,
        Legendary,
        Mythic
    }

    public class DataDefinition
    {

        static DataDefinition _instance;

        public Item[] Items { get; set; }

        private DataDefinition()
        {
            Items = new Item[] {
                new Item("Item 1", 10f),
                new Item("Item 2", 10f),
                new Item("Item 3", 10f),
                new Item("Item 4", 10f),
                new Item("Item 5", 10f),
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
