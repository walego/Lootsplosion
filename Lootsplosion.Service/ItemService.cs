using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Service
{
    public class ItemService
    {
        private readonly Guid _userId;

        public ItemService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateItem(ItemCreate model)
        {
            var entity = new Item()
            {
                OwnerId = _userId,
                ItemName = model.ItemName,
                ItemDescription = model.ItemDescription,
                Rarity = model.Rarity,
                ItemType = model.ItemType,
                Strength = model.Strength,
                Intelligence = model.Intelligence,
                Vitality = model.Vitality,
                Mobility = model.Mobility,
                CritChance = model.CritChance,
                OtherEffects = model.OtherEffects,
                WorldDrop = model.WorldDrop,
                // CHANGE THIS LATER
                MasterList = true
                // CHANGE THIS LATER
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                ctx.SaveChanges();
                var newLoot = new Loot()
                {
                    OwnerId = _userId,
                    ItemId = entity.ItemId,
                    LootName = model.ItemName,
                    LootDescription = model.ItemDescription,
                    Rarity = model.Rarity,
                    WorldDrop = model.WorldDrop,
                    // CHANGE THIS LATER
                    MasterList = true
                    // CHANGE THIS LATER
                };
                ctx.Loot.Add(newLoot);
                var saved = ctx.SaveChanges();
                WorldDropCheck();
                if(model.WorldDrop==true)
                {
                    var worldSource = ctx.LootSources.Single(s => s.SourceType == SourceType.World && s.OwnerId == _userId);
                    var addToWorldSource = new LootPool()
                    {
                        LootId = newLoot.LootId,
                        LootSourceId = worldSource.LootSourceId,
                        SecretRarity = model.Rarity,
                        OwnerId = _userId,
                        // CHANGE THIS LATER
                        MasterList = true
                        // CHANGE THIS LATER
                    };
                    ctx.LootPools.Add(addToWorldSource);
                    saved += ctx.SaveChanges();
                    return saved == 2;
                }
                return saved == 1;
            }
        }
        public IEnumerable<ItemListItem> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Items.Where(i => i.OwnerId == _userId).Select(i => new ItemListItem
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    Rarity = i.Rarity,
                    ItemType = i.ItemType,
                    WorldDrop = i.WorldDrop
                });
                return query.ToArray();
            }
        }
        public ItemDetail GetItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.SingleOrDefault(i => i.ItemId == id && i.OwnerId == _userId);
                if (entity == default)
                    return new ItemDetail { ItemId = -1 };
                return new ItemDetail
                {
                    ItemId = entity.ItemId,
                    ItemName = entity.ItemName,
                    ItemDescription = entity.ItemDescription,
                    Rarity = entity.Rarity,
                    ItemType = entity.ItemType,
                    Strength = entity.Strength,
                    Intelligence = entity.Intelligence,
                    Vitality = entity.Vitality,
                    Mobility = entity.Mobility,
                    CritChance = entity.CritChance,
                    OtherEffects = entity.OtherEffects,
                    WorldDrop = entity.WorldDrop
                };
            }
        }
        public bool UpdateItem(ItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.Single(i => i.ItemId == model.ItemId && i.OwnerId == _userId);
                entity.ItemName = model.ItemName;
                entity.ItemDescription = model.ItemDescription;
                entity.Rarity = model.Rarity;
                entity.ItemType = model.ItemType;
                entity.Strength = model.Strength;
                entity.Intelligence = model.Intelligence;
                entity.Vitality = model.Vitality;
                entity.Mobility = model.Mobility;
                entity.CritChance = model.CritChance;
                entity.OtherEffects = model.OtherEffects;
                entity.WorldDrop = model.WorldDrop;

                var oldLoot = ctx.Loot.Single(l => l.ItemId == model.ItemId && l.OwnerId == _userId);
                if (oldLoot.LootName == model.ItemName && oldLoot.LootDescription == model.ItemDescription && oldLoot.Rarity == model.Rarity && oldLoot.WorldDrop == model.WorldDrop)
                    return ctx.SaveChanges() == 1;
                oldLoot.LootName = model.ItemName;
                oldLoot.LootDescription = model.ItemDescription;
                oldLoot.Rarity = model.Rarity;
                oldLoot.WorldDrop = model.WorldDrop;

                return ctx.SaveChanges() == 2;
            }
        }
        public bool DeleteItem(int id)
        {
            var lootService = new LootService(_userId);
            using (var lootCtx = new ApplicationDbContext())
            {
                var lootList = lootCtx.Loot.Where(l => l.ItemId == id).ToList();
                foreach (Loot loot in lootList)
                {
                    bool deleteCheck = lootService.DeleteLoot(loot.LootId);
                    if (!deleteCheck)
                        return false;
                }
            }
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.Single(i => i.ItemId == id && i.OwnerId == _userId);
                ctx.Items.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public void WorldDropCheck()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var worldCheck = ctx.LootSources.SingleOrDefault(s => s.SourceType == SourceType.World && s.OwnerId == _userId);
                if (worldCheck == default)
                {
                    var newSource = new LootSource()
                    {
                        OwnerId = _userId,
                        SourceName = $"World Drop",
                        SourceType = SourceType.World,
                        NoLootWeight = 25,
                        CommonWeight = 35,
                        UncommonWeight = 24,
                        RareWeight = 10,
                        EpicWeight = 5,
                        LegendaryWeight = 1,
                        Pulls = 0,
                        // CHANGE THIS LATER
                        MasterList = true
                        // CHANGE THIS LATER
                    };
                    ctx.LootSources.Add(newSource);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
