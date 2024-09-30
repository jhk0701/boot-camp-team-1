namespace project_TextRPG
{
    class FeatureStore : Feature
    {
        Feature[] _subFeatures;
        // 상인의 아이템 목록 : 기본템 : 상시 판매
        // 재구입 목록 - 보관 : 20개까지만

        public FeatureStore(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
            _subFeatures = [
                new FeatureStoreBuying("상점-아이템 구매", _scene),
                new FeatureStoreSelling("상점-아이템 판매", _scene)
            ];
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "아이템을 사고 팔 수 있습니다.\n\n",

                $"[보유 골드]\n{_player.Gold} G\n\n",

                "[아이템 목록]\n"
            );
            
            Equipment[] e = DataDefinition.GetInstance().Equipments;
            for (int i = 0; i < e.Length; i++)
            {
                Utility.ShowScript(
                    $"- {e[i].Name, -15} | {e[i].GetBonusSpec(), -15} | {e[i].Price} G | {e[i].Description}"
                );
            }

            Console.WriteLine();
        }

        public override void Act()
        {
            Utility.ShowScript(
                "1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n"
            );

            int select = Utility.GetSelection(0, 2);
            if (select == 0) 
            {
                End();
                return;
            }

            // 1. 구매 2. 판매
            _subFeatures[select - 1].Start(_player);
        }

    }
}
