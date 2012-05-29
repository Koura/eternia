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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Hero : Being
    {
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private float maxMana;

        public float MaxMana
        {
            get { return maxMana; }
            set { maxMana = value; }
        }
        private float currentMana;

        public float CurrentMana
        {
            get { return currentMana; }
            set { currentMana = value; }
        }
        private float strength;

        public float Strength
        {
            get { return strength; }
            set { strength = value; }
        }
        private float contitution;

        public float Contitution
        {
            get { return contitution; }
            set { contitution = value; }
        }
        private float endurance;

        public float Endurance
        {
            get { return endurance; }
            set { endurance = value; }
        }
        private float intelligence;

        public float Intelligence
        {
            get { return intelligence; }
            set { intelligence = value; }
        }
        private List<Equipment> equipment;

        internal List<Equipment> Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }
        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public List<Observer> observers;

        public Hero(String name)
            : base()
        {
            this.name = name;
            this.maxMana = 50;
            this.currentMana = this.maxMana;
            this.strength = 10;
            this.contitution = 10;
            this.endurance = 10;
            this.intelligence = 10;
            this.equipment = new List<Equipment>();
            VulnerabilityCalc();
        }

        private void VulnerabilityCalc()
        {
            vulnerability.Add("physical", (1.0f - ((Armor / 10) / 100)));
            vulnerability.Add("magic", (1.0f - ((MagicResist / 10) / 100)));
            vulnerability.Add("fire", 1.0f - ((endurance/5)/100));
            vulnerability.Add("ice", 1.0f - ((endurance / 5) / 100));
            vulnerability.Add("lightning", 1.0f - ((endurance / 5) / 100));
            vulnerability.Add("wind", 1.0f - ((endurance / 5) / 100));
        }

        public void addObersever(Observer observer) {
            this.observers.Add(observer);
        }

        public void notifyObservers()
        {
            foreach (Observer o in observers)
            {
                o.update();
            }
        }

    }
}
