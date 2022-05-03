using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootSource
{
    public class LootSourceCreate
    {
        [Required]
        [Display(Name = "Loot Source Name")]
        public string SourceName { get; set; }
        [Display(Name = "Description")]
        public string SourceDescription { get; set; }
        [Required]
        [Display(Name = "No Loot Chance")]
        [Range(0,999999999)]
        public double NoLootWeight { get; set; }
        [Required]
        [Display(Name = "Common Chance")]
        [Range(0,999999999)]
        public double CommonWeight { get; set; }
        [Required]
        [Display(Name = "Uncommon Chance")]
        [Range(0,999999999)]
        public double UncommonWeight { get; set; }
        [Required]
        [Display(Name = "Rare Chance")]
        [Range(0,999999999)]
        public double RareWeight { get; set; }
        [Required]
        [Display(Name = "Epic Chance")]
        [Range(0,999999999)]
        public double EpicWeight { get; set; }
        [Required]
        [Display(Name = "Legendary Chance")]
        [Range(0,999999999)]
        public double LegendaryWeight { get; set; }
        [Required]
        [Display(Name = "Type")]
        public LootSourceType SourceType { get; set; }
        [Required]
        [Range(1, 10)]
        public int Pulls { get; set; }
    }
}
