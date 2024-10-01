namespace project_TextRPG
{
    class FeatureInventory : Feature
    {
        Feature[] _subFeatures;

        public FeatureInventory(string featureName, IScene scene) 
        { 
            _scene = scene;
            Name = featureName;

            _subFeatures = [
                new FeatureInventoryEquip("인벤토리 - 장착 관리", scene)
            ];
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

                // _player.Inventory.IsEquipped(item)
                Utility.ShowScript(
                    "- ",
                    item.GetDesc(0, _player.Inventory.IsEquipped(item))
                );
            }

            Console.WriteLine();
        }

        public override void Act()
        {
            Utility.ShowScript(
                "1. 장착 관리\n0. 나가기\n"
            );

            int select = Utility.GetSelection(0, _subFeatures.Length);
            if (select == 0)
            {
                End();
                return;
            }

            _subFeatures[select - 1].Start(_player);
        }

    }
}
