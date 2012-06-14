using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class Attack : IStrategy
    {
        private Being attacker;
        private Being target;

        public Attack(Being attacker,Being target)
        {
            this.attacker = attacker;
            this.target = target;
        }


        public void executeStrategy()
        {
            // calculate the damages to target
            float damage = attacker.Damage;
            float targetsNewHealth = target.CurrentHealth - damage;
            target.CurrentHealth = targetsNewHealth;
        }
    }
}
