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
        IGameState gameState;
        Texture2D menuarrow;
        int arrowXpos;
        int arrowYpos;
        
        int arrowOffset;
        float[] optionsYpos;
        SpriteFont font;
        private List<MenuOption> menuoptions = new List<MenuOption>();

        internal List<MenuOption> Menuoptions
        {
            get { return menuoptions; }
            set { menuoptions = value; }
        }

        

        public MainMenu(Game game, IGameState gameState)
            : base(game)
        {
            this.gameState = gameState;
        }

        public override void Initialize()

        {
            // Do our MainMenu component creation magicks here
            
            arrowXpos = game.GraphicsDevice.Viewport.Width/3+1;
            ArrowOnOption = 0;
            optionsYpos = new float[4];
            arrowYpos = (int)optionsYpos[ArrowOnOption];
            optionsYpos[0] = game.GraphicsDevice.Viewport.Height / 2 - 40;
            optionsYpos[1] = game.GraphicsDevice.Viewport.Height / 2;
            optionsYpos[2] = game.GraphicsDevice.Viewport.Height / 2 + 40;
            optionsYpos[3] = game.GraphicsDevice.Viewport.Height / 2 + 80;
            arrowOffset = -20;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            menuarrow = game.Content.Load<Texture2D>("images/menuarrow");
            font = game.Content.Load<SpriteFont>("fonts/menufont");
            Menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, optionsYpos[0]), "New Game", font, Color.White));
            Menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width/2, optionsYpos[1]), "Load Game", font, Color.White));
            Menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, optionsYpos[2]), "Options", font, Color.White));
            Menuoptions.Add(new MenuOption(new Vector2(game.GraphicsDevice.Viewport.Width / 2, optionsYpos[3]), "Exit Game", font, Color.White));
        }

        protected override void UnloadContent()
        {

        }

       

        /*
         * Can we just leave this like so? Does the gamestate/screenmanager handle things so that only the topmost screen gets to update?
         */
        public override void Update(GameTime gameTime)
        {
            ArrowOnOption = gameState.getArrowOnOptionState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(menuarrow, new Rectangle(arrowXpos,(int)optionsYpos[ArrowOnOption] + arrowOffset,menuarrow.Width/16, menuarrow.Height/16), Color.White);

            foreach (MenuOption option in Menuoptions)
            {
                spriteBatch.DrawString(option.Font, option.Text, option.Position, option.Colour,
                option.Rotation, option.Size / 2, option.Scale, SpriteEffects.None, 0);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void update()
        {
            throw new NotImplementedException();
        }
    }
}
