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
    public class LootSource
    {
        [Key]
        public int LootSourceId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string SourceName { get; set; }
        public string SourceDescription { get; set; }
        [Required]
        public double NoLootWeight { get; set; }
        [Required]
        public double CommonWeight { get; set; }
        [Required]
        public double UncommonWeight { get; set; }
        [Required]
        public double RareWeight { get; set; }
        [Required]
        public double EpicWeight { get; set; }
        [Required]
        public double LegendaryWeight { get; set; }
        [Required]
        public SourceType SourceType { get; set; }
        public List<LootPool> AttachedPools { get; set; }
        [ForeignKey("Enemy")]
        public int? EnemyId { get; set; }
        public virtual Enemy Enemy { get; set; }
        [Required]
        public int Pulls { get; set; }
        public bool MasterList { get; set; }
    }
}
