namespace TextRPG
{

    public enum ItemType
    {
        Weapon,
        Armor
    }

    public class Item
    {
        public string Name { get; set; } = "";
        public ItemType Type { get; set; }
        public int Value { get; set; }
        public string Description { get; set; } = "";
        public bool IsEquipped { get; set; } = false;

        public string GetDisplayInfo()
        {
            string equippedMark = IsEquipped ? "[E]" : "   ";
            string statText = Type == ItemType.Weapon ? $"공격력 +{Value}" : $"방어력 +{Value}";
            return $"- {equippedMark}{Name,-12} | {statText,-10} | {Description}";
        }
    }
}
