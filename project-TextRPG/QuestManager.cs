namespace project_TextRPG
{
    public class QuestManager
    {
        static QuestManager _instance;

        public Quest[] Quests { get; protected set; }


        private QuestManager() 
        {
            Quests = new Quest[0];
        }

        public static QuestManager GetInstance()
        {
            if (_instance == null)
                _instance = new QuestManager();

            return _instance;
        }


        public void PerformQuest<T>(T target, int cnt)
        {
            foreach (Quest q in Quests)
                q.Perform(target, cnt);
        }

        public void AcceptQuest(Quest q)
        {
            List<Quest> t = Quests.ToList();
            t.Add(q);
            Quests = t.ToArray();
        }

        public void RejectQuest(Quest q)
        {
            q.Clear();

            List<Quest> t = Quests.ToList();
            t.Remove(q);
            Quests = t.ToArray();
        }

        public bool IsProceedingQuest(Quest q)
        {
            return Quests.Contains(q);
        }

    }
}
