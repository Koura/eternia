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
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
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

        public Hero(Vector3 position, String name, float maxMana, float strength, float contitution, float endurance, float intelligence, float maxHealth, float armor, float damage, float experience,Dictionary<String, float> vulnerability, String elementType, String damageType, float speed)
            : base(maxHealth, armor, damage, experience,vulnerability, speed)
        {
            this.position = position;
            this.name = name;
            this.maxMana = maxMana;
            this.currentMana = this.maxMana;
            this.strength = strength;
            this.contitution = contitution;
            this.endurance = endurance;
            this.intelligence = intelligence;
            this.equipment = new List<Equipment>();
        }

    }
}
