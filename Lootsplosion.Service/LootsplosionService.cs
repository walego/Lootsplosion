using Lootsplosion.Data;
using Lootsplosion.Models.LootPull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Service
{
    public class LootsplosionService
    {
        private readonly Guid _userId;
        private readonly Random _random;

        public LootsplosionService(Guid userId)
        {
            _userId = userId;
            _random = new Random();
        }
        public List<PulledLootListItem> LootPull(LootPullSetup setup)
        {
            var lootList = new List<PulledLootListItem>();
            int pulls = 1;
            while (pulls <= setup.Pulls)
            {
                pulls++;
                var loot = new PulledLootListItem();
                int totalRandom = (int)(setup.NoLoot + setup.Common + setup.Uncommon + setup.Rare + setup.Epic + setup.Legendary);
                int pullNumber = _random.Next(0, totalRandom);
                if (setup.NoLoot < pullNumber && pullNumber < setup.NoLoot + setup.Common)
                    loot = CommonDrop(setup.LootSourceId);
                else if (setup.NoLoot < pullNumber && pullNumber < setup.NoLoot + setup.Common + setup.Uncommon)
                    loot = UncommonDrop(setup.LootSourceId);
                else if (setup.NoLoot < pullNumber && pullNumber < setup.NoLoot + setup.Common + setup.Uncommon + setup.Rare)
                    loot = RareDrop(setup.LootSourceId);
                else if (setup.NoLoot < pullNumber && pullNumber < setup.NoLoot + setup.Common + setup.Uncommon + setup.Rare + setup.Epic)
                    loot = EpicDrop(setup.LootSourceId);
                else if (setup.NoLoot < pullNumber && pullNumber < setup.NoLoot + setup.Common + setup.Uncommon + setup.Rare + setup.Epic + setup.Legendary)
                    loot = LegendaryDrop(setup.LootSourceId);
                lootList.Add(loot);
            }
            return lootList;
        }
        private PulledLootListItem CommonDrop(int sourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootSourceId == sourceId && p.OwnerId == _userId && p.SecretRarity == Rarity.Common).ToList();
                int listCount = poolList.Count();
                int lootPull = _random.Next(0, listCount);
                var loot = poolList[lootPull];
                return new PulledLootListItem
                {
                    LootId = loot.LootId,
                    LootName = loot.Loot.LootName,
                    Rarity = loot.Loot.Rarity
                };
            }
        }
        private PulledLootListItem UncommonDrop(int sourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootSourceId == sourceId && p.OwnerId == _userId && p.SecretRarity == Rarity.Uncommon).ToList();
                int listCount = poolList.Count();
                int lootPull = _random.Next(0, listCount);
                var loot = poolList[lootPull];
                return new PulledLootListItem
                {
                    LootId = loot.LootId,
                    LootName = loot.Loot.LootName,
                    Rarity = loot.Loot.Rarity
                };
            }
        }
        private PulledLootListItem RareDrop(int sourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootSourceId == sourceId && p.OwnerId == _userId && p.SecretRarity == Rarity.Rare).ToList();
                int listCount = poolList.Count();
                int lootPull = _random.Next(0, listCount);
                var loot = poolList[lootPull];
                return new PulledLootListItem
                {
                    LootId = loot.LootId,
                    LootName = loot.Loot.LootName,
                    Rarity = loot.Loot.Rarity
                };
            }
        }
        private PulledLootListItem EpicDrop(int sourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootSourceId == sourceId && p.OwnerId == _userId && p.SecretRarity == Rarity.Epic).ToList();
                int listCount = poolList.Count();
                int lootPull = _random.Next(0, listCount);
                var loot = poolList[lootPull];
                return new PulledLootListItem
                {
                    LootId = loot.LootId,
                    LootName = loot.Loot.LootName,
                    Rarity = loot.Loot.Rarity
                };
            }
        }
        private PulledLootListItem LegendaryDrop(int sourceId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var poolList = ctx.LootPools.Where(p => p.LootSourceId == sourceId && p.OwnerId == _userId && p.SecretRarity == Rarity.Legendary).ToList();
                int listCount = poolList.Count();
                int lootPull = _random.Next(0, listCount);
                var loot = poolList[lootPull];
                return new PulledLootListItem
                {
                    LootId = loot.LootId,
                    LootName = loot.Loot.LootName,
                    Rarity = loot.Loot.Rarity
                };
            }
        }
    }
}
