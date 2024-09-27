namespace project_TextRPG
{
    class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            Character player;

            // 임시로 전사 생성
            player = new ChairmanOfUnion("Player");

            IScene startScene = new StartScene(COMMON_NAME);
            startScene.Start(player);

            // 게임 시작
            IScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }
    }
}
