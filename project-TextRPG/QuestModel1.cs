namespace project_TextRPG
{
    internal class QuestModel1 : Quest
    {
        protected override void Perform<T>(T target, int cnt)
        {
            if(target is Monster && (target as Monster).Id == 0)
                Count += cnt;

            if (Count >= TargetCount)
                Clear();
        }

        public void Perform(Monster mon, int cnt)
        {
            Perform<Monster>(mon, cnt);
        }

        protected override void Clear()
        {
            // Quest Clear
        }
    }
}
