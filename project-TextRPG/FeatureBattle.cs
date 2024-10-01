using static System.Formats.Asn1.AsnWriter;

namespace project_TextRPG
{
    internal class FeatureBattle : Feature
    {
        public FeatureBattle(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;
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
