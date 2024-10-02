using System.Text.Json;

namespace project_TextRPG
{
    public class StartScene : IScene
    {
        public static string saveFilePath = "./SaveData.json";
        public string SceneName { get; set; }
        public Character Player { get; set; }

        bool _isSkip;

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

            bool isLoaded = LoadCharacterFromSaveDate(); ; // 데이터 로드
            Console.Clear(); // 화면을 지우고 메뉴를 새로 출력합니다.
            // 메뉴 항목을 출력합니다.
            Console.WriteLine("1. 새 게임");
            Console.WriteLine("2. 이어하기");
            Console.WriteLine("3. 종료");
            Console.Write("옵션을 선택하세요: ");
            string? choice = Console.ReadLine(); // 사용자 입력을 받습니다.
            //캐릭터 생성부분
            switch (choice)
            {
                case "1":
                    // 새 게임을 시작합니다.
                    CreateCharacter();
                    SaveGame();
                    break;
                case "2":
                    if (isLoaded)
                    {
                        // 1. 데이터 있으면 로드
                        LoadCharacter();
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
                default:
                    // 잘못된 입력이 있을 경우 메시지를 출력하고 다시 입력을 받습니다.
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    break;
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
            Thread.Sleep(2000);
            Console.Write("                                         [TEAM ");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("넥");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("슨");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("노");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("조]");
            Thread.Sleep(2000);
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
            string name = "";
            int select = 0;

            // 이름 설정
            do
            {
                Console.Clear();
                Utility.ShowScript(
                    $"{SceneName}에 오신 여러분 환영합니다.\n",
                    "원하시는 이름을 설정해주세요.\n"
                );
                name = Console.ReadLine();

                Utility.ShowScript(
                    $"\n입력하신 이름은 {name}입니다.\n",
                    "저장하시겠습니까?\n\n",

                    "1. 저장\n0. 취소\n"
                );

                select = Utility.GetSelection(0, 1);
            }
            while (select.Equals(0));

            // 직업 선택
            Console.Clear();
            Utility.ShowScript(
                $"{SceneName}에 오신 여러분 환영합니다.\n",
                "원하시는 직업을 선택해주세요.\n\n",

                "1. 노조 위원장\n",
                "2. 사무총장\n",
                "3. 조직 국장\n",
                "\n"
            );

            select = Utility.GetSelection(1, 3);
            switch ((EClass)(select - 1))
            {
                case EClass.ChairmanOfUnion:
                    Player = new ChairmanOfUnion(name);
                    break;
                case EClass.SecretaryGeneral:
                    Player = new SecretaryGeneral(name);
                    break;
                case EClass.DirectorOfUnion:
                    Player = new DirectorOfUnion(name);
                    break;
            }
        }

        public void SaveGame()
        {
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
        static void ExitGame()
        {
            Console.Clear(); // 화면을 지우고 종료 메시지를 출력합니다.
            Console.WriteLine("게임을 종료합니다. 안녕히 가세요!");
            Environment.Exit(0); // 프로그램을 종료합니다.
        }

    }
}

