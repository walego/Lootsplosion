using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootSource
{
    public class LootSourceEdit
    {
        public int LootSourceId { get; set; }
        [Required]
        [Display(Name = "Loot Source Name")]
        public string SourceName { get; set; }
        [Display(Name = "Description")]
        public string SourceDescription { get; set; }
        [Required]
        [Display(Name = "Chance of No Loot")]
        public double NoLootWeight { get; set; }
        [Required]
        [Display(Name = "Common Chance")]
        public double CommonWeight { get; set; }
        [Required]
        [Display(Name = "Uncommon Chance")]
        public double UncommonWeight { get; set; }
        [Required]
        [Display(Name = "Rare Chance")]
        public double RareWeight { get; set; }
        [Required]
        [Display(Name = "Epic Chance")]
        public double EpicWeight { get; set; }
        [Required]
        [Display(Name = "Legendary Chance")]
        public double LegendaryWeight { get; set; }
        [Required]
        [Display(Name = "Type")]
        public LootSourceType SourceType { get; set; }
        public int? EnemyId { get; set; }
        [Required]
        [Range(1, 10)]
        public int Pulls { get; set; }
    }
}
