namespace project_TextRPG
{
    internal class FeatureInventoryEquip : Feature
    {
        public FeatureInventoryEquip(string featureName, IScene scene)
        {
            _scene = scene;
            _name = featureName;
        }

        public override void ShowMenu()
        {
            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "보유 중인 아이템을 장착할 수 있습니다.\n\n",

                "[아이템 목록]\n"
            );

            for (int i = 0; i < _player.Inventory.Items.Length; i++)
            {
                Equipment item = _player.Inventory.Items[i];

                Utility.ShowScript(
                    $"{i + 1}. ",
                    _player.Inventory.IsEquipped(item) ? "[E]" : "",
                    $"{item.Name,-15} | {item.GetBonusSpec(),-15} | {item.Description}"
                );
            }

            Console.WriteLine();
        }

        public override void Act()
        {
            Equipment[] e = _player.Inventory.Items;
            int select = Utility.GetSelection(0, e.Length);

            if (select == 0) 
            {
                End();
                return;
            }

            // 장착
            if (!_player.Inventory.IsEquipped(e[select - 1]))
                _player.Inventory.Equip(e[select - 1]);
            else // 장착 해제
                _player.Inventory.Unequip(e[select - 1]);


            Console.Clear();
            ShowMenu();
            Act();
        }
    }
}
