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
    public class GameState : Microsoft.Xna.Framework.GameComponent, ISubject, IGameState
    {

        private Party party;
        private String state;
        private String status;
        private List<IObserver> observers;

        private Boolean safeZone;

        public GameState(Game game)
            : base(game)
        {
            observers = new List<IObserver>();
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

        public void NewGame()
        {
            party = new Party();
<<<<<<< HEAD
            state = "MainMenu";
=======
            state = "World";
>>>>>>> 28a2987a645934d17ca688e419a2d3b05e97b5de
            status = "outdoors";
            safeZone = false;
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
<<<<<<< HEAD
            
=======
            notify();
>>>>>>> 28a2987a645934d17ca688e419a2d3b05e97b5de
            base.Update(gameTime);
        }
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
    }
}
