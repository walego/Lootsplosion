using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Models.LootPull
{
    public class PulledLootListItem
    {
        public int LootId { get; set; }
        public string LootName { get; set; }
        public Rarity Rarity { get; set; }
    }
}
