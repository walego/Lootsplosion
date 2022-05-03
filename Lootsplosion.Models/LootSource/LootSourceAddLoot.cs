using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Models.LootSource
{
    public class LootSourceAddLoot
    {
        public string LootSourceName { get; set; }
        [Display(Name ="Item")]
        public int LootId { get; set; }
        [Display(Name ="Add all Common items?")]
        public bool AddAllCommons { get; set; }
        [Display(Name ="Add all Uncommon items?")]
        public bool AddAllUncommons { get; set; }
        [Display(Name ="Add all Rare items?")]
        public bool AddAllRares { get; set; }
        [Display(Name ="Add all Epic items?")]
        public bool AddAllEpics { get; set; }
        [Display(Name ="Add all Legendary items?")]
        public bool AddAllLegendaries { get; set; }
    }
}
