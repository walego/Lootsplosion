using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lootsplosion.Common
{
    public class EnumCollection
    {
        public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
        public enum ItemType { Weapon, Helmet, Armor, Gloves, Boots, Ring, Amulet, Accessory, Consumable, OffHand, Other }
        public enum LootSourceType { Enemy, Chest, Quest, World, Elite, Boss, Other }
        public SelectList GetRarities()
        {
            var rarities = from Rarity r in Enum.GetValues(typeof(Rarity)) select new { Id = (int)r, Name = r.ToString() };
            return new SelectList(rarities, "Id", "Name", "Rarity");
        }
        public SelectList GetItemTypes()
        {
            var types = from ItemType t in Enum.GetValues(typeof(ItemType)) select new { Id = (int)t, Name = t.ToString() };
            return new SelectList(types, "Id", "Name", "Type");
        }
    }
}
