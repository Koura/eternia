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
        private String state;
        private List<IObserver> observers;
        private Party party;

        internal Party Party
        {
            get { return party; }
            set { party = value; }
        }

        private Dictionary<String, Map> maps;
        private Camera camera;
        private Game game;
        public Camera Camera
        {
            get { return camera; }
        }
        public Map getMap(String key) {
            return maps[key];
        }

        public GameState(Game game)
        {
            this.game = game;
            camera = new Camera();
            maps = new Dictionary<string, Map>();
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
            Hero hero1 = new Hero("Taistelu Jaska", new Vector3(0,0,-50));
            Hero hero2 = new Hero("Ozzy", new Vector3 (200, 0, 0));
            Hero hero3 = new Hero("Wee Man", new Vector3 (400, 0, -50));
            party = new Party();
            party.addCompany(hero1);
            party.addCompany(hero2);
            party.addCompany(hero3);
            maps.Add("OverWorld", new Map("eternia", game));
            camera = new Camera();
            camera.SetUpCamera(game.GraphicsDevice);
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
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
        public void notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.update();
            }
            camera.notify();
        }
        public String getState()
        {
            return this.state;
        }
        public void setState(string state)
        {
            if(state.Equals("OverWorld"))
            {
                NewGame();
            }
            this.state = state;
            notify();
        }
        
        public Map getMap()
        {
            return maps[state];
        }
    }

}
