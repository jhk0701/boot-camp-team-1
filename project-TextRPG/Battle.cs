
namespace project_TextRPG
{
    public class Battle
    {
        public TestPlayer TestPlayer;
        Battle (TestPlayer player)
        {
            TestPlayer = player;
        }
        void DeadMonster(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        void ShowAttackresult(Monster selectedMonster)
        {
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("{0} 의 공격!", TestPlayer.Name);
            Console.WriteLine("{선택몬스터.레벨} {선택몬스터.이름} 을(를) 맞췄습니다. [데미지 : {선택몬스터.데미지}]\n");
            Console.WriteLine("{선택몬스터.레벨} {선택몬스터.이름}");
            if (selectedMonster.Health <= 0)
            {
                Console.WriteLine("{선택몬스터.체력} -> Dead\n");
            }
            else
            {
                Console.WriteLine("{선택몬스터.체력} -> {선택몬스터.체력 - 플레이어 데미지}\n");
            }
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

        void ShowAttackList()
        {

            Console.WriteLine("Battle!!\n");
            Console.WriteLine("1 Lv.2 미니언  HP 15");
            Console.WriteLine("2 Lv.5 대포미니언 HP 25");
            Console.WriteLine("3 LV.3 공허충 HP 10\r\n");
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", TestPlayer.Level, TestPlayer.Name, TestPlayer.Class);
            Console.WriteLine("HP {0}/{1}\n", TestPlayer.Health, TestPlayer.Maxhealth);
            Console.WriteLine("1. 공격\n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        return;
                    }
                    else if (choice > 0 && choice <= Monsters.count)
                    {
                        var selectedMonster = Monsters.count[choice - 1];

                        if (selectedMonster != null && selectedMonster.isDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                        else
                        {
                            selectedMonster.GetHIT();
                        }

                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

        void StartBattle(int floor)
        {
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("Lv.2 미니언  HP 15");
            Console.WriteLine("Lv.5 대포미니언 HP 25");
            Console.WriteLine("LV.3 공허충 HP 10\r\n");
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})",TestPlayer.Level, TestPlayer.Name, TestPlayer.Class);
            Console.WriteLine("HP {0}/{1}\n",TestPlayer.Health,TestPlayer.Maxhealth);
            Console.WriteLine("1. 공격\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        return;
                    }
                    else if (choice == 1)
                    {
                        ShowAttackList();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

    }
}
