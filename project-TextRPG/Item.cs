namespace project_TextRPG
{
    public class Item
    {
        public string Name { get; set; }
        public float Spec { get; set; }
        public ERank eRank { get; set; }

        public Item() : this("Unknown", 0f) { }

        public Item(string n, float spec)
        {
            Name = n;
            Spec = spec;
        }
    }
}
