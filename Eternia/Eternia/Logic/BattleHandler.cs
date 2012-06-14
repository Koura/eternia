using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class BattleHandler: IObserver
    {
        private Battle battle;

        public Battle Battle
        {
            get { return this.battle; }
            set { this.battle = value; }
        }
        private BattleMenu battleMenu;

        public BattleMenu BattleMenu
        {
            get { return this.battleMenu; }
            set { this.battleMenu = value; }
        }
        private GameState gameState;

        public BattleHandler(IGameState gameState,Battle battle, BattleMenu battleMenu)
        {
            this.gameState = (GameState)gameState;
            this.battle = battle;
            this.battleMenu = battleMenu;
            this.battleMenu.attachObserver(this);
            initializeBattle();
            
        }
        public void initializeBattle()
        {
            battle.setUpHeroes(this.gameState.Party.Heroes);
            battle.setUpBattle();
            battleMenu.Fighters = battle.Fighters;
            battleMenu.TimeBar = battle.TimeBar;
        }

        void IObserver.update()
        {
            
            battleMenu.PlayerTurn = battle.herosTurn();

            //if it is player turn and he has made an action, get the action and execute it
            if (battle.herosTurn() && battleMenu.ActionMade)
            {
                switch (battleMenu.PlayerAction)
                {
                    case "Attack":
                        {
                            battle.Attacking(battleMenu.Target);
                            battleMenu.Casualties = battle.checkEnemyStatus();
                            battleMenu.Target = 0;
                            break;
                        }
                    case "Magic":
                        {
                            // magic stuff
                            break;
                        }
                    case "Skills":
                        {
                            // skills stuff
                            break;
                        }
                    case "Items":
                        {
                            //Items stufff
                            break;
                        }
                }
                resetPlayerAction();
            }

            battle.fight();
            battleMenu.Casualties = battle.checkHeroStatus();
            battleMenu.HeroCount = battle.Heroes.Count;
        }

        private void resetPlayerAction()
        {
            BattleMenu.PlayerAction = "";
            BattleMenu.ActionMade = false;
        }
    }
}
