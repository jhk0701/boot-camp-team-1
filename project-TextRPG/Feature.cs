namespace project_TextRPG
{
    abstract class Feature
    {
        protected IScene _scene;
        protected Character _player;

        string _name;
        public virtual string Name 
        {
            get { return _name; } 
            set { _name = value; }
        }


        /// <summary>
        /// 기능 표시
        /// </summary>
        public abstract void ShowMenu();

        /// <summary>
        /// 플레이어 선택 실행
        /// </summary>
        public abstract void Act();


        /// <summary>
        /// 기능(Feature) 시작함수
        /// </summary>
        /// <param name="player"></param>
        public virtual void Start(Character player)
        {
            _player = player;

            Console.Clear();
            // 기능 실행
            // 1. 표시
            ShowMenu();
            // 2. 행위 선택
            Act();
        }

        /// <summary>
        /// 기능 종료 함수
        /// </summary>
        public virtual void End()
        {
            // 공통적으로 원래 씬으로 돌아감.
            _scene.Return();
        }

    }
}
