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
        public int NoLootWeight { get; set; }
        [Required]
        public int CommonWeight { get; set; }
        [Required]
        public int UncommonWeight { get; set; }
        [Required]
        public int RareWeight { get; set; }
        [Required]
        public int EpicWeight { get; set; }
        [Required]
        public int LegendaryWeight { get; set; }
        [Required]
        public LootSourceType SourceType { get; set; }
        [ForeignKey("Enemy")]
        public int? EnemyId { get; set; }
        public virtual Enemy Enemy { get; set; }
        [Required]
        public int Pulls { get; set; }
        public bool MasterList { get; set; }
    }
}
