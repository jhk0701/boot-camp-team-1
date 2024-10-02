namespace project_TextRPG
{
    public class Skill
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public float MinPowerRange { get; protected set; }
        public float MaxPowerRange { get; protected set; }
        public int RequiredLevel { get; protected set; }
        public float RequiredMana { get; protected set; }
        

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
