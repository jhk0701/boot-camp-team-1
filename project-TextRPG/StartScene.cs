using System.Numerics;
using System.Text.Json;
using System.Xml.Linq;

namespace project_TextRPG
{
    public class StartScene : IScene
    {
        public string SceneName { get; set; }
        public Character Player { get; set; }

        bool _isSkip;
        string _inputName;

        public StartScene(string sceneName, bool isSkip = false)
        {
            SceneName = sceneName;
            _isSkip = isSkip;
        }

        public void Start(Character visitor)
        {
            Player = visitor;
            if (!_isSkip)
                ShowIntro();

            bool isLoaded = DataIO.GetInstance().Load();
            Console.Clear(); // 화면을 지우고 메뉴를 새로 출력합니다.
            // 메뉴 항목을 출력합니다.
            Utility.ShowScript(
                "1. 새 게임\n",
                "2. 이어하기\n",
                "3. 종료\n"
            );

            //string? choice = Console.ReadLine(); // 사용자 입력을 받습니다.
            int select = Utility.GetSelection(1, 3, "옵션을 선택하세요.");
            //캐릭터 생성부분

            switch (select.ToString())
            {
                case "1":
                    // 새 게임을 시작합니다.
                    CreateCharacter();

                    SaveGame();

                    break;
                case "2": // 이어하기
                    if (isLoaded)
                    {
                        // 1. 데이터 있으면 로드
                        GameData gameData = DataIO.GetInstance().GetLoadedData();

                        Player = gameData.Player;
                        QuestManager.GetInstance().Load(gameData.Quests);
                        InstanceManager.GetInstance().Load(gameData.ItemId);
                    }
                    else
                    {
                        // 2. 데이터 없으면 생성
                        CreateCharacter();

                        SaveGame();
                    }

                    break;
                case "3":
                    // 게임을 종료합니다.
                    ExitGame();
                    return;
            }
        }

        public Character End()
        {
            return Player;
        }



        void ShowIntro()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"               ``    ` ` `    `` ```       `` `        ` `              `              `` `           
               `##  `##`     `##` ``       ``##`          `````````````  `               ##``         
``#########    `## ``##`      ##`   ```````  ##`        #################`              `##`          
  `     `## `  `##```##`      #################`         `            `##`             `####          
        `## `  `##  `##`      ##``  ```````  ##`        #################`      `   ``##` `###`  ``   
       ``##``  `##  `##`     `#################`        ##                      `#####` `  `  #####`` 
  #########`######  `##`             `## `     `        ##                         `###    ```##  ` ` 
 `##`````` `` ``##  `##`  `########################`   `#################`    ########################
 `##``    ` `  `##  `##`     ``           ``  `      ``                       ````````````````````````
 `##``     ``  `##  `##`      #################`    ########################`````` ```########`  `` ``
  ###########  `##  `##`     ` ````````````  ##`       `````````##` `````` ``     ####   ``   `###`   
`` ` `   `  `  `##  `##`      #################`      `        `## `       `    ``##`` ``  ```  ###`  
               `##  `##`      ##`  `           `             ` `##                ###  ` ``    ###``  
               `  `` ##```    ##################              ``##`                ``##########`````  



            ");
            Thread.Sleep(2000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"                    <임직원 사퇴!    <정시 퇴근 보장!    <타협이 없다면 죽창 뿐!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("               _______          _______          _______          _______");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("              /_투__쟁\\        /_투__쟁\\        /_투__쟁\\        /_투__쟁\\    <무한 투쟁!   ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"              |  O  O |  ㅇ    |  O--O |  ㅇ    | ㅡ  ㅡ|  ㅇ    |  ^  ^ |  ㅇ
               \______/ / /     \______/ / /     \______/ / /     \______/ / /
                /     \/ /       /     \/ /       /     \/ /       /     \/ /
                             
                                       ");
            Thread.Sleep(1000);
            Console.Write("                                         [TEAM ");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("넥");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("슨");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("노");
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("조]");
            Thread.Sleep(1000);
        }

        public void Return()
        {
        }

        Character LoadCharacter()
        {
            return new Character("name");
        }

        void CreateCharacter()
        {
            _inputName = "";
            int select = 0;

            // 이름 설정
            do
            {
                Console.Clear();
                Utility.ShowScript(
                    $"{SceneName}에 오신 여러분 환영합니다.\n",
                    "원하시는 이름을 설정해주세요.\n"
                );

                _inputName = Console.ReadLine();

                Utility.ShowScript(
                    $"\n입력하신 이름은 {_inputName}입니다.\n",
                    "저장하시겠습니까?\n\n",

                    "1. 저장\n0. 취소\n"
                );

                select = Utility.GetSelection(0, 1);
            }
            while (select.Equals(0));

            // 직업 선택
            Console.Clear();
            ShowClasses();

        }

        void ShowClasses()
        {
            ClassInitData[] classDatas = DataDefinition.GetInstance().ClassInitDatas;

            Utility.ShowScript(
                $"{SceneName}에 오신 여러분 환영합니다.\n",
                "원하시는 직업을 선택해주세요.\n\n"
            );

            Utility.WriteColorScript("[직업 목록]\n", ConsoleColor.Yellow);

            for (int i = 0; i < classDatas.Length; i++) 
            {
                Utility.ShowScript($"{i + 1}. {classDatas[i].name}");
            }

            Console.WriteLine();

            int select = Utility.GetSelection(1, classDatas.Length);
            ShowClass((EClass)(select - 1), classDatas[select - 1]);
        }

        void ShowClass(EClass selectedClass, ClassInitData classData)
        {
            Console.Clear();
            Utility.WriteColorScript("직업 선택\n", ConsoleColor.Yellow);
            Utility.ShowScript($"직업 : {classData.name}");
            Utility.WriteColorScript($"{classData.description}", ConsoleColor.Gray);
            Utility.ShowScript(
                "\n[기본 스탯]\n",
                $"공격력 : {classData.attack}\n",
                $"방어력 : {classData.defense}\n",
                $"체력 : {classData.maxHealth}\n",
                $"마나 : {classData.maxMana}\n\n",

                "[스킬 목록]"
            );

            for (int i = 0; i < classData.skills.Length; i++)
            {
                Utility.ShowScript(
                    $"- {classData.skills[i].Name} (Lv. {classData.skills[i].RequiredLevel}) : ",
                    $"{classData.skills[i].Description}"
                );
            }

            Utility.ShowScript(
                "\n\n",
                "1. 선택하기\n0. 나가기\n\n"
            );

            int select = Utility.GetSelection(0, 1);
            if (select == 0) 
            {
                Console.Clear();
                ShowClasses();
                return;
            }

            switch (selectedClass)
            {
                case EClass.ChairmanOfUnion:
                    Player = new ChairmanOfUnion(_inputName);
                    break;
                case EClass.SecretaryGeneral:
                    Player = new SecretaryGeneral(_inputName);
                    break;
                case EClass.DirectorOfUnion:
                    Player = new DirectorOfUnion(_inputName);
                    break;
            }
            // 초기 자금 지급
            Player.Gold += 500;
        }

        void ExitGame()
        {
            Console.Clear(); // 화면을 지우고 종료 메시지를 출력합니다.
            Console.WriteLine("게임을 종료합니다. 안녕히 가세요!");
            Environment.Exit(0); // 프로그램을 종료합니다.
        }


        public static string saveFilePath = "./SaveData.json";

        public void SaveGame()
        {
            DataIO.GetInstance().Save(Player);

            return;
            string jsonString = JsonSerializer.Serialize(Player);
            File.WriteAllText(saveFilePath, jsonString);
            Console.WriteLine("게임이 저장되었습니다.");
        }

        public bool LoadCharacterFromSaveDate()
        {
            if (File.Exists(saveFilePath))
            {
                try
                {
                    // 파일에서 데이터를 읽어와서 캐릭터 객체로 복원합니다.
                    string jsonString = File.ReadAllText(saveFilePath);
                    Player = JsonSerializer.Deserialize<Character>(jsonString);

                    Console.WriteLine("저장된 게임을 불러왔습니다!");
                    return true;
                }
                catch (Exception ex)
                {
                    // 데이터를 불러오는 중 오류가 발생할 경우 메시지를 출력합니다.
                    Console.WriteLine($"저장된 데이터를 불러오는 중 오류가 발생했습니다: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

    }
}

