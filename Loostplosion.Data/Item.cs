using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Loostplosion.Data
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        [Required]
        public Rarity Rarity { get; set; }
        public ItemType ItemType { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public double Mobility { get; set; }
        public int CritChance { get; set; }
        public string OtherEffects { get; set; }
        public bool WorldDrop { get; set; }
        public bool MasterList { get; set; }
    }
}
