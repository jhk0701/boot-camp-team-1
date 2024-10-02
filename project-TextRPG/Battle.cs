namespace project_TextRPG
{
    class Battle
    {
        IScene _scene;

        Character Player;
        List<Monster> Monsters;  // 해당 층에서 사용될 몬스터 리스트
        Skill SelectedSkill;    //선택된 스킬값 저장용 변수
        List<Equipment> DungeonRewardEquipItem; //해당 층의 장비보상아이템

        int DungeonRewardGold; // 해당 층의 보상골드
        int killcount;       //해당 층에서 플레이어가 죽인 몬스터의 숫자
        float TrueDamage;    //플레이어 데미지의 10% 오차를 계산한 값
        int Floar;           //던전 층수
        bool UseSkill;       //스킬사용 여부
        bool CriticalHit;    //치명타 공격 발동여부
        bool Dodge;          //회피성공 여부


        float Defaultlevel;  //플레이어의 던전진입당시의 레벨 (승리화면에서 비교용)
        float DefaultHp;     //플레이어의 던전진입당시의 체력 (승리화면에서 비교용)
        float DefaultExp;    //플레이어의 던전진입당시의 경험치 (승리화면에서 비교용)

        Random rand;
        public Battle(Character player, int dungeonid, IScene scene)
        {
            Player = player;

            Monsters = new List<Monster>();

            DungeonRewardEquipItem = new List<Equipment>();

            rand = new Random();

            CreateMonsters(dungeonid);
            Monsters = ShuffleMonsters(Monsters);

            Defaultlevel = player.Level;
            DefaultHp = player.Health;
            DefaultExp = player.Exp;

            _scene = scene;

        }

        void ShowBossBattle()
        {
            Utility.ShowScript(
            "                       :=              ..;;:*!    \n",
            "                      ;$#           .~~*~:~;=!    \n",
            "                     *$=~        ,:,~~,,,-~=;     \n",
            "                    !$$$.~!     $~..~:. !;;@;     \n",
            "                 ,-$=$$:-*  --*,-~- ,.,,;*!;      \n",
            "               ,;$***$=,:;:,;-., .,-.-:*!$*       \n",
            "               ~$#!=-;*-=*!...    .~:!;$$:.       \n",
            "             ..**-==- $#*,.~  ~~ ,-!=*!=-         \n",
            "             !#*:.*;--,,!~. --,~:;=*$*~!          \n",
            "            ~;#!~,;**,,,-;~,.:!!#=$!!,            \n",
            "           ,#=#~ *-;= ,;$$..*!$-.:=*      ;;,     \n",
            "           $!!-:=-:. .!$=,-*$*~:#@@#*!:*;!=.      \n",
            "     -;;;;;=!- ~*$;;!*#$#*:;*;*$@@@$$==$#*        \n",
            "     ~#@:,#@===*-:~=#@==$=**@@##@@@#@#=#,.        \n",
            "      $ ;#@$@@...:!$*=*@:$~$@##$*;$@#*=           \n",
            "      ;- .~@=#$=#@--;~~$,~:,~;-,*=#=~:!;!*!       \n",
            " -,    : .#$;:@;=!!-~:~!;*:, !.-!=#**!$$=-        \n",
            " ~=   =.$*!..=-*. .~:.!#,$,.$;*#@@#$$$$          .\n",
            "  ~   ; ,-:~,-.~$-*~~,:;,-*-##*#$#$=$$;#        ..\n",
            "  :,,,~--:*=-- ..-*=#@:!#=:=$;;=:-;$::!=-.    ....\n",
            "  ,;!~,~.!=$, ,.,,-$$$#@;:,!#:~--.,,-$#$=*:  ....,\n",
            "   ##!# -*:-;- ~:$@=$@$~,~*#,;:,.!$=#$=$ !#; ..., \n",
            "   ==~: ~ .,-,,$$-@@=:,:~*:!:=$##@@@#$,     -..,-~\n",
            "    =~,.~,~-=*-@@=#;~ :$!-~;;##$=@###=:;.   ..,-~:\n",
            "    @!::.:!*#@;##;$.**:,.:==-~*@#$$=$@#.   ..,-~:!\n",
            "    -=$$*!@;;@$##=.,*~-,,=;$~~.=$*!=##*   ..,-~:;*\n",
            "    ,*:;,-##**!==:~;--!:!*~;!#*;!$!!#*$-, .,-~:;$$\n",
            "     :.-~#@@#*-==,=*;*$!:##==@$*!=;:@**~:,-~:;;*$#\n",
            "       @@@@$=,,-. -*-~@@@=*#@==#~;-.     .;;!*$###\n",
            "       .$;*!,:-!::~#=-;#@@@=$*.--=!---,.. .:!!;;**\n",
            "       .#*#-,-,;~#;$@#=$#$##**.,:!,,;:!!:-,.,..   \n",
            "       .#!:,,-*$~!-#@@@##=@!,--: ..,~*===;,....., \n",
            "       .#;:~!$$ !~,#@@##*#=*#   . ,-;=##=-,...  .~\n",
            "       .#*-$;-    .*#@###=*! . .   ~==$#=~-,,..  .\n",
            "       .=:*-.,    *!$@##@-.       :*$$$$=::--,..  \n",
            "         ;@  !    =;$#$$$$       ~;#@=$*$!;:~-,.  \n",
            "         :      .#$$=#=;;;*     -:*$=*==$=!!:~-,..\n",
            "         ~     ,=#=@#=:~*=@*.  ::-**!*!=#*!!;:--,,\n",
            "               ;#@*##~~!$#;=$ :~.**-!!;$$!*=!;:~--\n",
            "             ;$##$$$$=#@@ . !;:.$=$::!*=$!*=*;!;::\n",
            "           .~$@#@!!=###$~.   :-$*=;!!$#=$$=$$***!!\n",
            "          -*=###$;~!$*#;,   . ~!!!;*!!=*$#$#==$$==\n",
            "        !!=#$$#$*;:!$:$: . . ,!:#!;*;!*=#=###$@@@=\n",
            "      .=$####$@@*-:#*;*~    ,!~=;!;!;;;*=$$#@@@#@@\n",
            "      =#$$###$##;,:=!!;~. .~:;!$;;,;.!!=**$$#@@@#@@\n",
            "      =@$#####$$;:!**~!-  ~*~;*;;=~ .-;#==*$$##@@@\n",
            " ..  !@@###@##=@*~$$=;!- .-:*;!!*,..~ ;;!=**$##@@@\n",
            "     :@##$@@###@=-#*;*;~ ~!~:**;---;*::::*=*==#@@@\n",
            " ..,,!@@###@#$#@#~@$*$*:~*!;;*==$=;-=**!$;$!$$#@@@\n"
        );

            Utility.WriteColorScript(
                "버러지 자식들 기어코 여기까지 올라왔군 하지만 헛된 망상은 여기까지다\n\n" +
                "이곳은 내가 지배하는 회사 변화의 바람 따위는 1m/s 도 혀용되지 않는다.\n\n", ConsoleColor.Blue
                );
        }

        // 던전 보상골드를 설정하는 함수
        void SetDungeonRewardGold(int floar)
        {
            DungeonRewardGold = floar * 500;
        }

        // 스킬데미지를 계산하는 함수
        float GetSkillDamage(float damage, float skillPercentage)
        {
            float skilldecimal = skillPercentage / 100;

            float SkillDamage = damage * skilldecimal;

            return SkillDamage;
        }

        // 10% 확률로 발생하는 회피 계산기
        void dodgecalcalculator()
        {
            int rolldice = rand.Next(1, 101);
            Dodge = false;

            if (rolldice <= 10)
            {
                Dodge = true;
            }
        }


        // 15% 확률로 발생하는 치명타 계산기
        void Criticalcalculator(float damage)
        {
            int rolldice = rand.Next(1, 101);
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

            damage = rand.Next((int)(damage - RandomDamageRange), (int)(damage + RandomDamageRange));
            return damage;
        }


        // 몬스터 생성마리수, 몬스터 순서를 범위내에서 랜덤으로 배정해주는 함수
        List<Monster> ShuffleMonsters(List<Monster> values)
        {
            int randMonsterCount = rand.Next(1, 5);


            var shuffled = values.OrderBy(_ => rand.Next()).ToList();

            while (shuffled.Count > randMonsterCount)
            {
                shuffled.RemoveAt(shuffled.Count - 1);
            }

            return shuffled;
        }

        // 던전 층수에 따라서 Monsters리스트에 몬스터를 생성해주는 함수
        void CreateMonsters(int dungeonId)
        {
            if (dungeonId == 1)
            {
                // 1층에서는 부당계약서, 연장근무
                for (int i = 0; i < 3; i++)
                {
                    Monsters.Add(DataDefinition.GetInstance().Monsters[0].Copy()); // 부당계약서
                    Monsters.Add(DataDefinition.GetInstance().Monsters[1].Copy()); // 연장근무
                }
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[0].Copy()); // 1층 보상 회사휴지
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[5].Copy()); // 노동자조끼
            }
            else if (dungeonId == 2)
            {
                // 2층에서는 환영복지술사, 월급루팡, 인사고과망령 추가 등장
                for (int i = 0; i < 3; i++)
                {
                    Monsters.Add(DataDefinition.GetInstance().Monsters[2].Copy()); // 환영복지술사
                    Monsters.Add(DataDefinition.GetInstance().Monsters[3].Copy()); // 월급루팡
                    Monsters.Add(DataDefinition.GetInstance().Monsters[4].Copy()); // 인사고과망령
                }
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[10].Copy()); // 2층 보상 낡은마스크
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[15].Copy()); // 공사장헬멧
            }
            else if (dungeonId == 3)
            {
                // 3층에서는 노동착취자, 과로골렘 추가 등장
                for (int i = 0; i < 3; i++)
                {
                    Monsters.Add(DataDefinition.GetInstance().Monsters[5].Copy()); // 노동착취자
                    Monsters.Add(DataDefinition.GetInstance().Monsters[6].Copy()); // 과로골렘
                }
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[1].Copy()); // 3층 보상 부러진 법인카드
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[11].Copy()); // 가족사진
            }
            else if (dungeonId == 4)
            {
                // 4층에서는 해고의그림자 추가 등장
                Monsters.Add(DataDefinition.GetInstance().Monsters[7].Copy()); // 해고의그림자
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[16].Copy()); // 4층 붉은 두건
            }
            else if (dungeonId == 5)
            {
                // 5층에서는 사장드래곤 등장
                Monsters.Add(DataDefinition.GetInstance().Monsters[8].Copy()); // 사장드래곤
                DungeonRewardEquipItem.Add(DataDefinition.GetInstance().Equipments[4].Copy()); // 5층 사장님 법카
            }
            else
            {
                // 6층 이상에서는 모든 몬스터들이 랜덤으로 등장
                for (int i = 0; i < 10; i++)
                Monsters.Add(DataDefinition.GetInstance().Monsters[rand.Next(1,9)].Copy());
            }
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
            Utility.WriteColorScript("Battle!!\n", ConsoleColor.Yellow);
            Console.WriteLine("LV.{0} {1} 의 공격!", selectedMonster.Level, selectedMonster.Name);
            if (!Dodge)
            {
                float damage = TrueDamage - Player.Defense;
                damage = damage > 0f ? damage : 0f;

                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}] - 치명타공격!!\n", Player.Name, damage);
                }
                else
                {
                    Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]\n",
                        Player.Name, damage);
                }
                Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
                if (Player.Health - (damage) <= 0)
                {
                    Console.WriteLine("{0} -> Dead\n", Player.Health);
                    Player.isDead = true;
                }
                else
                {
                    Console.WriteLine("{0} -> {1}\n", Player.Health, Player.Health - damage);
                }
                Console.WriteLine();
                Player.TakeDamage(damage);
            }
            else
            {
                Console.WriteLine("{0} 회피 성공!\n", Player.Name);
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
            Utility.WriteColorScript("Battle!! - Result\n",ConsoleColor.Yellow);
            Utility.WriteColorScript("Victory\n", ConsoleColor.Green);
            Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", killcount);
            Console.WriteLine("[캐릭터 정보]");

            int afterBattleExp = Player.Exp;
            Player.LevelCalculator(Player);

            Console.WriteLine("LV.{0} {1} -> LV.{2} {1}", Defaultlevel, Player.Name, Player.Level);
            Console.WriteLine("HP {0} -> {1}", DefaultHp, Player.Health);
            Console.WriteLine("exp {0} -> {1}\n", DefaultExp, afterBattleExp);
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine("{0} Gold 획득!",DungeonRewardGold);
            foreach (var item in DungeonRewardEquipItem)
            {
                if(DungeonRewardEquipItem == null)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("{0} 획득!", item.Name);
                    Player.Inventory.Add(item);
                }
            }
            Player.Gold += DungeonRewardGold;
            Console.WriteLine();
            if (Floar == 5)
            {
                Utility.WriteColorScript("Game Clear!\n", ConsoleColor.Yellow);
                Utility.ShowScript("당신은 사장드래곤을 무찌르고 혁명에 성공하였습니다.\n",
                    "사장은 고용노동부에 구속당하였으며 당신은 노조원들의 지지를 받아 사장의 자리를 이어받았습니다.\n",
                    "당신은 직원복지에 온힘을 다하겠다 선언하였지만 회사는 수익을 내기위한곳, 회사가 없으면 노조또한 존재하지 못합니다.\n",
                    "당신은 경영자와 노동자 모두를 만족시키는 유토피아를 만들수 있을까요, 아니면 또다른 사장 드래곤이 되어 도전을 받게될까요.\n\n");
                Utility.ShowScript("게임을 게속 진행하실수 있으며 던전 6층 부터는 랜덤한 몬스터가 나오는 무한모드 입니다.\n");
            }
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
            Utility.WriteColorScript("Battle!! - Result\n", ConsoleColor.Yellow);
            Utility.WriteColorScript("You Lose\n", ConsoleColor.Gray);
            Console.WriteLine("LV.{0} {1}", Player.Level, Player.Name);
            Console.WriteLine("{0} -> {1}\n", Player.MaxHealth + Player.EquipHealth, Player.Health);
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

            if (!UseSkill)
            {
                Console.WriteLine("{0} 의 공격!", Player.Name);
            }
            // 스킬사용시 공격! 이 아닌 스킬명을 외침
            else
            {
                Console.WriteLine("{0} 의 {1}!!!", Player.Name,SelectedSkill.Name);
                Player.SetManaDrop(SelectedSkill.RequiredMana); //매개변수 값 만큼 플레이어의 mana를 떨어뜨리는 함수
            }

            float damage = TrueDamage - selectedMonster.Defense;
            damage = damage <= 0f ? 0f : damage;

            // 공격하기전 회피계산기에서 회피를 실패할시 공격을 줌 (스킬은 회피하지 못함)
            if (!Dodge)
            {
                if (CriticalHit != null && CriticalHit)
                {
                    Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 치명타공격!!\n",
                     selectedMonster.Level, selectedMonster.Name, Math.Floor(damage));
                }
                else
                {
                    Console.WriteLine("LV.{0} {1} 을(를) 맞췄습니다. [데미지 : {2:0}]\n",
                      selectedMonster.Level, selectedMonster.Name, Math.Floor(damage));
                }
                Console.WriteLine("LV.{0} {1}", selectedMonster.Level, selectedMonster.Name);
                if (selectedMonster.Health - (damage) <= 0)
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
                    Console.WriteLine("{0} -> {1}\n", selectedMonster.Health, selectedMonster.Health - (damage));
                }
                Console.WriteLine();
                selectedMonster.TakeDamage(damage);
            }
            // 상대가 회피하면 바로 턴종료
            else
            {
                Console.WriteLine("{0} 회피 성공!\n", selectedMonster.Name);
                Console.WriteLine("LV.{0} {1}이 공격했지만 아무일도 일어나지 않았습니다.\n", Player.Level, Player.Name);
            }
            UseSkill = false;
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
                                    TrueDamage = GetTrueDamage(monster.Attack);
                                    Criticalcalculator(TrueDamage);
                                    dodgecalcalculator();
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

        // 배틀시작 화면에서 2번을 입력시 진입하는 스킬목록창
        public void ShowSkilllist()
        {
            Console.Clear();
            Utility.WriteColorScript("Battle!!\n", ConsoleColor.Yellow);
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("Lv.{0} {1} HP {2}", monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    Utility.WriteColorScript($"Lv.{monster.Level} {monster.Name} Dead", ConsoleColor.Gray);
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth + Player.EquipHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana + Player.EquipMana);
            int skillCount = 0;
            int checkHaveSkill = 0;
            foreach (var skill in Player.Skills)
            {
                if (skill.RequiredLevel <= Player.Level)
                {
                    Console.WriteLine("{0}. {1} - MP {2}\n    {3}\n", skillCount + 1, skill.Name, skill.RequiredMana, skill.Description);
                    skillCount++;
                }
                if (skillCount == checkHaveSkill)
                {
                    Console.WriteLine("현재 사용할수있는 스킬이 없습니다\n");
                    checkHaveSkill++;
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
                        StartBattle(Floar);
                        return;
                    }
                    else if (choice > 0 && choice <= skillCount)
                    {
                        var selectedSkill = Player.Skills[choice - 1];
                        SelectedSkill = selectedSkill;

                        if (selectedSkill != null && selectedSkill.RequiredMana <= Player.Mana)
                        {
                            float skillPercentage = rand.Next((int)selectedSkill.MinPowerRange, (int)selectedSkill.MaxPowerRange);
                            TrueDamage = GetSkillDamage(Player.Attack, skillPercentage);
                            UseSkill = true;
                            ShowAttackList();
                        }
                        else
                        {
                            Console.WriteLine("마나가 부족합니다.");
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
            Utility.WriteColorScript("Battle!!\n", ConsoleColor.Yellow);
            Console.WriteLine("[적정보]");
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("{0} - Lv.{1} {2} HP {3}\n", n, monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    Utility.WriteColorScript($"{n} - Lv.{monster.Level} {monster.Name} Dead\n", ConsoleColor.Gray);
                }
                n++;
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth + Player.EquipHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana + Player.EquipMana);
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
                            if (!UseSkill)
                            {
                                TrueDamage = GetTrueDamage(Player.Attack);
                                Criticalcalculator(TrueDamage);
                                dodgecalcalculator();
                                ShowAttackresult(selectedMonster);
                            }
                            else
                            {
                                CriticalHit = false;
                                Dodge = false;
                                ShowAttackresult(selectedMonster);
                            }
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
            SetDungeonRewardGold(dungeonid);

            Console.Clear();
            if (dungeonid == 5)
            {
                ShowBossBattle();
                Thread.Sleep(2000);
                Utility.WriteColorScript("BOSS", ConsoleColor.Red);
            }
            Utility.WriteColorScript("Battle!!\n", ConsoleColor.Yellow);

            Console.WriteLine("[적정보]");
            foreach (var monster in Monsters)
            {
                if (!monster.isDead)
                {
                    Console.WriteLine("Lv.{0} {1} HP {2}\n", monster.Level, monster.Name, monster.Health);
                }
                else
                {
                    Utility.WriteColorScript($"Lv.{monster.Level} {monster.Name} Dead\n", ConsoleColor.Gray);
                }
            }
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}  {1} ({2})", Player.Level, Player.Name, Player.CharClass);
            Console.WriteLine("HP {0}/{1}", Player.Health, Player.MaxHealth + Player.EquipHealth);
            Console.WriteLine("MP {0}/{1}\n", Player.Mana, Player.MaxMana + Player.EquipMana);
            Console.WriteLine("0. 도망");
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
                        _scene.Return();
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
