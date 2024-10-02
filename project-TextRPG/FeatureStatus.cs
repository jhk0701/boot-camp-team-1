namespace project_TextRPG
{
    internal class FeatureStatus : Feature
    {

        public FeatureStatus(string featureName, IScene scene)
        {
            _scene = scene;
            Name = featureName;
        }

        public override void ShowMenu()
        {
            string clName = DataDefinition.GetInstance().ClassInitDatas[(int)_player.CharClass].name;
            float eAtk = _player.EquipAttack;
            float eDef = _player.EquipDefense;
            float eHp = _player.EquipHealth;
            float eMp = _player.EquipMana;

            Utility.WriteColorScript(Name, ConsoleColor.Yellow);
            Utility.ShowScript(
                "캐릭터의 정보가 표시됩니다.\n\n",

                $"Lv. {_player.Level:0#} (Exp. {_player.Exp})\n",
                $"이름 : {_player.Name} ( {clName} )\n",
                $"공격력(ATK) : {_player.Attack} ({(eAtk >= 0 ? $"+{eAtk}" : eAtk)})\n",
                $"방어력(DEF) : {_player.Defense} ({(eDef >= 0 ? $"+{eDef}" : eDef)})\n",
                $"체력(Hp) : {_player.Health} / {_player.MaxHealth + eHp} ({(eHp >= 0 ? $"+{eHp}" : eHp)})\n",
                $"마나(Mp) : {_player.Mana} / {_player.MaxMana + eMp} ({(eMp >= 0 ? $"+{eMp}" : eMp)})\n",
                $"Gold : {_player.Gold} G\n\n",

                "0. 나가기\n"
            );            
        }

        public override void Act()
        {
            Utility.GetSelection(0, 0);
            End();
        }
    }
}
