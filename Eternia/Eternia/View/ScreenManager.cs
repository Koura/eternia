
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
    public class ScreenManager : IScreenManager
    {

        Stack<Screen> screens = new Stack<Screen>();
        private Game game;
        private List<IObserver> observers;

        public ScreenManager(Game game)
        {
            this.game = game;
            observers = new List<IObserver>();
        }

        //returns the screen currently on top of the stack without removing it from the stack.
        public Screen currentScreen()
        {
            return screens.Peek();
        }

        //Adds a new screen on the top of the stack and marks the previous top screen to not update and draw.
        //Screen is also added in the game components so the game automatically calls the update and draw
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

        //removes the top screen of the stack and if after that there are more screens in the stack, 
        //the methods marks top most screen to update and draw.
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
