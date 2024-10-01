namespace project_TextRPG
{
    internal class FeatureQuest : Feature
    {
        public FeatureQuest(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void ShowMenu()
        {
            Quest[] q = DataDefinition.GetInstance().QuestList;

            Utility.WriteColorScript($"{_name}!!\n\n", ConsoleColor.Yellow);
            for (int i = 0; i < q.Length; i++)
            {
                Utility.ShowScript(
                    $"{i + 1}. ",
                    q[i].Title
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
            ShowQuest(q[select]);

            select = Utility.GetSelection(1, 2);
            // 수락 거절
        }

        void ShowQuest(Quest q)
        {
            Utility.WriteColorScript($"{_name}!!\n\n", ConsoleColor.Yellow);
            Utility.ShowScript(
                $"{q.Title}\n\n",

                $"{q.Description}\n\n",

                ""// 내용
            );

        }
    }
}
