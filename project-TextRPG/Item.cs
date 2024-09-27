namespace project_TextRPG
{
    public partial class DataDefinition
    {
        public class Item
        {
            public string Name { get; set; }
            public float Spec { get; set; }

            public Item(string n, float spec)
            {
                Name = n;
                Spec = spec;
            }
        }

    }
}
