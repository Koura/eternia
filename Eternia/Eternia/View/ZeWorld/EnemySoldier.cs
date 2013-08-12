using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternia
{
    class EnemySoldier : Enemy
    {
        private int gold;

        public EnemySoldier(String name, Vector3 position,float maxHealth, float armor, float damage, int xp, int gold)
            : base(name, position, maxHealth, armor, damage, xp)
        {
            this.gold = gold;
        }

        public override int getGold()
        {
            return this.gold;
        }
    }
}
