using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.Loot
{
    public class LootCreate
    {
        [Required]
        public string LootName { get; set; }
        public string LootDescription { get; set; }
        [Required]
        public Rarity Rarity { get; set; }
        public bool WorldDrop { get; set; }
    }
}
