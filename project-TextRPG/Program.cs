namespace project_TextRPG
{
    class Program
    {
        static void Main(string[] args)
        {

            StartScene();
            
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
