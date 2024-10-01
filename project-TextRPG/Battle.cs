
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

        int killcount;       //해당 층에서 플레이어가 죽인 몬스터의 숫자
        float TrueDamage;    //플레이어 데미지의 10% 오차를 계산한 값
        int Floar;           //던전 층수
        bool CriticalHit;    //치명타 공격 발동여부
        bool Dodge;          //회피성공 여부

        float Defaultlevel;  //플레이어의 던전진입당시의 레벨 (승리화면에서 비교용)
        float DefaultHp;     //플레이어의 던전진입당시의 체력 (승리화면에서 비교용)
        float DefaultExp;    //플레이어의 던전진입당시의 경험치 (승리화면에서 비교용)

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

        // 10% 확률로 발생하는 회피 계산기
        void dodgecalcalculator()
        {
            int rolldice = ran.Next(1, 101);
            Dodge = false;

            if (rolldice <= 10)
            {
                Dodge = true;
            }
        }


        // 15% 확률로 발생하는 치명타 계산기
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

        // 플레이어 데미지오차 10% 를 계산후 다시 가져오는 함수
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


        // 몬스터 생성마리수, 몬스터 순서를 범위내에서 랜덤으로 배정해주는 함수
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

        
        // 던전 층수에 따라서 Monsters리스트에 몬스터를 생성해주는 함수
        // 던전 층수에 따라서 Monsters리스트에 몬스터를 생성해주는 함수
        List<Monster> CreateMonsters(int dungeonId)

        {
            DataDefinition data = DataDefinition.GetInstance(); // DataDefinition 인스턴스 가져오기
            List<Monster> monsters = new List<Monster>();
            if (dungeonId == 1)
            {
                // 1층에서는 부당계약서, 연장근무
                monsters.Add(data.Monsters[0]); // 부당계약서
                monsters.Add(data.Monsters[1]); // 연장근무
            }
            else if (dungeonId == 2)
            {
                // 2층에서는 환영복지술사, 월급루팡, 인사고과망령 추가 등장
                monsters.Add(data.Monsters[2]); // 환영복지술사
                monsters.Add(data.Monsters[3]); // 월급루팡
                monsters.Add(data.Monsters[4]); // 인사고과망령
            }
            else if (dungeonId == 3)
            {
                // 3층에서는 노동착취자, 과로골렘 추가 등장
                monsters.Add(data.Monsters[5]); // 노동착취자
                monsters.Add(data.Monsters[6]); // 과로골렘
            }
            else if (dungeonId == 4)
            {
                // 4층에서는 해고의그림자 추가 등장
                monsters.Add(data.Monsters[7]); // 해고의그림자
            }
            else if (dungeonId == 5)
            {
                // 5층에서는 사장드래곤 등장
                monsters.Add(data.Monsters[8]); // 사장드래곤
            }
            else if (dungeonId > 5)
            {
                // 6층 이상에서는 모든 몬스터들이 랜덤으로 등장
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

        // 적이 플레이어를 공격시 표출되는 화면
        void ShowEnemyPhase(Monster selectedMonster)
        {

            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("LV.{0} {1} 의 공격!", selectedMonster.Level, selectedMonster.Name);
            if (!Dodge)
            {
                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}] - 치명타공격!!\n", Player.Name, TrueDamage - Player.Defense);
                }
                else
                {
                    Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]\n",
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
                Console.WriteLine("회피 성공!");
                Console.WriteLine("LV.{0} {1}이 공격했지만 아무일도 일어나지 않았습니다.\n", selectedMonster.Level, selectedMonster.Name);
            }
            Console.WriteLine("0. 다음\n");
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
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

        // 모든 몬스터를 잡으면 출력돼는 승리 화면.
        void ShowVictory()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", killcount);
            Console.WriteLine("[캐릭터 정보]");
            Player.LevelCalculator(Player);
            Console.WriteLine("LV.{0} {1} -> LV.{2} {1}", Defaultlevel, Player.Name, Player.Level);
            Console.WriteLine("HP {0} -> {1}", DefaultHp, Player.Health);
            Console.WriteLine("exp {0} -> {1}\n", DefaultExp, Player.Exp);
            Console.WriteLine("0. 다음\n");
            Console.Write(">> ");
            Player.UpdateStageScore();
            Floar++;  // 층수 증가
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0)
                    {
                        QuestManager.GetInstance().PerformQuest(this, 1);
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

        // 플레이어가 isDead 상태가 되면 표출되는 패배화면.
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

        // 플레이어가 몬스터를 공격했을때의 결과를 출력해주는 함수.
        void ShowAttackresult(Monster selectedMonster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine("{0} 의 공격!", Player.Name);
            if (!Dodge)
            {
                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 치명타공격!!\n",
                     selectedMonster.Level, selectedMonster.Name, TrueDamage - selectedMonster.Defense);
                }
                else
                {
                    Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n",
                      selectedMonster.Level, selectedMonster.Name, TrueDamage - selectedMonster.Defense);
                }
                Console.WriteLine("LV.{0} {1}", selectedMonster.Level, selectedMonster.Name);
                if (selectedMonster.Health - (TrueDamage - selectedMonster.Defense) <= 0)
                {
                    Console.WriteLine("{0} -> Dead\n", selectedMonster.Health);
                    selectedMonster.isDead = true;
                    killcount++;
                    Player.Exp += selectedMonster.Exp + selectedMonster.Level;

                    // 퀘스트 수행
                    QuestManager.GetInstance().PerformQuest(selectedMonster, 1);
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
                Console.WriteLine("회피 성공!");
                Console.WriteLine("LV.{0} {1}이 공격했지만 아무일도 일어나지 않았습니다.\n", Player.Level, Player.Name);
            }
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
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
            }
        }

        // 배틀시작 화면에서 2번을 입력시 진입하는 스킬목록창
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
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
            foreach (var skill in Player.Skills)
            {
                if (skill.RequiredLevel <= Player.Level)
                {
                    Console.WriteLine("{0}. {1} - MP {2}\n    스킬설명이 들어갈 예정.", n, skill.Name, skill.RequiredMana);
                    n++;
                }
            }
            Console.WriteLine("0. 취소");
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
                            Criticalcalculator(TrueDamage);
                            dodgecalcalculator();
                            ShowAttackresult(selectedMonster);
                        }
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

        // 시작전투화면에서 1번을 입력시 표출되는 공격대상선택화면
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
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
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
                            Criticalcalculator(TrueDamage);
                            dodgecalcalculator();
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

        // 플레이어가 던전진입시 처음 보여지는 화면.
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
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana);
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬\n");
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
                    else if (choice == 2)
                    {
                        ShowSkilllist();
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
