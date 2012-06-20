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
        //gets notified by the screenmanager. Commandhandler then subscribes its methods to the current screen statechange event.
        public void update() 
        {
            screenManager.currentScreen().stateChange += new StateChangeEventHandler(OnStateChange);
            screenManager.currentScreen().stateChange += new StateChangeEventHandler(OnStateUpdate);
        }

        //informs logic that a state has probably changed
        public void OnStateChange(object sender, String newState)
        {
            logic.StateChange(newState);
        }
        //informs logic that updates may be occured
        public void OnStateUpdate(object sender, String newUpdate)
        {
            logic.stateUpdate(newUpdate);
        }
    }
}
