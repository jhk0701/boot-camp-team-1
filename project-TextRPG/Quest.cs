using System.Text;

namespace project_TextRPG
{
    public class Quest
    {
        public string Title { get; protected set; }
        
        public string Description { get; protected set; }

        public int TargetCount { get; protected set; }
        
        public int Count { get; set; }



    }
}
