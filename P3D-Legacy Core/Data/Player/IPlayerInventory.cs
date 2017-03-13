using System.Collections.Generic;

using P3D.Legacy.Core.Pokemon;

namespace P3D.Legacy.Core.Interfaces
{
    public interface IPlayerInventory : IList<ItemContainer>
    {
        /// <summary>
        /// Returns a character that represents the item's pocket icon.
        /// </summary>
        string GetItemPocketChar(Item item);

        /// <summary>
        /// Adds items to the inventory.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <param name="amount">Amount of items to add.</param>

        void AddItem(int id, int amount);
        /// <summary>
        /// Removes items from the inventory.
        /// </summary>
        /// <param name="id">The ID of the item to remove.</param>
        /// <param name="amount">The amount of items to remove.</param>

        void RemoveItem(int id, int amount);
        /// <summary>
        /// Removes all items of an ID from the inventory.
        /// </summary>
        /// <param name="id">The ID of the item.</param>

        void RemoveItem(int id);
        /// <summary>
        /// Returns the count of the item in the inventory.
        /// </summary>
        /// <param name="id">The ID of the item to be counted.</param>
        int GetItemAmount(int id);

        /// <summary>
        /// If the player has the Running Shoes in their inventory.
        /// </summary>

        bool HasRunningShoes { get; }

        /// <summary>
        /// Returns a message that displays the event of putting an item into the inventory.
        /// </summary>
        /// <param name="item">The Item to store in the inventory.</param>
        /// <param name="amount">The amount.</param>
        string GetMessageReceive(Item item, int amount);
    }
}