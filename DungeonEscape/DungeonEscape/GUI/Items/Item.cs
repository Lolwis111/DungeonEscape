namespace DungeonEscape.GUI.Items
{
    public struct Item
    {
        public ItemType Type;
        public int Id;

        public Item(ItemType type, int id)
        {
            Id = id;
            Type = type;
        }
    }

}
