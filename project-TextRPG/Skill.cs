namespace project_TextRPG
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float MinPowerRange { get; set; }
        public float MaxPowerRange { get; set; }
        public int RequiredLevel { get; set; }
        public float RequiredMana { get; set; }
        

        public Skill(string name,string description, float minpower, float maxpower, int requiredLv, float requiredMp)
        {
            Name = name;
            Description = description;
            MinPowerRange = minpower;
            MaxPowerRange = maxpower;
            RequiredLevel = requiredLv;
            RequiredMana = requiredMp;
        }

        public virtual void Use(Unit[] targets) { }
    }
}
