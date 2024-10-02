using Newtonsoft.Json;

namespace project_TextRPG
{
    /// <summary>
    /// 현재 수행 중인 퀘스트를 관리하기 위한 싱글턴
    /// </summary>
    public class QuestManager
    {
        static QuestManager _instance;

        [JsonProperty]
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

        /// <summary>
        /// 퀘스트 수행
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="cnt"></param>
        public void PerformQuest<T>(T target, int cnt)
        {
            foreach (Quest q in Quests)
                q.Perform(target, cnt);
        }

        /// <summary>
        /// 퀘스트 수락
        /// </summary>
        /// <param name="q"></param>
        public void AcceptQuest(Quest q)
        {
            List<Quest> t = Quests.ToList();
            t.Add(q);
            Quests = t.ToArray();
        }

        /// <summary>
        /// 퀘스트 포기 / 거절
        /// </summary>
        /// <param name="q"></param>
        public void RejectQuest(Quest q)
        {
            q.Clear();

            List<Quest> t = Quests.ToList();
            t.Remove(q);
            Quests = t.ToArray();
        }

        /// <summary>
        /// 퀘스트 수행 여부 확인
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsProceedingQuest(Quest q)
        {
            return Quests.Contains(q);
        }

    }
}
