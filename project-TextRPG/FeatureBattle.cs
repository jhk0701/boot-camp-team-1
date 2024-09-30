using static System.Formats.Asn1.AsnWriter;

namespace project_TextRPG
{
    internal class FeatureBattle : Feature
    {
        public FeatureBattle(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void Start(Character player)
        {
            _player = player;


            Battle battle = new Battle(player, 1, _scene);
            battle.StartBattle(1);
        }

        void BattleExample()
        {
            ChairmanOfUnion player = new ChairmanOfUnion("주인공");
            Battle battle = new Battle(player, 1, _scene);
            //Character player = null;
            //IScene startScene = new StartScene(COMMON_NAME);
            //startScene.Start(player);
            //player = startScene.End();

            //// 게임 시작
            //IScene gameScene = new GameScene(COMMON_NAME);
            //gameScene.Start(player);
            battle.StartBattle(1);
        }

        public override void ShowMenu()
        {

        }

        public override void Act()
        {

        }
    }
}
