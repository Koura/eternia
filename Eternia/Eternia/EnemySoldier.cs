using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class EnemySoldier : Enemy
    {
        public EnemySoldier(float maxHealth, float armor, float damage,Dictionary<string, float> vulnerability)
            : base(maxHealth, armor, damage, vulnerability)
        {

        }
    }
}
