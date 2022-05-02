using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.LootSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Service
{
    public class LootSourceService
    {
        private readonly Guid _userId;

        public LootSourceService(Guid userId)
        {
            _userId = userId;
        }
        public IEnumerable<LootSourceListItem> GetSources()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.LootSources.Where(s => s.OwnerId == _userId).Select(s => new LootSourceListItem
                {
                    LootSourceId = s.LootSourceId,
                    SourceName = s.SourceName,
                    SourceType = s.SourceType,
                    NoLootWeight = s.NoLootWeight,
                    CommonWeight = s.CommonWeight,
                    UncommonWeight = s.UncommonWeight,
                    RareWeight = s.RareWeight,
                    EpicWeight = s.EpicWeight,
                    LegendaryWeight = s.LegendaryWeight,
                    Pulls = s.Pulls
                });
                return query.ToArray();
            }
        }
        public LootSourceDetail GetSourceById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.LootSources.Single(s => s.LootSourceId == id && s.OwnerId == _userId);
                return new LootSourceDetail
                {
                    LootSourceId = entity.LootSourceId,
                    SourceName = entity.SourceName,
                    SourceDescription = entity.SourceDescription,
                    SourceType = entity.SourceType,
                    NoLootWeight = entity.NoLootWeight,
                    CommonWeight = entity.CommonWeight,
                    UncommonWeight = entity.UncommonWeight,
                    RareWeight = entity.RareWeight,
                    EpicWeight = entity.EpicWeight,
                    LegendaryWeight = entity.LegendaryWeight,
                    Pulls = entity.Pulls,
                };
            }
        }
        public bool DeleteSource(int id)
        {
            var poolService = new LootPoolService(_userId);
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootId == id).ToList();
                foreach (LootPool pool in poolList)
                {
                    bool deleteCheck = poolService.DeleteLootPool(pool.LootPoolId);
                    if (!deleteCheck)
                        return false;
                }
                var entity = ctx.LootSources.Single(l => l.LootSourceId == id && l.OwnerId == _userId);
                ctx.LootSources.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
