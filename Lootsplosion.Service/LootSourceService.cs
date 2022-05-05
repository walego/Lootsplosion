﻿using Loostplosion.Data;
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
        public bool CreateSource(LootSourceCreate model)
        {
            var entity = new LootSource()
            {
                OwnerId = _userId,
                SourceName = model.SourceName,
                SourceDescription = model.SourceDescription,
                SourceType = model.SourceType,
                NoLootWeight = model.NoLootWeight,
                CommonWeight = model.CommonWeight,
                UncommonWeight = model.UncommonWeight,
                RareWeight = model.RareWeight,
                EpicWeight = model.EpicWeight,
                LegendaryWeight = model.LegendaryWeight,
                Pulls = model.Pulls,
                // CHANGE THIS LATER
                MasterList = true
                // CHANGE THIS LATER
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.LootSources.Add(entity);
                return ctx.SaveChanges() == 1;
            }
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
            using (var ctx = new ApplicationDbContext())
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
                var entity = ctx.LootSources.Single(l => l.LootSourceId == id && l.OwnerId == _userId);
                ctx.LootSources.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public List<double> RarityWeightCalculationsForRandom(int id)
        {
            var model = GetSourceById(id);
            bool percentToInt = true;
            double n = model.NoLootWeight;
            double c = model.CommonWeight;
            double u = model.UncommonWeight;
            double r = model.RareWeight;
            double e = model.EpicWeight;
            double l = model.LegendaryWeight;
            while (percentToInt)
            {
                //Using modulo to see if any weights have decimals
                if (n % 1 != 0 || c % 1 != 0 || u % 1 != 0 || r % 1 != 0 || e % 1 != 0 || l % 1 != 0)
                {
                    //Multiply all by 10 and loop again
                    n *= 10;
                    c *= 10;
                    u *= 10;
                    r *= 10;
                    e *= 10;
                    l *= 10;
                }
                //Once if statement passes calculate multiplier to calculate percent chance and end loop
                else
                {
                    percentToInt = false;
                }
            }
            double weightMultiplier = (n + c + u + r + e + l) / 100;
            List<double> rarityWithMultiplier = new List<double> { n, c, u, r, e, l, weightMultiplier };
            return rarityWithMultiplier;
        }
    }
}
