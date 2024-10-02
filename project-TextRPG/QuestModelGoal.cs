namespace project_TextRPG
{
    internal class QuestModelGoal : Quest
    {
        ERequestGoal _goal;

        public QuestModelGoal(ERequestGoal target, int targetCount, string title, string desc, QuestReward reward, bool isRepeatable = true)
        {
            _goal = target;
            Title = title;
            Description = desc;
            TargetCount = targetCount;
            Reward = reward;
            IsRepeatable = isRepeatable;
        }

        public override void Perform<T>(T target, int cnt)
        {
            switch (_goal)
            {
                case ERequestGoal.WinBattle :
                    if (target is Battle)
                        Count++;
                    break;
                case ERequestGoal.LevelUp :
                    if (target is Character)
                        Count = cnt;
                    break;
                case ERequestGoal.EnhanceItem :
                    if (target is FeatureStoreEnhance)
                        Count++;
                    break;
            }

            //if ((target as Monster).Id == _targetMonsterId)
            //    Count += cnt;
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
            // 보상 지급

            player.Gold += Reward.gold;
            player.Exp += Reward.exp;

            foreach (int eId in Reward.equips)
                player.Inventory.Add(DataDefinition.GetInstance().Equipments[eId].Copy());

            QuestManager.GetInstance().RejectQuest(this);

        }

        public override string GetRequestDesc()
        {
            string name = "";
            switch (_goal)
            {
                case ERequestGoal.WinBattle:
                    name = "전투 승리";
                    break;
                case ERequestGoal.LevelUp:
                    name = "레벨업";
                    break;
                case ERequestGoal.EnhanceItem:
                    name = "아이템 강화";
                    break;
            }
            //string name = DataDefinition.GetInstance().Monsters.Where(n => n.Id.Equals(_targetMonsterId)).First().Name;
            return $"{name} {TargetCount}회 수행 ({Count} / {TargetCount})";
        }
    }
}
