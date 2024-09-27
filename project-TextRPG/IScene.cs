namespace project_TextRPG
{
    interface IScene
    {
        string SceneName { get; set; }
        Character Player { get; set; }

        void Start(Character unit);
        Character End();
        void Return();
    }
}
