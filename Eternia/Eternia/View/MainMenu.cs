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
       
        Texture2D menuarrow;
        public MainMenu(Game game)
            : base(game)
        {
            
            // Do we actually need this constructor?
        }

        public override void Initialize()

        {
            // Do our MainMenu component creation magicks here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
        }

        protected override void UnloadContent()
        {

        }
        // Do we need both of these?
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuarrow, new Rectangle(50,50,menuarrow.Width, menuarrow.Height), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
