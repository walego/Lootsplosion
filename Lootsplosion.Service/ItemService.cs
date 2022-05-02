using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ItemId = ctx.Items.Last().ItemId,
                    LootName = model.ItemName,
                    Rarity = model.Rarity,
                    WorldDrop = model.WorldDrop,
                    // CHANGE THIS LATER
                    MasterList = true
                    // CHANGE THIS LATER
                };
                ctx.Loot.Add(newLoot);
                return ctx.SaveChanges() == 1;
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
                var entity = ctx.Items.Single(i => i.ItemId == id && i.OwnerId == _userId);
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
                oldLoot.LootName = model.ItemName;
                oldLoot.Rarity = model.Rarity;
                oldLoot.WorldDrop = model.WorldDrop;

                return ctx.SaveChanges() == 2;
            }
        }
        public bool DeleteItem(int id)
        {
            var lootService = new LootService(_userId);
            using(var ctx = new ApplicationDbContext())
            {
                var lootList = ctx.Loot.Where(l => l.ItemId == id).ToList();
                foreach(Loot loot in lootList)
                {
                    bool deleteCheck = lootService.DeleteLoot(loot.LootId);
                    if (!deleteCheck)
                        return false;
                }
                var entity = ctx.Items.Single(i => i.ItemId == id && i.OwnerId == _userId);
                ctx.Items.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
