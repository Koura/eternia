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
    abstract class Enemy : Being
    {
        public Enemy(float maxHealth, float armor, float damage, Dictionary<string, float> vulnerability) : base(maxHealth, armor, damage,vulnerability)
        {

        }
    }
}
