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
    class Party
    {
        
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        
        private List<Hero> heroes;

        public List<Hero> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }
        private List<Equipment> inventory;

        public List<Equipment> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }
        public Party()
        {
            heroes = new List<Hero>();
        }
        public void addCompany(Hero hero)
        {
            heroes.Add(hero);
        }
        public void removeCompany(Hero hero)
        {
            if(heroes.Contains(hero))
               heroes.Remove(hero);
        }
    }
}
