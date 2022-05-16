using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootPool
{
    public class LootPoolListItem
    {
        [Display(Name ="Id")]
        public int LootPoolId { get; set; }
        [Display(Name ="Loot Info")]
        public int LootId { get; set; }
        [Display(Name ="")]
        public string LootName { get; set; }
        [Display(Name ="Source Info")]
        public int LootSourceId { get; set; }
        [Display(Name ="")]
        public string LootSourceName { get; set; }
        [Display(Name = "Rarity Relative to Loot Source")]
        public Rarity SecretRarity { get; set; }
        public bool WorldDrop { get; set; }
    }
}
