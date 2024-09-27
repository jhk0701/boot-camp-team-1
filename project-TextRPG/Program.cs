namespace project_TextRPG
{
    class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            Character player;

            // 캐릭터 생성부분
            // 1. 데이터 있으면 로드
            // 2. 데이터 없으면 생성

            // 임시로 전사 생성
            player = new ChairmanOfUnion("Player");

            // 게임 시작
            GameScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }
    }
}
