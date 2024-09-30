namespace project_TextRPG
{
    internal class FeatureStoreSelling : Feature
    {
        public FeatureStoreSelling(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "아이템을 판매할 수 있습니다.\n\n",

                $"[보유 골드]\n{_player.Gold} G\n\n",

                "[아이템 목록]\n"
            );


            Equipment[] e = _player.Inventory.Items;
            for (int i = 0; i < e.Length; i++)
            {
                Utility.ShowScript(
                    $"-{i + 1}. {e[i].Name,-15} | {e[i].GetBonusSpec(),-15} | {e[i].Price * 0.85} G | {e[i].Description}"
                );
            }

            Utility.ShowScript(
                "\n0. 나가기\n"
            );

            Console.WriteLine();
        }

        public override void Act()
        {
            Equipment[] e = _player.Inventory.Items;
            int select = Utility.GetSelection(0, e.Length);
            if (select == 0)
            {
                End();
                return;
            }

            // sell
            // 골드 지급
            _player.Gold += (int)(e[select - 1].Price * 0.85f);
            // 아이템 지불
            _player.Inventory.Remove(e[select - 1]);

            Console.Clear();
            ShowMenu();
            Utility.WriteColorScript("판매가 완료되었습니다.", ConsoleColor.Green);

            Act();
        }
    }
}
