namespace project_TextRPG
{
    internal class FeatureRest : Feature
    {

        const int COST = 500;

        public FeatureRest(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                $"{COST} G를 지불하고 캐릭터를 회복시킬 수 있습니다.\n\n",

                $"[보유 골드]\n{_player.Gold} G\n\n",

                "[현재 상태]",
                $"Lv. {_player.Level} {_player.Name}\n",
                $"Hp {_player.Health}\n\n",

                "1. 회복하기\n",
                "0. 나가기\n"
            );

            Console.WriteLine();
        }

        public override void Act()
        {
            int select = Utility.GetSelection(0, 1);

            if (select == 0) 
            {
                End();
                return;
            }

            if(COST > _player.Gold)
            {
                Utility.WriteColorScript("골드가 부족합니다.\n", ConsoleColor.Red);
                Act();
                return;
            }

            Utility.ShowScript(
                $"{COST} G를 지불하고 캐릭터를 회복시켰습니다.\n\n",

                $"Lv. {_player.Level} {_player.Name}\n",
                $"Hp {_player.Health} -> {_player.MaxHealth}\n",
                $"Mp {_player.Mana} -> {_player.MaxMana}\n",
                $"보유 골드 {_player.Gold} -> {_player.Gold - COST}\n\n",

                "0. 나가기\n"
            );
            _player.Gold -= COST;
            _player.Rest();

            Utility.GetSelection(0, 0);
            End();
        }
    }
}
