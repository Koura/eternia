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
    public class Party : ISubject
    {
        private List<IObserver> observers;
        private Vector3 position;
        private Quaternion partyRotation = Quaternion.Identity;

        public Quaternion PartyRotation
        {
            get { return partyRotation; }
            set { partyRotation = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        
        public Party()
        {
            heroes = new List<Hero>();
            position = new Vector3(600, -120, 600);
            observers = new List<IObserver>();
        }

        public void setPosi(Vector3 position)
        {
            this.position = position;
            notify();
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
        
        public void addCompany(Hero hero)
        {
            heroes.Add(hero);
        }
        public void removeCompany(Hero hero)
        {
            if(heroes.Contains(hero))
               heroes.Remove(hero);
        }

        public void attachObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void detachObserver(IObserver observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
        public void notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.update();
            }
        }
    }
}
