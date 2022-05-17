using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.Loot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

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
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Loot.Add(entity);
                var saved = ctx.SaveChanges();
                var itemService = new ItemService(_userId);
                itemService.WorldDropCheck();
                if (model.WorldDrop == true)
                {
                    var worldSource = ctx.LootSources.Single(s => s.SourceType == SourceType.World && s.OwnerId == _userId);
                    var addToWorldSource = new LootPool()
                    {
                        LootId = entity.LootId,
                        LootSourceId = worldSource.LootSourceId,
                        SecretRarity = model.Rarity,
                        OwnerId = _userId,
                    };
                    ctx.LootPools.Add(addToWorldSource);
                    saved += ctx.SaveChanges();
                    return saved == 2;
                }
                return saved == 1;
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
                bool worldDropCheck = false;
                if (entity.WorldDrop != model.WorldDrop)
                {
                    worldDropCheck = true;
                    // Gotta add it to world source
                    var worldSource = ctx.LootSources.Single(s => s.SourceType == SourceType.World && s.OwnerId == _userId);
                    if (entity.WorldDrop == false)
                    {
                        var addToWorldSource = new LootPool()
                        {
                            LootId = entity.LootId,
                            LootSourceId = worldSource.LootSourceId,
                            SecretRarity = model.Rarity,
                            OwnerId = _userId,
                        };
                        ctx.LootPools.Add(addToWorldSource);
                    }
                    // Gotta delete current pool with loot
                    if (entity.WorldDrop == true)
                    {
                        var lootToRemove = ctx.LootPools.SingleOrDefault(p => p.LootId == entity.LootId && p.LootSourceId == worldSource.LootSourceId);
                        if (lootToRemove == default)
                        {
                            worldDropCheck = false;
                        }
                        else
                        {
                            ctx.LootPools.Remove(lootToRemove);
                        }
                    }
                }
                entity.LootName = model.LootName;
                entity.LootDescription = model.LootDescription;
                entity.Rarity = model.Rarity;
                entity.WorldDrop = model.WorldDrop;

                if(worldDropCheck)
                return ctx.SaveChanges() == 2;
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
