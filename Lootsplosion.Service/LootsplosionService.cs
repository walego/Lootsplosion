using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lootsplosion.Service
{
    public class LootsplosionService
    {
        private readonly Guid _userId;

        public LootsplosionService(Guid userId)
        {
            _userId = userId;
        }
    }
}
