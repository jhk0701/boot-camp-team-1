namespace project_TextRPG
{
    class Skill
    {
        public string Name { get; set; }
        public float[] PowerRange { get; set; }
        public int RequiredLevel { get; set; }
        public float RequiredMana { get; set; }
        

        public Skill(string name, float[] power, int requiredLv, float requiredMp)
        {
            Name = name;
            PowerRange = power;
            RequiredLevel = requiredLv;
            RequiredMana = requiredMp;
        }

        public virtual void Use(Unit[] targets) { }
    }
}
