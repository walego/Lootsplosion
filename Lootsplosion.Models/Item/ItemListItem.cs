using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.Item
{
    public class ItemListItem
    {
        public int ItemId { get; set; }
        [Display(Name ="Name")]
        public string ItemName { get; set; }
        public Rarity Rarity { get; set; }
        [Display(Name ="Type")]
        public ItemType ItemType { get; set; }
        [Display(Name ="World Drop?")]
        public bool WorldDrop { get; set; }
    }
}
