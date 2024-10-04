namespace project_TextRPG
{
    internal interface IEnhanceable
    {
        public int EnhanceLevel { get; protected set; }
        public float[] Enhancements { get; protected set; }

        public void Enhance(float val);
    }
}
