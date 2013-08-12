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
    public class Defeated : Screen
    {

        private Texture2D menuarrow;
        private List<MenuOption> menuoptions = new List<MenuOption>();
        SpriteFont font;
        Rectangle arrowposi;
        int arrowValue = 1;
        public Defeated(Game game)
            : base(game)
        {
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
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2-GraphicsDevice.Viewport.Height/15), "Load Game", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2), "Exit", font, Color.White));
            arrowposi = new Rectangle(game.GraphicsDevice.Viewport.Width / 2 - 122, game.GraphicsDevice.Viewport.Height / 2 - 21 - GraphicsDevice.Viewport.Height / 15, menuarrow.Width / 16, menuarrow.Height / 16);

        }

        protected override void UnloadContent()
        {
        }
        //processes the messages gotten from the player and acts accordingly.
        protected override void ProcessInput(String message)
        {           
            if (message.Equals("up"))
            {
                if (arrowValue>1)
                {
                    arrowposi.Y -= game.GraphicsDevice.Viewport.Height / 15;
                    arrowValue--;
                }
            }
            if (message.Equals("down"))
            {
                if (arrowValue < 2)
                {
                    arrowposi.Y += game.GraphicsDevice.Viewport.Height / 15;
                    arrowValue++;
                }
            }
            if (message.Equals("accept"))
            {
                interpretAccept();
            }

        }
        //Determines how to react to pressing A button in a given situation
        private void interpretAccept()
        {
            if (arrowValue == 1)
            {
            }
            if (arrowValue == 2)
            {
                StateChanged("MainMenu");
            }
            
        }      

        //Draws the menu. Nothing peculiar in here.
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuarrow, arrowposi, Color.White);
            spriteBatch.DrawString(font, "You were defeated.", new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 7), Color.Magenta,
                0.0f, font.MeasureString("You were defeated.") / 2, 2.5f, SpriteEffects.None, 0);
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
