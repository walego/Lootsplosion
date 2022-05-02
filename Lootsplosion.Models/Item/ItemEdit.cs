using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.Item
{
    public class ItemEdit
    {
        public int ItemId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }
        [Required]
        public Rarity Rarity { get; set; }
        [Display(Name = "Type")]
        public ItemType ItemType { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public double Mobility { get; set; }
        [Display(Name = "Crit")]
        public int CritChance { get; set; }
        [Display(Name = "Other Effects")]
        public string OtherEffects { get; set; }
        [Display(Name = "World Drop?")]
        public bool WorldDrop { get; set; }
    }
}
