namespace project_TextRPG
{
    internal class FeatureRest : Feature
    {

        public FeatureRest(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "보유 중인 아이템을 관리할 수 있습니다.\n\n",

                "[아이템 목록]\n"
            );

            for (int i = 0; i < _player.Inventory.Items.Length; i++)
            {
                Equipment item = _player.Inventory.Items[i];

                Utility.ShowScript(
                    "-",
                    _player.Inventory.IsEquipped(item) ? "[E]" : "",
                    $"{item.Name,-15} | {item.GetBonusSpec(),-15} | {item.Description}"
                );
            }

            Console.WriteLine();
        }

        public override void Act()
        {
            
        }
    }
}
