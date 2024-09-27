namespace project_TextRPG
{
    class FeatureInventory : Feature
    {
        public FeatureInventory(string featureName, IScene scene) : base(featureName, scene) { }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(

            );
        }

        public override void Act()
        {
            
        }

    }
}
