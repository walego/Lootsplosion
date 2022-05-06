using Loostplosion.Data;
using Lootsplosion.Data;
using Lootsplosion.Models.Enemy;
using Lootsplosion.Models.LootSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lootsplosion.Common.EnumCollection;

namespace Lootsplosion.Service
{
    public class EnemyService
    {
        private readonly Guid _userId;

        public EnemyService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateEnemy(EnemyCreate model)
        {
            var entity = new Enemy()
            {
                OwnerId = _userId,
                EnemyName = model.EnemyName,
                EnemyDescription = model.EnemyDescription,
                IsBoss = model.IsBoss,
                IsElite = model.IsElite,
                Level = model.Level,
                Strength = model.Strength,
                Intelligence = model.Intelligence,
                Vitality = model.Vitality,
                Mobility = model.Mobility,
                CritChance = model.CritChance,
                Skills = model.Skills,
                WorldPulls = model.WorldPulls,
                // CHANGE THIS LATER
                MasterList = true
                // CHANGE THIS LATER
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Enemies.Add(entity);
                if (!model.AddNewLootSource)
                    return ctx.SaveChanges() == 1;
                int saved = ctx.SaveChanges();
                var newSource = new LootSource()
                {
                    OwnerId = _userId,
                    EnemyId = entity.EnemyId,
                    SourceName = $"{model.EnemyName} Main",
                    SourceType = LootSourceType.Enemy,
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
                saved += ctx.SaveChanges();
                return saved == 2;
            }
        }
        public IEnumerable<EnemyListItem> GetEnemies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Enemies.Where(e => e.OwnerId == _userId).Select(e => new EnemyListItem
                {
                    EnemyId = e.EnemyId,
                    EnemyName = e.EnemyName,
                    Level = e.Level,
                    IsBoss = e.IsBoss,
                    IsElite = e.IsElite,
                    WorldPulls = e.WorldPulls
                });
                return query.ToArray();
            }
        }
        public EnemyDetail GetEnemyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Enemies.Single(e => e.EnemyId == id && e.OwnerId == _userId);
                return new EnemyDetail
                {
                    EnemyId = entity.EnemyId,
                    EnemyName = entity.EnemyName,
                    EnemyDescription = entity.EnemyDescription,
                    IsBoss = entity.IsBoss,
                    IsElite = entity.IsElite,
                    Level = entity.Level,
                    Strength = entity.Strength,
                    Intelligence = entity.Intelligence,
                    Vitality = entity.Vitality,
                    Mobility = entity.Mobility,
                    CritChance = entity.CritChance,
                    Skills = entity.Skills,
                    WorldPulls = entity.WorldPulls
                };
            }
        }
        public bool UpdateEnemy(EnemyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Enemies.Single(e => e.EnemyId == model.EnemyId && e.OwnerId == _userId);
                entity.EnemyName = model.EnemyName;
                entity.EnemyDescription = model.EnemyDescription;
                entity.IsBoss = model.IsBoss;
                entity.IsElite = model.IsElite;
                entity.Level = model.Level;
                entity.Strength = model.Strength;
                entity.Intelligence = model.Intelligence;
                entity.Vitality = model.Vitality;
                entity.Mobility = model.Mobility;
                entity.CritChance = model.CritChance;
                entity.Skills = model.Skills;
                entity.WorldPulls = model.WorldPulls;

                var oldSource = ctx.LootSources.FirstOrDefault(l => l.EnemyId == model.EnemyId && l.OwnerId == _userId);
                if (oldSource != default)
                {
                    oldSource.SourceName = $"{entity.EnemyName} Main";
                    return ctx.SaveChanges() == 2;
                }
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteEnemy(int id)
        {
            var sourceService = new LootSourceService(_userId);
            using (var sourceCtx = new ApplicationDbContext())
            {
                var sourceList = sourceCtx.LootSources.Where(l => l.EnemyId == id).ToList();
                foreach (LootSource source in sourceList)
                {
                    bool deleteCheck = sourceService.DeleteSource(source.LootSourceId);
                    if (!deleteCheck)
                        return false;
                }
            }
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Enemies.Single(e => e.EnemyId == id && e.OwnerId == _userId);
                ctx.Enemies.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
