using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootSource
{
    public class LootSourceListItem
    {
        public int LootSourceId { get; set; }
        [Display(Name = "Loot Source Name")]
        public string SourceName { get; set; }
        [Display(Name = "C")]
        public double CommonWeight { get; set; }
        [Required]
        [Display(Name = "U")]
        public double UncommonWeight { get; set; }
        [Required]
        [Display(Name = "R")]
        public double RareWeight { get; set; }
        [Required]
        [Display(Name = "E")]
        public double EpicWeight { get; set; }
        [Required]
        [Display(Name = "L")]
        public double LegendaryWeight { get; set; }
        [Display(Name = "Type")]
        public LootSourceType SourceType { get; set; }
        public int Pulls { get; set; }
    }
}
