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
    public class LootPool
    {
        [Key]
        public int LootPoolId { get; set; }
        public Guid OwnerId { get; set; }
        [ForeignKey("Loot")]
        public int LootId { get; set; }
        public virtual Loot Loot { get; set; }
        [ForeignKey("LootSource")]
        public int LootSourceId { get; set; }
        public virtual LootSource LootSource { get; set; }
        public Rarity SecretRarity { get; set; }
        public bool MasterList { get; set; }
    }
}
