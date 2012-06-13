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

        public OverWorldObserver(GameState gameState, OverWorld overWorld)
        {
            this.gameState = gameState;
            this.overWorld = overWorld;
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
        }

        public void pushChanges()
        {
            overWorld.receiveChanges(worldMap,worldCamera);
        }
    }
}
