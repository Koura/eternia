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

namespace Eternia.Logic
{
    class Battle
    {
        List<Enemy> enemies;

        internal List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }
        List<Hero> heroes;

        public List<Hero> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }

        
        float maxBar;
        /*
         * this is timebar for each hero and enemy. Their timeBar will grow in turns untill it reaches the maxbar level
         * - then it will be set to zero and it will start to ingrease again untill fight is over.
         */
        Dictionary<String, float> timeBar;

        public Battle(List<Hero> heroes)
        {
            this.heroes = heroes;
            timeBar = new Dictionary<string, float>();
            foreach (Hero h in this.heroes)
            {
                timeBar.Add(h.Name, 0);
            }
        }

        public void setUpBattle() {
            setEnemies();

        }

        private void setEnemies()
        {
            Random randomizer = new Random();

            // ask from factory randomly boss and foot enemies from 1 - 4
            // if double is less than 0.31 call for a boss, otherwise normal foot soldier.

            // amount of enemies
            int enemyCount = randomizer.Next(1,4);
            
            Enemies = new List<Enemy>();

            for (int i = 1; i <= enemyCount; i++)
            {
                double next = randomizer.NextDouble();

                if (next < 0.31)
                {
                    Enemy enemy = EnemyFactory.createBoss("Ravenous Bugblatter Beast Of Traal " + i, new Vector3(10, 10, 10), 100, 0, 30);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
                else
                {
                    Enemy enemy = EnemyFactory.createSoldier("Bugblatter " + i, new Vector3(10, 100, 10), 100, 0, 50);
                    enemies.Add(enemy);
                    timeBar.Add(enemy.Name, 0);
                }
            }
            
        }
        
    }
}