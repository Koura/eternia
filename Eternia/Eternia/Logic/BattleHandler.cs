using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternia
{
    /*
     * battlehandler is object between battle and battleMenu. Battlehandler is battleMenu's observer.
     */
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
        /*
         * Initializes battle. Sets fighters i.e heroes and enemies in battle. Method will get fighters information from battle and will set all
         *  required information to battleMenu.
         */
        public void initializeBattle()
        {
            battle.setHeroes(this.gameState.Party.Heroes);
            battle.setUpBattle();
            battleMenu.Fighters = battle.Fighters;
            List<Being> enemies = battle.Enemies;
            List<Being> heroes = battle.Heroes;
            battleMenu.modelManager.setEnemies(enemies);
            battleMenu.modelManager.setHeros(heroes);
            battleMenu.TimeBar = battle.TimeBar;
        }

        /*
         * Method is notified by battleMenu. If it is player's turn to take action method will fetch player's choise from menu
         * and calls battle's corresponding method to execute action. 
         */
        void IObserver.update()
        {
            battleMenu.PlayerTurn = battle.heroesTurn();

            if (battle.heroesTurn() && battleMenu.ActionMade)
            {
                switch (battleMenu.PlayerAction)
                {
                    case "Attack":
                        {
                            battle.Attacking(battleMenu.Target);
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
            battleMenu.HeroCount = battle.Heroes.Count;
            checkCasualties();
        }
        /*
         * Method will reset player's previous actions in menu, so that same choise won't be executed again.
         */
        private void resetPlayerAction()
        {
            BattleMenu.PlayerAction = "";
            BattleMenu.ActionMade = false;
        }

        /*
         * Method will check if any party members or enemies are alive in battle, and if there are enemies and heroes alive
         * method will check if any heroes or enemies has been killed in battle.
         */
        private void checkCasualties()
        {
            bool heroesAlive = battle.checkPartyAlive();
            bool enemiesAlive = battle.checkEnemiesAlive();
            if (!heroesAlive)
            {
                // exit to mainMenu
            }
            else
            {
                heroCasualties();
            }


            if (!enemiesAlive)
            {
                // Continue quest in overWorld
                gameState.setState("OverWorld");
            }
            else
            {
                enemyCasualties();
            }
                
            
            
        }
        /*
         * method will check if any party members has been killed and creates a new information to be displayed to player if so,
         * and will delete the corresponding model from modelManager's model list.
         */
        private void heroCasualties()
        {
            Being deadHero = battle.checkHeroKilled();
            String infoText = "";
            if (deadHero != null)
            {
                infoText = "Party member has been killed.";
                battleMenu.modelManager.removeModel(deadHero);
            }
            battleMenu.casualtiesInfo = new Info(infoText, new Vector2(200, 250), battleMenu.CurrentTime);
        }
        /*
        * method will check if any enemies has been killed and creates a new information to be displayed to player if so,
         * and will delete the corresponding model from modelManager's model list.
        */
        private void enemyCasualties()
        {
            Being deadEnemy = battle.checkEnemyKilled();
            String infoText = "";
            if (deadEnemy != null)
            {
                infoText = "Enemy defeaded.";
                battleMenu.modelManager.removeModel(deadEnemy);
            }
            battleMenu.casualtiesInfo = new Info(infoText, new Vector2(200, 250), battleMenu.CurrentTime);
        }

    }
}
