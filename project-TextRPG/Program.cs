namespace project_TextRPG
{
    class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            ChairmanOfUnion player = new ChairmanOfUnion("주인공");
            Battle battle = new Battle(player,1);
            //Character player = null;
            //IScene startScene = new StartScene(COMMON_NAME);
            //startScene.Start(player);
            //player = startScene.End();

            //// 게임 시작
            //IScene gameScene = new GameScene(COMMON_NAME);
            //gameScene.Start(player);
            battle.StartBattle(1);
        }
    }
}
