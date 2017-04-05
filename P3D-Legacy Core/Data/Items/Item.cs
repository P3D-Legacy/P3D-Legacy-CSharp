using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;

namespace P3D.Legacy.Core.Pokemon
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ItemAttribute : Attribute
    {
        public int Id { get; }
        public string Name { get; }

        public ItemAttribute(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }

    public struct ItemIdentifier
    {
        public string Name;
        public int Id;
    }

    public abstract class Item
    {
        /// <summary>
        /// The type of item. This is also the bag they get sorted into.
        /// </summary>
        public enum ItemTypes
        {
            /// <summary>
            /// The default item category for misc. items.
            /// </summary>
            Standard,
            /// <summary>
            /// Medicine items that restore Pokémon.
            /// </summary>
            Medicine,
            /// <summary>
            /// Plants, like berries and apricorns.
            /// </summary>
            Plants,
            /// <summary>
            /// All Poké Balls.
            /// </summary>
            Pokéballs,
            /// <summary>
            /// TMs and HMs.
            /// </summary>
            Machines,
            /// <summary>
            /// Keyitems of the game.
            /// </summary>
            KeyItems,
            /// <summary>
            /// Mail items.
            /// </summary>
            Mail,
            /// <summary>
            /// Items to be used in battle.
            /// </summary>
            BattleItems
        }


        private ItemAttribute _attribute;
        private ItemAttribute GetAttribute() => _attribute ?? (_attribute = (ItemAttribute)GetType().GetCustomAttributes(typeof(ItemAttribute), false)[0]);

        private Texture2D _texture;

        protected string TextureSource = Path.Combine("Items", "ItemSheet");
        protected Rectangle TextureRectangle;
        

        /// <summary>
        /// The singular item name.
        /// </summary>
        public virtual string Name => GetAttribute().Name;

        /// <summary>
        /// The ID of the item.
        /// </summary>
        public virtual int Id => GetAttribute().Id;

        /// <summary>
        /// The plural name of the item.
        /// </summary>
        public virtual string PluralName => Name + "s"; //Default plural name with "s" at the end.

        /// <summary>
        /// The texture of this item.
        /// </summary>
        public virtual Texture2D Texture => _texture ?? (_texture = TextureManager.GetTexture(TextureSource, TextureRectangle, ""));

        /// <summary>
        /// The price of this item if the player purchases it in exchange for PokéDollars. This halves when selling an item to the store.
        /// </summary>
        public virtual int PokeDollarPrice { get; } = 0;

        /// <summary>
        /// The price of this item if the player purchases it exchange for BattlePoints.
        /// </summary>
        public virtual int BattlePointsPrice { get; } = 1;

        /// <summary>
        /// The type of this item. This also controls in which bag this item gets sorted.
        /// </summary>
        public virtual ItemTypes ItemType { get; } = ItemTypes.Standard;

        /// <summary>
        /// The default catch multiplier if the item gets used as a Pokéball.
        /// </summary>
        public virtual float CatchMultiplier { get; } = 1.0F;

        /// <summary>
        /// The maximum amount of this item type (per ID) that can be stored in the bag.
        /// </summary>
        public virtual int MaxStack { get; } = 999;

        /// <summary>
        /// A value that can be used to sort items in the bag after. Lower values make items appear closer to the top.
        /// </summary>
        public virtual int SortValue { get; } = 0;

        /// <summary>
        /// The bag description of this item.
        /// </summary>
        public virtual string Description { get; } = "";

        /// <summary>
        /// The additional data that is stored with this item.
        /// </summary>
        public virtual string AdditionalData { get; set; } = "";

        /// <summary>
        /// The damage the Fling move does when this item is attached to a Pokémon.
        /// </summary>
        public virtual int FlingDamage { get; } = 30;

        /// <summary>
        /// If this item can be traded in for money.
        /// </summary>
        public virtual bool CanBeTraded { get; } = true;

        /// <summary>
        /// If this item can be given to a Pokémon.
        /// </summary>
        public virtual bool CanBeHold { get; } = true;

        /// <summary>
        /// If this item can be used from the bag.
        /// </summary>
        public virtual bool CanBeUsed { get; } = true;

        /// <summary>
        /// If this item can be used in battle.
        /// </summary>
        public virtual bool CanBeUsedInBattle { get; } = true;

        /// <summary>
        /// If this item can be tossed in the bag.
        /// </summary>
        public virtual bool CanBeTossed { get; } = true;

        /// <summary>
        /// If this item requires the player to select a Pokémon to use the item on in battle.
        /// </summary>
        public virtual bool BattleSelectPokemon { get; } = true;

        /// <summary>
        /// If this item is a Healing item.
        /// </summary>
        public virtual bool IsHealingItem { get; } = false;

        /// <summary>
        /// If this item is a Pokéball item.
        /// </summary>
        public virtual bool IsBall => GetType().IsSubclassOf(typeof(BallItem));

        /// <summary>
        /// If this item is a Berry item.
        /// </summary>
        public bool IsBerry => GetType().IsSubclassOf(typeof(Berry));

        /// <summary>
        /// If this item is a Mail item.
        /// </summary>
        public bool IsMail => GetType().IsSubclassOf(typeof(MailItem));

        /// <summary>
        /// If this item is a Mega Stone.
        /// </summary>
        public virtual bool IsMegaStone => GetType().IsSubclassOf(typeof(MegaStone));

        /// <summary>
        /// If this item is a Plate.
        /// </summary>
        public virtual bool IsPlate => GetType().IsSubclassOf(typeof(PlateItem));


        /// <summary>
        /// The item gets used from the bag.
        /// </summary>
        public virtual void Use() => Logger.Debug("PLACEHOLDER FOR ITEM USE");

        /// <summary>
        /// A method that gets used when the item is applied to a Pokémon. Returns True if the action was successful.
        /// </summary>
        /// <param name="pokeIndex">The Index of the Pokémon in party.</param>
        public virtual bool UseOnPokemon(int pokeIndex)
        {
            if (pokeIndex < 0 || pokeIndex > 5)
                throw new ArgumentOutOfRangeException(nameof(pokeIndex), pokeIndex, "The index for a Pokémon in a player's party can only be between 0 and 5.");

            Logger.Debug("PLACEHOLDER FOR ITEM USE ON POKEMON");
            return false;
        }

        /// <summary>
        /// Tries to remove a single item of this item type from the player's bag and returns a message which changes depending on if the item that got removed was the last one of its kind.
        /// </summary>
        public virtual string RemoveItem()
        {
            Core.Player.Inventory.RemoveItem(Id, 1);
            return Core.Player.Inventory.GetItemAmount(Id) == 0 ? "*There are no~" + PluralName + " left." : "";
        }


        /// <summary>
        /// The color for player dialogues.
        /// </summary>
        public static Color PlayerDialogueColor => new Color(0, 128, 227);


        protected static Dictionary<ItemIdentifier, Type> ItemBuffer { get; } = LoadItemBuffer();
        protected static Dictionary<ItemIdentifier, Type> LoadItemBuffer()
        {
            return Assembly.GetEntryAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(ItemAttribute), false).Length == 1)
                .ToDictionary(t =>
                {
                    var attr = (ItemAttribute) t.GetCustomAttributes(typeof(ItemAttribute), false)[0];
                    return new ItemIdentifier { Id = attr.Id, Name = attr.Name };
                }, t => t);
        }


        /// <summary>
        /// Returns an item instance based on the passed in ID.
        /// </summary>
        /// <param name="id">The desired item's ID.</param>
        public static Item GetItemByID(int id)
        {
            //if (ItemBuffer == null)
            //    ItemBuffer = LoadItemBuffer();

            var type = ItemBuffer.FirstOrDefault(itemTypePair => itemTypePair.Key.Id == id).Value;
            if (type != null)
                return (Item) Activator.CreateInstance(type);

            return null;
        }

        /// <summary>
        /// Returns an item based on its name.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        public static Item GetItemByName(string name)
        {
            var type = ItemBuffer.FirstOrDefault(itemTypePair => itemTypePair.Key.Name.ToLowerInvariant() == name.ToLowerInvariant()).Value;
            if (type != null)
                return (Item) Activator.CreateInstance(type);

            Logger.Log(Logger.LogTypes.Warning, "Item.vb: Cannot find item with the name \"" + name + "\".");
            return null;
        }
    }
}
