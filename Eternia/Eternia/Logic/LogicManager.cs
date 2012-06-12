using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class LogicManager
    {
        GameState gameState;

        public LogicManager(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void StateChange(String newState)
        {
            gameState.setState(newState);
        }
    }
}
