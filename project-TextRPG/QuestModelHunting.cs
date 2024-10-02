using System.Text;

namespace project_TextRPG
{
    internal class QuestModelHunting : Quest
    {
        int _targetMonsterId;

        public QuestModelHunting(int targetId, int targetCount,string title, string desc, QuestReward reward, bool isRepeatable = true)
        {
            _targetMonsterId = targetId;
            Title = title;
            Description = desc;
            TargetCount = targetCount;
            Reward = reward;
            IsRepeatable = isRepeatable;
        }

        public override void Perform<T>(T target, int cnt)
        {
            if (!(target is Monster)) 
                return;

            if ((target as Monster).Id == _targetMonsterId)
                Count += cnt;
        }

        public override void Clear()
        {
            Count = 0;
        }

        public override bool IsCompletable()
        {
            return Count >= TargetCount;
        }

        public override void Complete(Character player)
        {
            //보상 지급
            player.Gold += Reward.gold;
            player.Exp += Reward.exp;

            foreach (int eId in Reward.equips)
                player.Inventory.Add(DataDefinition.GetInstance().Equipments[eId].Copy());

            QuestManager.GetInstance().RejectQuest(this);
        }

        public override string GetRequestDesc()
        {
            string name = DataDefinition.GetInstance().Monsters.Where(n => n.Id.Equals(_targetMonsterId)).First().Name;
            return $"{name} {TargetCount}마리 처치 ({Count} / {TargetCount})";
        }

    }
}
