using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class CommandHandler : IObserver
    {
        IScreenManager screenManager;
        IGameState gameState;

        public CommandHandler(IScreenManager screenManager, IGameState gameState)
        {
            this.screenManager = screenManager;
            this.gameState = gameState;
        }
        public void update() 
        {
            screenManager.currentScreen().stateChange += new StateChangeEventHandler(OnStateChange);
        }

        public void OnStateChange(object sender, String newState)
        {
            gameState.setState(newState);
        }
    }
}
