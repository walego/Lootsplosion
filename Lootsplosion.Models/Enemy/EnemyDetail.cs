using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Models.Enemy
{
    public class EnemyDetail
    {
        public int EnemyId { get; set; }
        [Display(Name ="Name")]
        public string EnemyName { get; set; }
        [Display(Name ="Description")]
        public string EnemyDescription { get; set; }
        [Display(Name ="Boss")]
        public bool IsBoss { get; set; }
        [Display(Name ="Elite")]
        public bool IsElite { get; set; }
        public int Level { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public int Mobility { get; set; }
        [Display(Name ="Crit")]
        public int CritChance { get; set; }
        [Display(Name ="World Drop Pulls")]
        public int WorldPulls { get; set; }
        public string Skills { get; set; }
    }
}
