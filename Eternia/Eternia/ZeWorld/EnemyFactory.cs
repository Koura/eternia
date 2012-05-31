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
    class EnemyFactory
    {
        public static EnemyBoss createBoss(String name,Vector3 position, float maxHealth, float armor, float damage)
        {
            return new EnemyBoss(name,position, maxHealth, armor, damage);
        }
        public static EnemySoldier createSoldier(String name,Vector3 position, float maxHealth, float armor, float damage)
        {
            return new EnemySoldier(name,position, maxHealth, armor, damage);
        }
       

    }
}
