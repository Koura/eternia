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
    public class MainMenu : Screen, IObserver
    {

        Rectangle arrowposi;
        Texture2D menuarrow;
        Texture2D background;
        Texture2D title;
        SpriteFont font;
        private List<MenuOption> menuoptions = new List<MenuOption>();

        internal List<MenuOption> Menuoptions
        {
            get { return menuoptions; }
            set { menuoptions = value; }
        }

        

        public MainMenu(Game game)
            : base(game)
        {
            
        }

        public override void Initialize()

        {
            // Do our MainMenu component creation magicks here
            
            ArrowOnOption = 0;

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

        protected override void ProcessInput()
        {  
          
        }

        /*
         * Can we just leave this like so? Does the gamestate/screenmanager handle things so that only the topmost screen gets to update?
         */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

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
