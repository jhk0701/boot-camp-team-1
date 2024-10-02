using Newtonsoft.Json;

namespace project_TextRPG
{
    /// <summary>
    /// 현재 수행 중인 퀘스트를 관리하기 위한 싱글턴
    /// </summary>
    public class QuestManager
    {
        static QuestManager _instance;

        //public Quest[] Quests { get; protected set; }
        public Dictionary<int, int> Quests { get; protected set; }

        private QuestManager() 
        {
            Quests = new Dictionary<int, int>();
        }

        public static QuestManager GetInstance()
        {
            if (_instance == null)
                _instance = new QuestManager();

            return _instance;
        }

        public void Load(Dictionary<int, int> data)
        {
            Quests = data;
        }

        /// <summary>
        /// 퀘스트 수행
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="cnt"></param>
        public void PerformQuest<T>(T target, int cnt)
        {
            foreach (KeyValuePair<int, int> q in Quests)
            {
                Quest quest = DataDefinition.GetInstance().QuestList[q.Key];
                quest.Count = q.Value;

                int cur = quest.Perform(target, cnt);
                if (cur >= 0)
                    Quests[q.Key] = cur;
            }
        }

        /// <summary>
        /// 퀘스트 수락
        /// </summary>
        /// <param name="q"></param>
        public void AcceptQuest(Quest q)
        {
            Quests.Add(q.Id, 0);
        }

        /// <summary>
        /// 퀘스트 포기 / 거절
        /// </summary>
        /// <param name="q"></param>
        public void RejectQuest(Quest q)
        {
            q.Clear();

            Quests.Remove(q.Id);;
        }

        /// <summary>
        /// 퀘스트 수행 여부 확인
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsProceedingQuest(Quest q)
        {
            return Quests.ContainsKey(q.Id);
        }

    }
}
