namespace project_TextRPG
{
    public abstract class Quest
    {
        public string Title { get; protected set; }
        
        public string Description { get; protected set; }

        public int TargetCount { get; protected set; }
        
        public int Count { get; set; }

        protected abstract void Perform<T>(T target, int cnt);
        protected abstract void Clear();
    }
}
