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
        public static EnemyBoss createBoss(Vector3 position, float maxHealth, float armor, float damage, Dictionary<string, float> vulnerability)
        {
            return new EnemyBoss(position, maxHealth, armor, damage, vulnerability);
        }
        public static EnemySoldier createSoldier(Vector3 position, float maxHealth, float armor, float damage, Dictionary<string, float> vulnerability)
        {
            return new EnemySoldier(position, maxHealth, armor, damage, vulnerability);
        }


    }
}
