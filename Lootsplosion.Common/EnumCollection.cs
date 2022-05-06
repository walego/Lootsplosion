using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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
            var list = new SelectList(rarities, "Id", "Name", "Rarity");
            return list;
        }
        public SelectList GetItemTypes()
        {
            var types = from ItemType t in Enum.GetValues(typeof(ItemType)) select new { Id = (int)t, Name = t.ToString() };
            var list = new SelectList(types, "Id", "Name", "Type");
            return list;
        }
        public SelectList GetLootSourceTypes()
        {
            var types = from LootSourceType t in Enum.GetValues(typeof(LootSourceType)) select new { Id = (int)t, Name = t.ToString() };
            var list = new SelectList(types, "Id", "Name", "Type");
            return list;
        }
    }
}
