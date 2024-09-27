
using System.Threading;

namespace project_TextRPG
{
    class Battle
    {
        Character Player;
        List<Monster> Monsters;

        Battle (Character player, int dungeonid)
        {
            Player = player;

            Monsters = new List<Monster> ();

            List<Monster> CreateMonsters(int dungeonId)
            {
                switch (dungeonId)
                {
                    case 1:
                        for (int i = 0; i < 3; i++)
                        {
                            Monsters.Add(new Goblin("고블린"));
                        }
                        for (int i = 0; i < 1; i++)
                        {
                            Monsters.Add(new Orc("오크"));
                        }
                        break;

                    case 2:
                        for (int i = 0; i < 10; i++)
                        {
                            Monsters.Add(new Orc("오크"));
                        }
                        break;

                    case 3:
                        for (int i = 0; i < 10; i++)
                        {
                            Monsters.Add(new Slime("슬라임"));
                        }
                        break;
                    default:
                        Console.WriteLine($"유효하지 않은 던전 ID: {dungeonId}");
                        break;
                }
                return Monsters;
            }
        }

        void DeadWriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        void ShowAttackresult(Monster selectedMonster)
        {
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("{0} 의 공격!", Player.Name);
            Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n",
                selectedMonster.Level, selectedMonster.Name, Player.Attack);
            Console.WriteLine("LV.{0} {1}", selectedMonster.Level, selectedMonster.Name);
            if (selectedMonster.Health <= 0)
            {
                Console.WriteLine("{0} -> Dead\n", selectedMonster.Health);
            }
            else
            {
                Console.WriteLine("{0} -> {1}\n", selectedMonster.Health, selectedMonster.Health - Player.Attack);
            }
            selectedMonster.TakeDamage(Player.Attack);
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
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("Lv.{0} {1} HP {2}", monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    DeadWriteLine($"Lv.{monster.Level} {monster.Name} Dead");
                }
            }
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}\n", Player.Health, Player.MaxHealth);
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
                    else if (choice > 0 && choice <= Monsters.Count)
                    {
                        var selectedMonster = Monsters[choice - 1];

                        if (selectedMonster != null && selectedMonster.isDead)
                        {
                            Console.WriteLine("잘못된 입력입니다.");
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
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("Lv.{0} {1} HP {2}", monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    DeadWriteLine($"Lv.{monster.Level} {monster.Name} Dead");
                }
            }
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}\n", Player.Health, Player.MaxHealth);
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
