namespace project_TextRPG
{
    class StartScene : IScene
    {
        public string SceneName { get; set; }
        public Character Player { get; set; }


        public StartScene(string sceneName)
        {
            SceneName = sceneName;
        }

        public void Start(Character visitor)
        {
            Player = visitor;
            ShowIntro();

            bool isLoaded = false; // 데이터 로드

            // 캐릭터 생성부분
            if (isLoaded) 
            {
                // 1. 데이터 있으면 로드
            }
            else
            {
                // 2. 데이터 없으면 생성
                Player = CreateCharacter();
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
            Thread.Sleep(500);
        }

        public void Return()
        {

        }
        
        Character LoadCharacter()
        {
            return new Character("name");
        }

        Character CreateCharacter()
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
                    $"{name}님 이시군요.\n",
                    "저장하시겠습니까?\n\n",

                    "1. 저장\n0. 취소\n"
                );

                select = Utility.GetSelection(0, 1);
            }
            while (select.Equals(0));



            return new Character(name);
        }
    
    }
}
