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
            items = new Dictionary<String,int>();
            position = new Vector3(50, 15.25f, -60);
            observers = new List<IObserver>();
        }

        //updates party position on the overworld map
        public void setPosi(Vector3 position)
        {
            this.position += position * 1.5f;
            notify();
        }
        //updates the party rotation on the overworld map
        public void setRota(Quaternion rotation)
        {
            this.partyRotation *= rotation;
            notify();
        }
        private List<Hero> heroes;

        public List<Hero> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }
        private Dictionary<IEquipment, int> equipment;

        //List of all the equipment the party has

        private Dictionary<String, int> items;

        public Dictionary<String, int> getItemList()
        {
            return items;
        }

        public void addItem(String name)
        {
            if (items.ContainsKey(name))
            {
                items["name"] = items["name"] + 1;
            }
            else
            {
                items.Add(name, 1);
            }
        }

        public void removeItem(String name)
        {
            if (items.ContainsKey(name))
            {
                if (items["name"] > 1)
                {
                    items["name"] = items["name"] - 1;
                }
                else
                {
                    items.Remove(name);
                }
            }
        }
        //adds a hero/companion/ally in to the party roster
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
