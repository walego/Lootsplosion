using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Service
{
    public class LootService
    {
        private readonly Guid _userId;

        public LootService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateLoot(LootCreate model)
        {
            var entity = new Loot()
            {
                OwnerId = _userId,
                LootName = model.LootName,
                LootDescription = model.LootDescription,
                Rarity = model.Rarity,
                WorldDrop = model.WorldDrop,
                // CHANGE THIS LATER
                MasterList = true
                // CHANGE THIS LATER
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Loot.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<LootListItem> GetAllLoot()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Loot.Where(l => l.OwnerId == _userId).Select(l => new LootListItem
                {
                    LootId = l.LootId,
                    LootName = l.LootName,
                    Rarity = l.Rarity,
                    WorldDrop = l.WorldDrop,
                    PoolsIn = ctx.LootPools.Where(p => p.LootId == l.LootId).Count()
                }).ToList();
                query.OrderBy(l => l.Rarity).ToList();
                return query;
            }
        }
        public IEnumerable<LootDescription> GetLootDescriptions()
        {
            var lootList = GetAllLoot();
            var descriptions = new List<LootDescription>();
            foreach (var loot in lootList)
            {
                var lootDesc = new LootDescription
                {
                    LootId = loot.LootId,
                    Loot = $"{loot.LootName} {loot.Rarity.ToString().ToUpper()}"
                };
                descriptions.Add(lootDesc);
            }
            return descriptions;
        }
        public LootDetail GetLootById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Loot.SingleOrDefault(l => l.LootId == id && l.OwnerId == _userId);
                if (entity == default)
                    return new LootDetail { LootId = -1 };
                return new LootDetail
                {
                    LootId = entity.LootId,
                    LootName = entity.LootName,
                    LootDescription = entity.LootDescription,
                    Rarity = entity.Rarity,
                    WorldDrop = entity.WorldDrop,
                    PoolsIn = ctx.LootPools.Where(p => p.LootId == entity.LootId).Count()
                };
            }
        }
        public bool UpdateLoot(LootEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Loot.Single(l => l.LootId == model.LootId && l.OwnerId == _userId);
                entity.LootName = model.LootName;
                entity.LootDescription = model.LootDescription;
                entity.Rarity = model.Rarity;
                entity.WorldDrop = model.WorldDrop;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteLoot(int id)
        {
            var poolService = new LootPoolService(_userId);
            using (var poolCtx = new ApplicationDbContext())
            {
                var poolList = poolCtx.LootPools.Where(p => p.LootId == id).ToList();
                foreach (LootPool pool in poolList)
                {
                    bool deleteCheck = poolService.DeleteLootPool(pool.LootPoolId);
                    if (!deleteCheck)
                        return false;
                }
            }
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Loot.Single(l => l.LootId == id && l.OwnerId == _userId);
                ctx.Loot.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
