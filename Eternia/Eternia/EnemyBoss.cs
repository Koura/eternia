using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class EnemyBoss : Enemy
    {
        public EnemyBoss(float maxHealth, float armor, float damage, Dictionary<string, float> vulnerability) : base(maxHealth, armor, damage, vulnerability)
        {

        }
    }
}
