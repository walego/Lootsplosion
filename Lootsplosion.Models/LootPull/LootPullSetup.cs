using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Models.LootPull
{
    public class LootPullSetup
    {
        public double NoLoot { get; set; }
        public double Common { get; set; }
        public double Uncommon { get; set; }
        public double Rare { get; set; }
        public double Epic { get; set; }
        public double Legendary { get; set; }
        public double WeightMultiplier { get; set; }
        public int Pulls { get; set; }
        public int LootSourceId { get; set; }
    }
}
