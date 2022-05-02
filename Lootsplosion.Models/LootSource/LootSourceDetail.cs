using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootSource
{
    public class LootSourceDetail
    {
        public int LootSourceId { get; set; }
        [Display(Name = "Loot Source Name")]
        public string SourceName { get; set; }
        [Display(Name = "Description")]
        public string SourceDescription { get; set; }
        [Display(Name = "No Loot %")]
        public double NoLootWeight { get; set; }
        [Display(Name = "Common %")]
        public double CommonWeight { get; set; }
        [Display(Name = "Uncommon %")]
        public double UncommonWeight { get; set; }
        [Display(Name = "Rare %")]
        public double RareWeight { get; set; }
        [Display(Name = "Epic %")]
        public double EpicWeight { get; set; }
        [Display(Name = "Legendary %")]
        public double LegendaryWeight { get; set; }
        [Display(Name = "Type")]
        public LootSourceType SourceType { get; set; }
        public int Pulls { get; set; }
    }
}
