using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class CommandHandler : IObserver
    {
        IScreenManager screenManager;
        LogicManager logic;

        public CommandHandler(IScreenManager screenManager, LogicManager logic)
        {
            this.screenManager = screenManager;
            this.logic = logic;
        }
        public void update() 
        {
            screenManager.currentScreen().stateChange += new StateChangeEventHandler(OnStateChange);
        }

        public void OnStateChange(object sender, String newState)
        {
            logic.StateChange(newState);
        }
    }
}
