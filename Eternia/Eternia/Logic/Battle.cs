using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternia
{
    public struct Info
    {
        public String text;
        public Vector2 position;
        public TimeSpan startTime;

        public Info(String text, Vector2 position, TimeSpan startTime)
        {
            this.text = text;
            this.position = position;
            this.startTime = startTime;
        }
    }
    class Battle
    {
        private List<IObserver> observers;
        Random randomizer;
        private IStrategy strategy;
        private List<Being> fighters;

        public List<Being> Fighters
        {
            get { return fighters; }
            set { }
        }
        private List<Being> heroes;

        public List<Being> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }

        private List<Being> enemies;
        public List<Being> Enemies
        {
            get { return this.enemies; }

        }
        bool waitingAction;
        public bool WaitingAction
        {
            get { return waitingAction; }
            set { waitingAction = value; }
        }
        private int turn;
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }
        
        float maxBar;
        public float MaxBar
        {
            get { return maxBar; }
            set { maxBar = value; }
        }
        

        /*
         * this is timebar for each hero and enemy. Their timeBar will grow in turns untill it reaches the maxbar level
         * - then it will be set to zero and it will start to ingrease again untill fight is over.
         */
        private Dictionary<String, float> timeBar;

        public Dictionary<String, float> TimeBar
        {
            get { return timeBar; }
        }

        public Battle()
        {
            turn = -1;
            observers = new List<IObserver>();
            timeBar = new Dictionary<string, float>();
            fighters = new List<Being>();
            heroes = new List<Being>();
            enemies = new List<Being>();
            maxBar = 200;
            waitingAction = false;
        }
        /*
         * Set given list of heros to battle. Heros are added to fighter and hero list. Each figter has own timebar
         * - timebar value is mapped by fighter's name and it's value is set to zero. As timebar grows and get's to max timebar value
         * it is corresponding fighter's turn to take action.
         */
        public void setHeroes(List<Hero> heroes)
        {
            foreach (Hero h in heroes)
            {
                this.fighters.Add(h);
                this.heroes.Add(h);
                timeBar.Add(h.Name, 0);
            }
        }

        public void setUpBattle() {
            setEnemies();
            

        }
        /*
         * Randomly select's amount of enemies between 1-4. Possibility is 0.3 to enemy to be "greater" boss fighter.
         * Enemys are created by enemy factory. Enemies are added to the fighter and to the enemy list. Enemies are also given 
         * their own timebar values to take action on their own turn.
         */
        private void setEnemies()
        {
            randomizer = new Random();

            int enemyCount = randomizer.Next(1,4);
            for (int i = 1; i <= enemyCount; i++)
            {
                double next = randomizer.NextDouble();

                if (next < 0.31)
                {
                    Enemy enemy = EnemyFactory.createBoss("Ravenous Bugblatter Beast Of Traal " + i, new Vector3((3 * i), 0, (-5 * i)), 100, 0, 30);
                    enemy.Speed = randomizer.Next(10, 20);
                    fighters.Add(enemy);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
                else
                {
                    Enemy enemy = EnemyFactory.createSoldier("Bugblatter " + i, new Vector3((3 * i),0, (-3 * i)), 100, 0, 50);
                    enemy.Speed = randomizer.Next(10, 20);
                    fighters.Add(enemy);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
            }
            
        }
        /*
         * Add's each fighters timebar value if battle is not on "waiting action" mode i.e none of fighter is currently performing their 
         * action. If fighter's timebar value goes to max value, turn changes to corresponding fighter on fighter list amd fighter's timebar
         * value is set to zero. 
         */
        public void addTimeBarValues()
        {
            if (WaitingAction)
                return;
            for (int i = 0; i < fighters.Count; i++)
            {
                float currentValue;
                Being being = fighters.ElementAt(i);
                if (timeBar.TryGetValue(being.Name, out currentValue))
                {
                    currentValue = currentValue + being.Speed / 50;

                    if (currentValue >= maxBar)
                    {
                        currentValue = 0;
                        WaitingAction = true;
                        TimeBar[being.Name] = currentValue;
                        Turn = i;
                        break;
                    }
                    TimeBar[being.Name] = currentValue;

                }


            }
        }
        public bool heroesTurn()
        {
            if (waitingAction)
            {
                if (turn < heroes.Count) return true;
            }
            return false;
        }

        private void changeTurn()
        {
            turn++;

            if (turn == fighters.Count())
            {
                turn = 0;
            }
        }
        public void Attacking(int target)
        {
            Being attacker = heroes.ElementAt(turn);
            Being enemyTarget = fighters.ElementAt(target);
            strategy = new Attack(attacker,enemyTarget);
            strategy.executeStrategy();
            waitingAction = false;
            changeTurn();
        }

        internal void executeEnemyAction()
        {
            if (!waitingAction) return;
            System.Threading.Thread.Sleep(3000);
            enemyAttack(turn);
            
        }

        private void enemyAttack(int attackerNro)
        {
            System.Threading.Thread.Sleep(1000);
            Being attacker = fighters.ElementAt(attackerNro);
            int targetNumber = randomizer.Next(0, heroes.Count - 1);
            Being target = fighters.ElementAt(targetNumber);
            strategy = new Attack(attacker, target);
            strategy.executeStrategy();
            changeTurn();
            waitingAction = false;
        }

        internal bool enemiesTurn()
        {
            if (waitingAction)
            {
                if(turn >= heroes.Count)
                    return true;
            }
                
            return false;
        }
        
        internal void fight()
        {
            addTimeBarValues();
            if (enemiesTurn())
            {
                executeEnemyAction();
            }
            
        }

        internal Being checkEnemyKilled()
        {
            // if fighters health is zero or less take him out from the list and returns dead enemy. 
            Being dead = null;
            foreach (Being being in enemies)
            {
                if (being.CurrentHealth <= 0)
                {
                    dead = being;
                    break;
                }

            }
            if (dead != null)
            {
                fighters.Remove(dead);
                enemies.Remove(dead);
            }
            return dead;
        }

        internal Being checkHeroKilled()
        {
            Being dead = null;
            foreach (Being being in heroes)
            {
                if (being.CurrentHealth <= 0)
                {
                    dead = being;
                    break;
                }
            }
            return dead;
        }
    }
}