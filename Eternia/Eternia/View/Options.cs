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
    public class Options : Screen
    {
        Rectangle arrowposi;
        Texture2D menuarrow;
        Texture2D background;
        SpriteFont font;

        private List<MenuOption> menuoptions = new List<MenuOption>();  

        int arrowValue = 1;

         public Options(Game game)
            : base(game)
        {            
           
        }

        public override void Initialize()

        {        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            background = game.Content.Load<Texture2D>("images/background");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 80), "End", font, Color.White));
            arrowposi = new Rectangle(game.GraphicsDevice.Viewport.Width / 3 + 10, game.GraphicsDevice.Viewport.Height / 2 + 59, menuarrow.Width / 16, menuarrow.Height / 16);
        }

        protected override void UnloadContent()
        {

        }

        protected override void ProcessInput(String message)
        {           
            if (message.Equals("accept"))
            {
                interpretAccept();
            }
        }

        private void interpretAccept()
        {
            //going back to mainmenu
            if (arrowValue == 1)
            {
                StateChanged("MainMenu");
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.SteelBlue);
            spriteBatch.Draw(menuarrow, arrowposi, Color.White);

            foreach (MenuOption option in menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
