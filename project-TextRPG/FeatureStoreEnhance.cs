namespace project_TextRPG
{
    class FeatureStoreEnhance : Feature
    {
        int enhanceCost = 500;
        int maxEnhanceLevel = 3;
        int[] probabilityOfSuccess;
        float[] valueOfEnhancement;

        public FeatureStoreEnhance(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;

            probabilityOfSuccess = [70, 50, 30];
            valueOfEnhancement = [20f, 20f, 20f];
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "아이템을 강화 수 있습니다.\n\n",

                $"[보유 골드]\n{_player.Gold} G\n\n",

                "[아이템 목록]\n"
            );

            Equipment[] e = _player.Inventory.Items;
            for (int i = 0; i < e.Length; i++)
            {
                Utility.ShowScript(
                    $"-{i + 1}. ",
                    e[i].GetDesc(
                        _player.Inventory.IsEquipped(e[i]), 
                        e[i].EnhanceLevel >= maxEnhanceLevel ? 0 : (e[i].EnhanceLevel + 1) * enhanceCost
                    )
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
                // 나가기
                End();
                return;
            }

            select--;

            int cost = (e[select].EnhanceLevel + 1) * enhanceCost;

            // 강화 불가
            if (e[select].EnhanceLevel >= maxEnhanceLevel)
            { 
                // 강화 레벨 제한
                Utility.WriteColorScript("강화가 완료된 장비입니다.", ConsoleColor.Red);
                Act();
                return;
            }
            else if (_player.Gold < cost)
            {
                // 골드 부족
                Utility.WriteColorScript("골드가 부족합니다.", ConsoleColor.Red);
                Act();
                return;
            }

            // 강화
            int p = GetRandom();
            if (p < probabilityOfSuccess[e[select].EnhanceLevel])
            {
                bool isEquipped = _player.Inventory.IsEquipped(e[select]);
                if (isEquipped)
                    _player.Inventory.Unequip(e[select]);

                // 강화 성공
                ShowSuccessResult(e[select]);

                e[select].Enhance(valueOfEnhancement[e[select].EnhanceLevel]);

                if (isEquipped)
                    _player.Inventory.Equip(e[select]);

                Utility.GetSelection(0, 0); // 돌아가기                
            }
            else
            {
                // 강화 실패
                ShowFailResult();
                Utility.GetSelection(0, 0); // 돌아가기
            }

            Console.Clear();
            ShowMenu();
            Act();
        }

        int GetRandom()
        {
            Random r = new Random();
            return r.Next(1, 101);
        }
        
        void ShowSuccessResult(Equipment e)
        {
            float curEnhance = e.Enhancements.Sum();

            Console.Clear();
            Utility.WriteColorScript("강화 성공", ConsoleColor.Green);
            Utility.ShowScript(
                "아이템 강화가 성공했습니다!\n\n",
                "[강화]\n",
                $"{e.Name} +{e.EnhanceLevel} -> +{e.EnhanceLevel + 1}\n",
                $"성능 증가 + {curEnhance} -> + {curEnhance + valueOfEnhancement[e.EnhanceLevel]} %\n\n",

                "0. 돌아가기\n"
            );
        }

        void ShowFailResult()
        {
            Console.Clear();
            Utility.WriteColorScript("강화 실패", ConsoleColor.Red);
            Utility.ShowScript(
                "아이템 강화에 실패했습니다!\n\n",
                
                "0. 돌아가기\n"
            );
        }
    }
}
