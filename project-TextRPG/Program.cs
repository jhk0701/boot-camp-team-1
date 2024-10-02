using System.Text.Json;
using System.Linq;

namespace project_TextRPG
{
    public class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            //DataIO.GetInstance().Load();
            //Character t = DataIO.GetInstance().GetLoadedData().Player;
            //CheckData(t);

            //return;

            Character player = null;
            IScene startScene = new StartScene(COMMON_NAME/*, true*/);
            startScene.Start(player);
            player = startScene.End();
            player.Gold += 500;

            // 게임 시작
            IScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }

        static void CheckData(Character t)
        {
            Console.WriteLine(t.Name);
            Console.WriteLine(t.CharClass.ToString());
            Console.WriteLine($"Inventory.Items : {t.Inventory.Items.Length}");
            Console.WriteLine($"Inventory.Equipments : {t.Inventory.Equipments.Count}");
            
            Console.WriteLine($"BasicAttack : {t.BasicAttack}");
            Console.WriteLine($"BasicDefense : {t.BasicDefense}");
            Console.WriteLine($"MaxHealth : {t.MaxHealth}");
            Console.WriteLine($"Health : {t.Health}");
            Console.WriteLine($"MaxMana : {t.MaxMana}");
            Console.WriteLine($"Mana : {t.Mana}");

            Console.WriteLine($"Skills : {t.Skills.Length}");
        }
    }
}
