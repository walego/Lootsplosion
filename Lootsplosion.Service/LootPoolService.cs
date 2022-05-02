﻿using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.LootPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Service
{
    public class LootPoolService
    {
        private readonly Guid _userId;

        public LootPoolService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateLootPool(LootPoolCreate model)
        {
            var entity = new LootPool()
            {
                OwnerId = _userId,
                LootId = model.LootId,
                LootSourceId = model.LootSourceId,
                SecretRarity = model.SecretRarity
            };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.LootPools.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool UpdateLootPool(LootPoolEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.LootPools.Single(p => p.LootPoolId == model.LootPoolId && p.OwnerId == _userId);
                entity.LootId = model.LootId;
                entity.LootSourceId = model.LootSourceId;
                entity.SecretRarity = model.SecretRarity;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteLootPool(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.LootPools.Single(p => p.LootPoolId == id && p.OwnerId == _userId);
                ctx.LootPools.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}