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
    public class ScreenManager
    {

        Stack<Screen> screens = new Stack<Screen>();
        private Game game;
        public ScreenManager(Game game)
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
    }
}
