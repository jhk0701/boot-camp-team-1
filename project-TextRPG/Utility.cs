using System.Text;

namespace project_TextRPG
{
    class Utility
    {
        /// <summary>
        /// 플레이어의 행동을 입력받을 때 쓰는 함수. 
        /// min : 선택지의 최솟값. 대체로 0일 듯
        /// max : 선택지의 최댓값.
        /// msg : 입력을 받을 때 사용할 메시지. 생략가능
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int GetSelection(int min, int max, string msg = "원하는 행동을 입력해주세요.")
        {
            Console.WriteLine(msg);

            string i = Console.ReadLine();
            int result = -1;

            while (!int.TryParse(i, out result) || result < min || result > max)
            {
                Console.WriteLine();
                WriteColorScript("잘못된 입력입니다.\n", ConsoleColor.Red);

                Console.WriteLine(msg);
                i = Console.ReadLine();
            }

            return result; 
        }


        /// <summary>
        /// 색상 텍스트
        /// </summary>
        /// <param name="script"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        public static void WriteColorScript(string script, ConsoleColor foreColor, ConsoleColor backColor = ConsoleColor.Black)
        {
            ConsoleColor oForeCol = Console.ForegroundColor;
            ConsoleColor oBackCol = Console.BackgroundColor;

            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;
            Console.WriteLine(script);

            // Console.ResetColor();

            Console.ForegroundColor = oForeCol;
            Console.BackgroundColor = oBackCol;
        }

        public static void ShowScript(params string[] scripts)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < scripts.Length; i++)
                sb.Append(scripts[i]);

            Console.WriteLine(sb.ToString());
        }

    }
}
