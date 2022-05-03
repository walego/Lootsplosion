using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loostplosion.Data
{
    public class Enemy
    {
        [Key]
        public int EnemyId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string EnemyName { get; set; }
        public string EnemyDescription { get; set; }
        [Required]
        public bool IsBoss { get; set; }
        [Required]
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
        public int CritChance { get; set; }
        [Required]
        public int WorldPulls { get; set; }
        public List<LootSource> LootSources { get; set; }
        public string Skills { get; set; }
        public bool MasterList { get; set; }
    }
}
