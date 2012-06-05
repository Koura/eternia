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
using Eternia.View;

namespace Eternia.Logic
{
    class Battle : ISubject
    {
        
        private List<IObserver> observers;

        private List<Being> fighters;

        public List<Being> Fighters
        {
            get;
            set;
        }
        
        float maxBar;
        /*
         * this is timebar for each hero and enemy. Their timeBar will grow in turns untill it reaches the maxbar level
         * - then it will be set to zero and it will start to ingrease again untill fight is over.
         */
        private Dictionary<String, float> timeBar;

        public Dictionary<String, float> TimeBar
        {
            get;
            set;
        }

        public Battle()
        {
            observers = new List<IObserver>();
            timeBar = new Dictionary<string, float>();
            fighters = new List<Being>();
            maxBar = 200;
        }
        public void setUpHeroes(List<Hero> heroes)
        {
            foreach (Hero h in heroes)
            {
                fighters.Add(h);
                timeBar.Add(h.Name, 50);
            }
        }

        public void setUpBattle() {
            //setEnemies();
            

        }

        private void setEnemies()
        {
            Random randomizer = new Random();

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
                    fighters.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
                else
                {
                    Enemy enemy = EnemyFactory.createSoldier("Bugblatter " + i, new Vector3(10, 100, 10), 100, 0, 50);
                    fighters.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
            }
            
        }


        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void notify()
        {
            foreach (IObserver item in observers)
            {
                item.update();
            }
        }

        internal void fight()
        {
            notify();
        }
    }
}