
using System.Reflection.Emit;
using System.Threading;
using System.Linq;

namespace project_TextRPG
{
    class Battle
    {
        IScene _scene;

        Character Player;
        List<Monster> Monsters;

        int killcount;       //�ش� ������ �÷��̾ ���� ������ ����
        float TrueDamage;    //�÷��̾� �������� 10% ������ ����� ��
        int Floar;           //���� ����
        bool CriticalHit;    //ġ��Ÿ ���� �ߵ�����
        bool Dodge;          //ȸ�Ǽ��� ����

        float Defaultlevel;  //�÷��̾��� �������Դ���� ���� (�¸�ȭ�鿡�� �񱳿�)
        float DefaultHp;     //�÷��̾��� �������Դ���� ü�� (�¸�ȭ�鿡�� �񱳿�)
        float DefaultExp;    //�÷��̾��� �������Դ���� ����ġ (�¸�ȭ�鿡�� �񱳿�)

        Random ran;
        public Battle(Character player, int dungeonid, IScene scene)
        {
            Player = player;

            Monsters = new List<Monster>();

            ran = new Random();

            Monsters = CreateMonsters(dungeonid);
            Monsters = ShuffleMonsters(Monsters);

            Defaultlevel = player.Level;
            DefaultHp = player.Health;
            DefaultExp = player.Exp;

            _scene = scene;

        }

        // 10% Ȯ���� �߻��ϴ� ȸ�� ����
        void dodgecalcalculator()
        {
            int rolldice = ran.Next(1, 101);
            Dodge = false;

            if (rolldice <= 10)
            {
                Dodge = true;
            }
        }


        // 15% Ȯ���� �߻��ϴ� ġ��Ÿ ����
        void Criticalcalculator(float damage)
        {
            int rolldice = ran.Next(1, 101);
            CriticalHit = false;

            if (rolldice <= 15)
            {
                damage = damage * 1.6f;
                CriticalHit = true;
            }
        }

        // �÷��̾� ���������� 10% �� ����� �ٽ� �������� �Լ�
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


        // ���� ����������, ���� ������ ���������� �������� �������ִ� �Լ�
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

        
        // ���� ������ ���� Monsters����Ʈ�� ���͸� �������ִ� �Լ�
        List<Monster> CreateMonsters(int dungeonId)

        {
            DataDefinition data = DataDefinition.GetInstance(); // DataDefinition �ν��Ͻ� ��������
            List<Monster> monsters = new List<Monster>();
            if (dungeonId == 1)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        Monsters.Add(DataDefinition.GetInstance().Monsters[0].Copy());
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Monsters.Add(DataDefinition.GetInstance().Monsters[1].Copy());
                    }
                    break;

                case 2:
                    for (int i = 0; i < 10; i++)
                    {
                        Monsters.Add(DataDefinition.GetInstance().Monsters[1].Copy());
                    }
                    break;

                case 3:
                    for (int i = 0; i < 10; i++)
                    {
                        Monsters.Add(new Slime("������"));
                    }
                    break;
                default:
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    break;
            }
            else if (dungeonId == 2)
            {
                // 2�������� ȯ����������, ���޷���, �λ������� �߰� ����
                monsters.Add(data.Monsters[2]); // ȯ����������
                monsters.Add(data.Monsters[3]); // ���޷���
                monsters.Add(data.Monsters[4]); // �λ�������
            }
            else if (dungeonId == 3)
            {
                // 3�������� �뵿������, ���ΰ� �߰� ����
                monsters.Add(data.Monsters[5]); // �뵿������
                monsters.Add(data.Monsters[6]); // ���ΰ�
            }
            else if (dungeonId == 4)
            {
                // 4�������� �ذ��Ǳ׸��� �߰� ����
                monsters.Add(data.Monsters[7]); // �ذ��Ǳ׸���
            }
            else if (dungeonId == 5)
            {
                // 5�������� ����巡�� ����
                monsters.Add(data.Monsters[8]); // ����巡��
            }
            else if (dungeonId > 5)
            {
                // 6�� �̻󿡼��� ��� ���͵��� �������� ����
                monsters.AddRange(data.Monsters);
            }
            return monsters;
        }

        void DeadWriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        // ���� �÷��̾ ���ݽ� ǥ��Ǵ� ȭ��
        void ShowEnemyPhase(Monster selectedMonster)
        {

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("LV.{0} {1} �� ����!", selectedMonster.Level, selectedMonster.Name);
            if (!Dodge)
            {
                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("{0} ��(��) ������ϴ�. [������ : {1}] - ġ��Ÿ����!!\n", Player.Name, TrueDamage - Player.Defense);
                }
                else
                {
                    Console.WriteLine("{0} ��(��) ������ϴ�. [������ : {1}]\n",
                        Player.Name, TrueDamage - Player.Defense);
                }
                Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
                if (Player.Health - (TrueDamage - Player.Defense) <= 0)
                {
                    Console.WriteLine("{0} -> Dead\n", Player.Health);
                    Player.isDead = true;
                }
                else
                {
                    Console.WriteLine("{0} -> {1}\n", Player.Health, Player.Health - (TrueDamage - Player.Defense));
                }
                Console.WriteLine();
                Player.TakeDamage(TrueDamage - Player.Defense);
            }
            else
            {
                Console.WriteLine("ȸ�� ����!");
                Console.WriteLine("LV.{0} {1}�� ���������� �ƹ��ϵ� �Ͼ�� �ʾҽ��ϴ�.\n", selectedMonster.Level, selectedMonster.Name);
            }
            Console.WriteLine("0. ����\n");
            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        if (Player.isDead)
                        {
                            ShowLose();
                        }
                        return;
                    }
                    else
                    {
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

        // ��� ���͸� ������ ��µŴ� �¸� ȭ��.
        void ShowVictory()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine("�������� ���� {0}������ ��ҽ��ϴ�.\n", killcount);
            Console.WriteLine("[ĳ���� ����]");
            Player.LevelCalculator(Player);
            Console.WriteLine("LV.{0} {1} -> LV.{2} {1}", Defaultlevel, Player.Name, Player.Level);
            Console.WriteLine("HP {0} -> {1}", DefaultHp, Player.Health);
            Console.WriteLine("exp {0} -> {1}\n", DefaultExp, Player.Exp);
            Console.WriteLine("0. ����\n");
            Console.Write(">> ");
            Player.UpdateStageScore();
            Floar++;  // ���� ����
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
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }

        }

        // �÷��̾ isDead ���°� �Ǹ� ǥ��Ǵ� �й�ȭ��.
        void ShowLose()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
            Console.WriteLine("{0} -> {1}\n", Player.MaxHealth, Player.Health);
            Console.WriteLine("0. ����\n");
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
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

        // �÷��̾ ���͸� ������������ ����� ������ִ� �Լ�.
        void ShowAttackresult(Monster selectedMonster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("{0} �� ����!", Player.Name);
            if (!Dodge)
            {
                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("LV.{0} {1} ��(��) ������ϴ�. [������ : {2}] - ġ��Ÿ����!!\n",
                     selectedMonster.Level, selectedMonster.Name, TrueDamage - selectedMonster.Defense);
                }
                else
                {
                    Console.WriteLine("LV.{0} {1} ��(��) ������ϴ�. [������ : {2}]\n",
                      selectedMonster.Level, selectedMonster.Name, TrueDamage - selectedMonster.Defense);
                }
                Console.WriteLine("LV.{0} {1}", selectedMonster.Level, selectedMonster.Name);
                if (selectedMonster.Health - (TrueDamage - selectedMonster.Defense) <= 0)
                {
                    Console.WriteLine("{0} -> Dead\n", selectedMonster.Health);
                    selectedMonster.isDead = true;
                    killcount++;
                    Player.Exp += selectedMonster.Exp + selectedMonster.Level;
                }
                else
                {
                    Console.WriteLine("{0} -> {1}\n", selectedMonster.Health, selectedMonster.Health - (TrueDamage - selectedMonster.Defense));
                }
                Console.WriteLine();
                selectedMonster.TakeDamage(TrueDamage - selectedMonster.Defense);
            }
            else
            {
                Console.WriteLine("ȸ�� ����!");
                Console.WriteLine("LV.{0} {1}�� ���������� �ƹ��ϵ� �Ͼ�� �ʾҽ��ϴ�.\n", Player.Level, Player.Name);
            }
            Console.WriteLine("0. ����\n");
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
                                    TrueDamage = GetTrueDamage(monster.Attack);
                                    Criticalcalculator(TrueDamage);
                                    dodgecalcalculator();
                                }
                            }
                        }
                        StartBattle(Floar);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

        // ��Ʋ���� ȭ�鿡�� 2���� �Է½� �����ϴ� ��ų���â
        public void ShowSkilllist()
        {
            int n = 1;
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
            Console.WriteLine("[������]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
            foreach (var skill in Player.Skills)
            {
                if (skill.RequiredLevel <= Player.Level)
                {
                    Console.WriteLine("{0}. {1} - MP {2}\n    ��ų������ �� ����.", n, skill.Name, skill.RequiredMana);
                    n++;
                }
            }
            Console.WriteLine("0. ���");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
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
                    else if (choice > 0 && choice <= Monsters.Count)
                    {
                        var selectedMonster = Monsters[choice - 1];

                        if (selectedMonster != null && selectedMonster.isDead)
                        {
                            Console.WriteLine("�̹� ���� ����Դϴ�.");
                        }
                        else
                        {
                            TrueDamage = GetTrueDamage(Player.Attack);
                            Criticalcalculator(TrueDamage);
                            dodgecalcalculator();
                            ShowAttackresult(selectedMonster);
                        }
                    }
                    else
                    {
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

        // ��������ȭ�鿡�� 1���� �Է½� ǥ��Ǵ� ���ݴ����ȭ��
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
            Console.WriteLine("[������]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
            Console.WriteLine("0. ���\n");
            Console.WriteLine("����� �������ּ���.");
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
                            Console.WriteLine("�̹� ���� ����Դϴ�.");
                        }
                        else
                        {
                            TrueDamage = GetTrueDamage(Player.Attack);
                            Criticalcalculator(TrueDamage);
                            dodgecalcalculator();
                            ShowAttackresult(selectedMonster);
                        }
                    }
                    else
                    {
                        Console.WriteLine("�߸��� �Է��Դϴ�.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

        // �÷��̾ �������Խ� ó�� �������� ȭ��.
        public void StartBattle(int dungeonid)
        {

            Floar = Player.StageScore = dungeonid;

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
            Console.WriteLine("[������]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
            Console.WriteLine("1. ����");
            Console.WriteLine("2. ��ų\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
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
                    else if (choice == 2)
                    {
                        ShowSkilllist();
                    }
                    else
                    {
                        Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                    }
                }
                else
                {
                    Console.WriteLine("�߸��� �Է��Դϴ�. �ٽ� �õ��ϼ���.");
                }
            }
        }

    }
}
