using Loostplosion.Data;
using Lootsplosion.Data;
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
