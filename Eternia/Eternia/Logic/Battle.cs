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
        public void setUpHeroes(List<Hero> heroes)
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

        private void setEnemies()
        {
            randomizer = new Random();

            // ask from factory randomly boss and foot enemies from 1 - 4
            // if double is less than 0.31 call for a boss, otherwise normal foot soldier.

            // amount of enemies
            int enemyCount = randomizer.Next(1,4);
            for (int i = 1; i <= enemyCount; i++)
            {
                double next = randomizer.NextDouble();

                if (next < 0.31)
                {
                    Enemy enemy = EnemyFactory.createBoss("Ravenous Bugblatter Beast Of Traal " + i, new Vector3(10, 10, 10), 100, 0, 30);
                    enemy.Speed = randomizer.Next(10, 20);
                    fighters.Add(enemy);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
                else
                {
                    Enemy enemy = EnemyFactory.createSoldier("Bugblatter " + i, new Vector3(10, 100, 10), 100, 0, 50);
                    enemy.Speed = randomizer.Next(10, 20);
                    fighters.Add(enemy);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
            }
            
        }
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
        public bool herosTurn()
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

        
        public int checkEnemyStatus()
        {
            // if fighters health is zero or less take him out from the list. Message to modelManager not to draw those dead models
            // anymore. If all heros all all enemys are dead end battle.
            Being dead = null;
            foreach (Being being in enemies)
            {
                if (being.CurrentHealth <= 0)
                    dead = being;

            }
            fighters.Remove(dead);
            enemies.Remove(dead);
            if (enemies.Count == 0) return 4;
            if (dead != null) return 1;
            
            return 0;
        }

        internal int checkHeroStatus()
        {
            Being dead = null;
            foreach (Being being in heroes)
            {
                if (being.CurrentHealth <= 0)
                    dead = being;
            }
            fighters.Remove(dead);
            heroes.Remove(dead);
            if (heroes.Count == 0) return -4;
            if (dead != null) return -1;

            return 0;
        }

        internal void checkStatus()
        {
            // check all beings, who is alive
        }
    }
}