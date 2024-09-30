
using System.Threading;

namespace project_TextRPG
{
    class Battle
    {
        IScene _scene;

        Character Player;
        List<Monster> Monsters;

        int killcount;
        float TrueDamage;
        int Floar;

        Random ran;
        public Battle(Character player, int dungeonid, IScene scene)
        {
            Player = player;

            Monsters = new List<Monster>();

            ran = new Random();

            Monsters = CreateMonsters(dungeonid);
            Monsters = ShuffleMonsters(Monsters);
            _scene = scene;
        }

        float GetTrueDamage(float damage)
        {
            float RandomDamageRange = damage / 10;
            if (RandomDamageRange % 1 != 0)
            {
                RandomDamageRange += 1 - RandomDamageRange % 1;
            }

            damage = ran.Next((int)(damage - RandomDamageRange), (int)(damage + RandomDamageRange));
            return damage;
        }

        List<Monster> ShuffleMonsters(List<Monster> values)
        {
            Random rand1 = new Random();
            int randMonsterCount = rand1.Next(1, 5);


            Random rand2 = new Random();
            var shuffled = values.OrderBy(_ => rand2.Next()).ToList();

            while (shuffled.Count > randMonsterCount)
            {
                shuffled.RemoveAt(shuffled.Count - 1);
            }

            return shuffled;
        }

        List<Monster> CreateMonsters(int dungeonId)
        {
            switch (dungeonId)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        Monsters.Add(new Goblin("고블린"));
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Monsters.Add(new Orc("오크"));
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Monsters.Add(new Orc("슬라임"));
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
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    break;
            }
            return Monsters;
        }

        void DeadWriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        void ShowEnemyPhase(Monster selectedMonster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("LV.{0} {1} 의 공격!", selectedMonster.Level, selectedMonster.Name);
            Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]\n",
                Player.Name, selectedMonster.Attack - Player.Defense);
            Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
            if (Player.Health - (selectedMonster.Attack - Player.Defense) <= 0)
            {
                Console.WriteLine("{0} -> Dead\n", Player.Health);
                Player.isDead = true;
            }
            else
            {
                Console.WriteLine("{0} -> {1}\n", Player.Health, Player.Health - (selectedMonster.Attack - Player.Defense));
            }
            Console.WriteLine();
            Player.TakeDamage(selectedMonster.Attack - Player.Defense);
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        if(Player.isDead)
                        {
                            ShowLose();
                        }
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

        void ShowVictory()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", killcount);
            Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
            Console.WriteLine("{0} -> {1}\n", Player.MaxHealth,Player.Health);
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        _scene.Return();
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

        void ShowLose()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
            Console.WriteLine("{0} -> {1}\n", Player.MaxHealth, Player.Health);
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        _scene.Return();
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

        void ShowAttackresult(Monster selectedMonster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("{0} 의 공격!", Player.Name);
            Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n",
                selectedMonster.Level, selectedMonster.Name, TrueDamage - selectedMonster.Defense);
            Console.WriteLine("LV.{0} {1}", selectedMonster.Level, selectedMonster.Name);
            if (selectedMonster.Health - (TrueDamage - selectedMonster.Defense) <= 0)
            {
                Console.WriteLine("{0} -> Dead\n", selectedMonster.Health);
                selectedMonster.isDead = true;
                killcount++;
            }
            else
            {
                Console.WriteLine("{0} -> {1}\n", selectedMonster.Health, selectedMonster.Health - (TrueDamage - selectedMonster.Defense));
            }
            Console.WriteLine();
            selectedMonster.TakeDamage(TrueDamage - selectedMonster.Defense);
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        if (killcount == Monsters.Count)
                        {
                            ShowVictory();
                        }
                        else
                        {
                            foreach (var monster in Monsters)
                            {
                                if (!monster.isDead)
                                {
                                    ShowEnemyPhase(monster);
                                }
                            }
                        }
                        StartBattle(Floar);
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
            int n = 1;
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("{0} - Lv.{1} {2} HP {3}", n, monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    DeadWriteLine($"{n} - Lv.{monster.Level} {monster.Name} Dead");
                }
                n++;
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}\n", Player.Health, Player.MaxHealth);
            Console.WriteLine("0. 취소\n");
            Console.WriteLine("대상을 선택해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        StartBattle(Floar);
                        return;
                    }
                    else if (choice > 0 && choice <= Monsters.Count)
                    {
                        var selectedMonster = Monsters[choice - 1];

                        if (selectedMonster != null && selectedMonster.isDead)
                        {
                            Console.WriteLine("이미 죽은 대상입니다.");
                        }
                        else
                        {
                            TrueDamage = GetTrueDamage(Player.Attack);
                            ShowAttackresult(selectedMonster);
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

        public void StartBattle(int dungeonid)
        {
            Floar = dungeonid;

            Console.Clear();
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
            Console.WriteLine();
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
