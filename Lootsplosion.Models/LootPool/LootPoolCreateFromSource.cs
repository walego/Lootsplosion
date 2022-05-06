using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootPool
{
    public class LootPoolCreateFromSource
    {
        [Required]
        public int LootId { get; set; }
        [Required]
        [Display(Name = "Rarity Relative to Loot Source")]
        public Rarity SecretRarity { get; set; }
    }
}
