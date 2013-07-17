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
        BasicModel worldModel;

        public BasicModel WorldModel
        {
            get { return worldModel; }
            set { worldModel = value; }
        }

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
            camera = new Camera(game.GraphicsDevice);
            state = "MainMenu";
            observers = new List<IObserver>();
       
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>

        //When new game is chosen from the main menu this method initializes some basic values to party, camera etc.
        public void NewGame()
        {
            
            maps = new Dictionary<string, Map>();
            Hero hero1 = new Hero("Taistelu Jaska", new Vector3(1400, -428, 1900));
            party = new Party();
            party.addCompany(hero1);
            party.attachObserver(new CameraObserver(this));
            setModels();
            worldModel = ModelManager.instance(game).models["Taistelu Jaska"];
            worldModel.setPosition(party.Position, party.PartyRotation);
            maps.Add("OverWorld", new Map("eternia", game));
            maps.Add("Battle1", new Map("battle1", game));
            camera = new Camera(game.GraphicsDevice);
            camera.SetUpCamera();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //Sets heroes in party/overworld in to modelmanager which gives the heroes models to represent them.
        private void setModels()
        {
            List<Being> temp = new List<Being>();
            foreach (Hero hero in party.Heroes)
            {
                temp.Add(hero);
            }
            ModelManager.instance(game).setHeros(temp);
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
            camera.notify();
        }

        //Gets the state string
        public String getState()
        {
            return this.state;
        }

        //sets the state according to the string gotten
        public void setState(string state)
        {
            if (state.Equals("newGame"))
            {
                NewGame();
                this.state = "OverWorld";
            }
            else
            {
                this.state = state;
            }
            
            notify();
        }
        
        //returns the map appropriate for the gamestate
        public Map getMap()
        {
            return maps[state];
        }
    }

}
