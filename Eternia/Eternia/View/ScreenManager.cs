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
    public class ScreenManager : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Stack<Screen> screens = new Stack<Screen>();
        private Game game;
        public ScreenManager(Game game)
            : base(game)
        {
            this.game = game;
            // TODO: Construct any child components here
        }

        public Screen currentScreen
        {
            get { return screens.Peek(); }
        }

        public void pushScreen(Screen screen)
        {
            if (screens.Count != 0)
            {
                currentScreen.Enabled = false;
                currentScreen.Visible = false;
            }
            screens.Push(screen);
            game.Components.Add(screen);
        }

        public void popScreen()
        {
            currentScreen.Enabled = false;
            currentScreen.Visible = false;
            game.Components.Remove(currentScreen);
            screens.Pop();
            if (screens.Count != 0)
            {
                currentScreen.Enabled = true;
                currentScreen.Visible = true;
            }
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //foreach (Screen screen in screens)
            //{
            //    if (screen.Enabled)
            //    {
            //        //screen.Update(gameTime);
            //    }
            //}
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //foreach (Screen screen in screens)
            //{
            //    if (screen.Visible)
            //    {
            //        //screen.Draw(gameTime);
            //    }
            //}
            base.Draw(gameTime);
        }

    }
}
