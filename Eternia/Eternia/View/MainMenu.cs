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
using Eternia.View;


namespace Eternia
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MainMenu : Screen
    {

        Rectangle arrowposi;
        Texture2D menuarrow;
        Texture2D background;
        Texture2D title;
        SpriteFont font;

        internal List<MenuOption> Menuoptions
        {
            get { return menuoptions; }
            set { menuoptions = value; }
        }

        int arrowValue = 1;
      
        private List<MenuOption> menuoptions = new List<MenuOption>();      

        public MainMenu(Game game)
            : base(game)
        {
            // send message "menu"
            // dj.playdatfunkysong("menu"); somewhere else?
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
            background = game.Content.Load<Texture2D>("images/background");
            font = game.Content.Load<SpriteFont>("fonts/menufont");

            title = game.Content.Load<Texture2D>("images/menutxt");
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2-40), "New Game", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2), "Load Game", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 40), "Options", font, Color.White));
            menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2 + 80), "Exit Game", font, Color.White));
            arrowposi = new Rectangle(game.GraphicsDevice.Viewport.Width / 3 + 10, game.GraphicsDevice.Viewport.Height / 2 - 61, menuarrow.Width / 16, menuarrow.Height / 16);

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
                    arrowposi.Y -= 40;
                    arrowValue--;
                }
            }
            if (message.Equals("down"))
            {
                if (arrowValue < 4)
                {
                    arrowposi.Y += 40;
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
            //starting new game
            if (arrowValue == 1)
            {
                StateChanged("newGame");
            }
            //loading a previous game
            if (arrowValue == 2)
            {
                //insert loading here
            }
            //pressed A at options
            if (arrowValue == 3)
            {
                StateChanged("Options");
            }
            //A was pressed at Exit game
            if (arrowValue == 4)
            {
                game.Exit();
            }
        }      

        //Draws the menu. Nothing peculiar in here.
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();


            foreach (MenuOption option in Menuoptions)

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.SteelBlue);
            spriteBatch.Draw(title, new Rectangle(10,0, title.Width, title.Height), Color.White);
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
