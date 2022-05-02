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
        [Display(Name = "No Loot %")]
        public double NoLootWeight { get; set; }
        [Required]
        [Display(Name = "Common %")]
        public double CommonWeight { get; set; }
        [Required]
        [Display(Name = "Uncommon %")]
        public double UncommonWeight { get; set; }
        [Required]
        [Display(Name = "Rare %")]
        public double RareWeight { get; set; }
        [Required]
        [Display(Name = "Epic %")]
        public double EpicWeight { get; set; }
        [Required]
        [Display(Name = "Legendary %")]
        public double LegendaryWeight { get; set; }
        [Range(100,100)]
        public double WeightSum
        {
            get
            {
                return NoLootWeight+CommonWeight + UncommonWeight + RareWeight + EpicWeight + LegendaryWeight;
            }
        }
        [Required]
        [Display(Name = "Type")]
        public LootSourceType SourceType { get; set; }
        [Required]
        [Range(1, 10)]
        public int Pulls { get; set; }
    }
}
