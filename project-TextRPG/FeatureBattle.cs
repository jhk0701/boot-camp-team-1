using static System.Formats.Asn1.AsnWriter;

namespace project_TextRPG
{
    internal class FeatureBattle : Feature
    {
        public override string Name 
        {
            get { return $"{base.Name} (현재 진행: {_player.StageScore}층)"; } 
            set => base.Name = value; 
        }

        public FeatureBattle(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;
        }

        void BattleExample()
        {
            ChairmanOfUnion player = new ChairmanOfUnion("주인공");
            Battle battle = new Battle(player, 1, _scene);

            battle.StartBattle(1);
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(_name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "들어가려는 층을 선택하실 수 있습니다.\n\n",
                $"[현재 진행 : {_player.StageScore} 층]\n"
            );

            for (int i = 0; i < _player.StageScore; i++)
            {
                Utility.ShowScript(
                    $"{i + 1}. {i + 1}층 선택"
                );
            }

            Console.WriteLine();

            Utility.ShowScript("\n0. 나가기\n");
        }

        public override void Act()
        {
            int select = Utility.GetSelection(0, _player.StageScore);
            if (select == 0) 
            {
                End();
                return;
            }

            Battle battle = new Battle(_player, select, _scene);
            battle.StartBattle(select);
        }
    }
}
