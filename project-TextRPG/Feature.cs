namespace project_TextRPG
{
    class Feature
    {
        IScene _scene;

        string _name;
        public virtual string Name { get; set;}

        public Feature(string featureName, IScene scene)
        {
            Name = featureName;
            _scene = scene;
        }

        public virtual void Start()
        {
            Console.Clear();
            // 기능 실행
            // 1. 표시
            ShowMenu();
            // 2. 행위 선택
            Act();
        }

        /// <summary>
        /// 기능 관련 사항 표시
        /// </summary>
        public virtual void ShowMenu()
        {

        }

        public virtual void Act()
        {
            // 관련 기능

            // 관련 기능 완료 후, 복귀
            // End();
        }

        public virtual void End()
        {
            // 기능 종료 시 호출
            // 공통적으로 원래 씬으로 돌아감.
            _scene.Return();
        }
    }
}
