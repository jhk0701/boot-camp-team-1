namespace project_TextRPG
{
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


    public partial class DataDefinition
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
