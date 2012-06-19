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
            battle.setHeroes(this.gameState.Party.Heroes);
            battle.setUpBattle();
            battleMenu.Fighters = battle.Fighters;
            List<Being> enemies = battle.Enemies;
            List<Being> heroes = battle.Heroes;
            battleMenu.modelManager.setEnemies(enemies);
            battleMenu.modelManager.setHeros(heroes);
            battleMenu.TimeBar = battle.TimeBar;
        }

        void IObserver.update()
        {
            battleMenu.PlayerTurn = battle.heroesTurn();

            //if it is player turn and he has made an action, get the action and execute it
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
                checkCasualties();
                resetPlayerAction();
            }

            battle.fight();
            battleMenu.HeroCount = battle.Heroes.Count;
        }

        private void resetPlayerAction()
        {
            BattleMenu.PlayerAction = "";
            BattleMenu.ActionMade = false;
        }

        private void checkCasualties()
        {
            enemyCasualties();
            heroCasualties();
        }

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
