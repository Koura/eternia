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
    public class GameState : ISubject, IGameState
    {
        private int arrowOnOption;

        
        private String state;
        private String status;
        private List<IObserver> observers;
        private Boolean safeZone;

        public GameState()
        {
            state = "MainMenu";
            observers = new List<IObserver>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>

        public void NewGame()
        {
            state = "MainMenu";
            status = "outdoors";
            safeZone = false;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            if(observers.Contains(observer))
                observers.Remove(observer);
        }
        public void notify()
        {
            foreach(IObserver observer in observers) {
                observer.update();
            }
        }
        public String getState()
        {
            return this.state;
        }
        public void setState(string state)
        {
            this.state = state;
            notify();
        }




        public int getArrowOnOptionState()
        {
            return arrowOnOption;
        }


        public void setArrowOnOptionState(int arrowOnOption)
        {
            this.arrowOnOption = arrowOnOption;
        }


        int IGameState.setArrowOnOptionState(int arrowOnOption)
        {
            return this.arrowOnOption;
        }
    }
}
