using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Common
{
    public class EnumCollection
    {
        public enum Rarity { NoDrop, Common, Uncommon, Rare, Epic, Legendary }
        public enum ItemType { Weapon, Helmet, Armor, Gloves, Boots, Ring, Amulet, Accessory, Consumable, OffHand, Other }
        public enum LootSourceType { Enemy, Chest, Quest, World, Elite, Boss, Other }
    }
}
