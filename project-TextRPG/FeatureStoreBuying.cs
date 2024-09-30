namespace project_TextRPG
{
    internal class FeatureStoreBuying : Feature
    {
        public FeatureStoreBuying(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "아이템을 구매할 수 있습니다.\n\n",

                $"[보유 골드]\n{_player.Gold} G\n\n",

                "[아이템 목록]\n"
            );


            Equipment[] e = DataDefinition.GetInstance().Equipments;
            for (int i = 0; i < e.Length; i++)
            {
                Utility.ShowScript(
                    $"-{i + 1}. {e[i].Name,-15} | {e[i].GetBonusSpec(),-15} | {e[i].Price} G ",
                    _player.Inventory.HasItem(e[i]) ? "(보유중) ":"",
                    $"| {e[i].Description}"
                );
            }

            Utility.ShowScript(
                "\n0. 나가기\n"
            );

            Console.WriteLine();
        }

        public override void Act()
        {
            Equipment[] e = DataDefinition.GetInstance().Equipments;
            int select = Utility.GetSelection(0, e.Length);
            if (select == 0)
            {
                End();
                return;
            }

            // buy
            // 구매불가
            if (_player.Gold <= e[select - 1].Price)
            {
                Utility.WriteColorScript("골드가 부족합니다.", ConsoleColor.Red);
                Act();
                return;
            }

            // 구매
            // 골드 지불
            _player.Gold -= e[select - 1].Price;
            // 아이템 지급
            _player.Inventory.Add(e[select - 1]);

            Console.Clear();
            ShowMenu();
            Utility.WriteColorScript("구매가 완료되었습니다.", ConsoleColor.Green);

            Act();
        }
    }
}
