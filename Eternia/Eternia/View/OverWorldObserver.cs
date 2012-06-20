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
        public void update()
        {
            pullChanges();
            pushChanges();
        }

        public void pullChanges()
        {
            this.worldMap = gameState.getMap("OverWorld");
            this.worldCamera = gameState.Camera;
            this.heroes = gameState.Party.Heroes;
            this.model = gameState.WorldModel;
        }

        public void pushChanges()
        {
            overWorld.receiveChanges(worldMap,worldCamera, heroes, model);
        }
    }
}
