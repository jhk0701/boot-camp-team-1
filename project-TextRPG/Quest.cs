using System.Text;

namespace project_TextRPG
{
    public abstract class Quest
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public bool IsRepeatable { get; protected set; }

        public int TargetCount { get; protected set; }
        public int Count { get; set; }

        public QuestReward Reward { get; protected set; }


        /// <summary>
        /// 퀘스트 수행 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="cnt"></param>
        public abstract void Perform<T>(T target, int cnt);

        /// <summary>
        /// 퀘스트 초기화 함수
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 퀘스트 완수 함수
        /// </summary>
        public abstract void Complete(Character player);

        /// <summary>
        /// 퀘스트 완료 가능 표시
        /// </summary>
        /// <returns></returns>
        public abstract bool IsCompletable();

        /// <summary>
        /// 퀘스트 요구사항 출력 함수
        /// </summary>
        /// <returns></returns>
        public abstract string GetRequestDesc();

        /// <summary>
        /// 퀘스트 보상 출력 함수
        /// </summary>
        /// <returns></returns>
        public virtual string GetRewardDesc()
        {
            StringBuilder sb = new StringBuilder();
            
            if(Reward.gold > 0)
                sb.Append($"{Reward.gold} G\n");
            if(Reward.exp > 0)
                sb.Append($"{Reward.exp} EXP\n");

            for (int i = 0; i < Reward.equips.Length; i++)
                sb.Append($"{DataDefinition.GetInstance().Equipments[Reward.equips[i]].Name} x 1\n");

            int[] keys = Reward.heals.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
                sb.Append($"{DataDefinition.GetInstance().HealItems[keys[i]].Name} x {Reward.heals[keys[i]]}\n");

            return sb.ToString();
        }
        
    }
}