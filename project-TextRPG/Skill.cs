namespace project_TextRPG
{
    public class Skill
    {
        public string Name { get; protected set; }
        public float[] PowerRange { get; protected set; }
        public int RequiredLevel { get; protected set; }
        public float RequiredMana { get; protected set; }
        

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
