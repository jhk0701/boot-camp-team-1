using System.Numerics;

namespace project_TextRPG
{
    internal class FeatureQuest : Feature
    {
        public FeatureQuest(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;
        }

        public override void ShowMenu()
        {
            Quest[] q = DataDefinition.GetInstance().QuestList;

            Utility.WriteColorScript($"{Name}!!\n\n", ConsoleColor.Yellow);
            for (int i = 0; i < q.Length; i++)
            {
                Utility.ShowScript(
                    $"{i + 1}. ",
                    q[i].Title,

                    _player.IsProceedingQuest(q[i]) ? " (진행중)": ""
                );
            }

            Utility.ShowScript(
                "\n0. 나가기\n"
            );
        }

        public override void Act()
        {
            Quest[] q = DataDefinition.GetInstance().QuestList;
            int select = Utility.GetSelection(0, q.Length, "원하시는 퀘스트를 선택해주세요.");

            if(select == 0) // 나가기
            {
                End();
                return;
            }

            select--;

            // 퀘스트 선택
            Quest selectedQuest = q[select];
            bool isProceeding = _player.IsProceedingQuest(selectedQuest);

            ShowQuest(selectedQuest, isProceeding);
            select = Utility.GetSelection(0, isProceeding ? 2 : 1);

            if(select == 0)
            {
                End();
                return;
            }

            if (!isProceeding)
            {
                // 진행중이지 않을 때는 수락
                _player.AcceptQuest(selectedQuest);
                Utility.WriteColorScript("퀘스트를 수락했습니다.\n", ConsoleColor.Green);
            }
            else if (select == 1)
            {
                // 기존 진행 중일때는 퀘스트 포기
                _player.RejectQuest(selectedQuest);
                Utility.WriteColorScript("퀘스트를 포기했습니다.\n", ConsoleColor.Red);
            }
            else if (select == 2) 
            {
                // 완수하기
                if (selectedQuest.IsCompletable())
                {
                    Utility.WriteColorScript("퀘스트를 완료했습니다!\n", ConsoleColor.Green);
                    selectedQuest.Complete(_player);
                }   
                else
                    Utility.WriteColorScript("조건이 충족되지 않았습니다!\n", ConsoleColor.Red);
            }

            Utility.ShowScript("0. 돌아가기\n");
            Utility.GetSelection(0, 0);

            End();
            return;
        }

        void ShowQuest(Quest q, bool isProceed)
        {
            Console.Clear();
            Utility.WriteColorScript($"{Name}!!\n\n", ConsoleColor.Yellow);
            Utility.ShowScript(
                $"{q.Title}\n\n",

                $"{q.Description}\n\n",

                $"{q.GetRequestDesc()}\n\n", // 내용
                "- 보상 - \n",
                $"{q.GetRewardDesc()}\n",

                !isProceed ? "1. 수락\n0. 나가기\n" : "1. 포기\n2. 완료하기\n0. 나가기\n"
            );

        }
    }
}
