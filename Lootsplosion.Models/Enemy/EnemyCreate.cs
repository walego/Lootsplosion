using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Models.Enemy
{
    public class EnemyCreate
    {
        [Required]
        [Display(Name = "Name")]
        public string EnemyName { get; set; }
        [Display(Name = "Description")]
        public string EnemyDescription { get; set; }
        [Required]
        [Display(Name = "Boss")]
        public bool IsBoss { get; set; }
        [Required]
        [Display(Name = "Elite")]
        public bool IsElite { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int Strength { get; set; }
        [Required]
        public int Intelligence { get; set; }
        [Required]
        public int Vitality { get; set; }
        [Required]
        public int Mobility { get; set; }
        [Required]
        [Display(Name = "Crit")]
        public int CritChance { get; set; }
        [Required]
        [Display(Name = "World Drop Pulls")]
        public int WorldPulls { get; set; }
        public string Skills { get; set; }
    }
}
