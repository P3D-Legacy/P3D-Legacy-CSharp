namespace P3D.Legacy.Core.Interfaces
{
    /// <summary>
    /// A secure class to contain ItemId and Amount.
    /// </summary>
    public class ItemContainer
    {
        public int ItemId { get; set; }

        public int Amount { get; set; }

        public ItemContainer(int itemId, int amount)
        {
            ItemId = itemId;
            Amount = amount;
        }

    }
}
