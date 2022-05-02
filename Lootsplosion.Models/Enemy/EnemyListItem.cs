using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Models.Enemy
{
    public class EnemyListItem
    {
        public int EnemyId { get; set; }
        [Display(Name = "Name")]
        public string EnemyName { get; set; }
        [Display(Name = "Boss")]
        public bool IsBoss { get; set; }
        [Display(Name = "Elite")]
        public bool IsElite { get; set; }
        [Required]
        public int Level { get; set; }
        [Display(Name = "World Drop Pulls")]
        public int WorldPulls { get; set; }
    }
}
