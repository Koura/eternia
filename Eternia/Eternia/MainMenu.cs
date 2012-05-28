using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Eternia
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainMenu : Screen
    {
        public MainMenu(Game game)
            : base(game)
        {
            // Do we actually need this constructor?
        }

        public override void Initialize()
        {
            // Do our MainMenu component creation magicks here
        }

        
        
        // Do we need both of these?
        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // base.Draw(gameTime);
        }
    }
}
