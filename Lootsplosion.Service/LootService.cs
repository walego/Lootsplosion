﻿using Loostplosion.Data;
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
        public LootListItem GetLootById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Loot.Single(l => l.LootId == id && l.OwnerId == _userId);
                return new LootListItem
                {
                    LootId = entity.LootId,
                    LootName = entity.LootName,
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
                entity.Rarity = model.Rarity;
                entity.WorldDrop = model.WorldDrop;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteLoot(int id)
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
                var entity = ctx.Loot.Single(l => l.LootId == id && l.OwnerId == _userId);
                ctx.Loot.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
