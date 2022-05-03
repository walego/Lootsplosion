using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Loostplosion.Data
{
    public class Loot
    {
        [Key]
        public int LootId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string LootName { get; set; }
        public string LootDescription { get; set; }
        [Required]
        public Rarity Rarity { get; set; }
        public bool WorldDrop { get; set; }
        public List<LootPool> PoolsIn { get; set; }
        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }
        public bool MasterList { get; set; }
    }
}
