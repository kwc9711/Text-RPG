namespace TextRPG
{
    public class ShopItem
    {
        public Item ItemData { get; set; }
        public int Price { get; set; }
        public bool IsPurchased { get; set; } = false;

        public string GetDisplayInfo(int index)
        {
            string statText = ItemData.Type == ItemType.Weapon ? $"공격력 +{ItemData.Value}" : $"방어력 +{ItemData.Value}";
            string rightText = IsPurchased ? "구매완료" : $"{Price} G";
            return $"- {index + 1}. {ItemData.Name,-12} | {statText,-10} | {ItemData.Description,-30} | {rightText}";
        }
    }
}
