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
        private Boolean safeZone;
        private Party party;

        public Party Party
        {
            get { return this.party; }
        }

        private Dictionary<String, Map> maps;

        public GameState()
        {
            state = "MainMenu";
            observers = new List<IObserver>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>

        public void NewGame(Game game)
        {

            state = "MainMenu";
            Hero hero1 = new Hero("Taistelu Jaska");
            Hero hero2 = new Hero("Ozzy");
            Hero hero3 = new Hero("Wee Man");
            hero1.Damage = 50;
            hero2.Damage = 20;
            hero3.Damage = 20;
            party = new Party();
            party.addCompany(hero1);
            party.addCompany(hero2);
            party.addCompany(hero3);
            //maps.Add("OverWorld", new Map("eternia", game));
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
        public String getState()
        {
            return this.state;
        }
        public void setState(string state)
        {
            this.state = state;
            notify();
        }

        public Map getMap()
        {
            return maps[state];
        }
    }

}
