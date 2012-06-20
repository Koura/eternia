using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class OverWorldObserver : IObserver
    {

        GameState gameState;
        OverWorld overWorld;
        Map worldMap;
        Camera worldCamera;
        List<Hero> heroes;
        BasicModel model;

        public OverWorldObserver(GameState gameState, OverWorld overWorld)
        {
            this.gameState = gameState;
            this.overWorld = overWorld;
            this.heroes = new List<Hero>();
        }

        //gets notified when the camera is updated and then it pulls all the relevant information and after that tells the overworld screen about the changes.
        public void update()
        {
            pullChanges();
            pushChanges();
        }

        //Pulls the map, camera, heroes and model from the gamestate.
        public void pullChanges()
        {
            this.worldMap = gameState.getMap("OverWorld");
            this.worldCamera = gameState.Camera;
            this.heroes = gameState.Party.Heroes;
            this.model = gameState.WorldModel;
        }
        //pushes pulled data to overworld.
        public void pushChanges()
        {
            overWorld.receiveChanges(worldMap,worldCamera, heroes, model);
        }
    }
}
