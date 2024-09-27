namespace project_TextRPG
{
    class Program
    {
        const string COMMON_NAME = "악덕 기업";

        static void Main(string[] args)
        {
            StartScene();
            
            Character player;

            // 캐릭터 생성부분
            // 1. 데이터 있으면 로드
            // 2. 데이터 없으면 생성

            // 임시로 전사 생성
            player = new ChairmanOfUnion("Player");

            // 게임 시작
            GameScene gameScene = new GameScene(COMMON_NAME);
            gameScene.Start(player);
        }

        static void StartScene() 
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

        
    }
}
