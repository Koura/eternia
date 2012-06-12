
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
    public class ScreenManager : IScreenManager
    {

        Stack<Screen> screens = new Stack<Screen>();
        private Game game;
        private List<IObserver> observers;

        public ScreenManager(Game game)
        {
            this.game = game;
            observers = new List<IObserver>();
            // TODO: Construct any child components here
        }

        public Screen currentScreen()
        {
            return screens.Peek();
        }

        public void pushScreen(Screen screen)
        {
            if (screens.Count != 0)
            {
                currentScreen().Enabled = false;
                currentScreen().Visible = false;
            }
            screens.Push(screen);
            game.Components.Add(screen);
            notify();
        }

        public void popScreen()
        {
            if (screens.Count != 0)
            {

                currentScreen().Enabled = false;
                currentScreen().Visible = false;
                game.Components.Remove(currentScreen());
                screens.Pop();
            }
            if (screens.Count != 0)
            {
                currentScreen().Enabled = true;
                currentScreen().Visible = true;
            }
        }

        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
        public void notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.update();
            }
        }
    }
}
