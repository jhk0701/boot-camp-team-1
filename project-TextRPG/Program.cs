using System.Text.Json;

namespace project_TextRPG
{
    class Program
    {
        static string saveFilePath = "saveData.json";

        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            Character player = null;
            IScene startScene = new StartScene(COMMON_NAME, true);
            startScene.Start(player);
            player = startScene.End();
            player.Gold += 10000;

            // 게임 시작
            IScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }


        static void SaveGame()
        {
           // string jsonString = JsonSerializer.Serialize(playerCharacter);
           // File.WriteAllText(saveFilePath, jsonString);
           // Console.WriteLine("게임이 저장되었습니다.");
        }

    }
}
