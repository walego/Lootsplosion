using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.Loot
{
    public class LootDetail
    {
        public int LootId { get; set; }
        [Display(Name = "Name")]
        public string LootName { get; set; }
        public string LootDescription { get; set; }
        public Rarity Rarity { get; set; }
        [Display(Name = "World Drop?")]
        public bool WorldDrop { get; set; }
        [Display(Name = "# of Loot Pools")]
        public int PoolsIn { get; set; }
    }
}
