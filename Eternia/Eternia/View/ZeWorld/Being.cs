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
    public  abstract class Being
    {
        
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private float maxHealth;

        public float MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        private float currentHealth;

        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value < maxHealth)
                {
                    currentHealth = value;
                }
                else
                {
                    currentHealth = maxHealth;
                }
            }
        }
        private float armor;

        public float Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        private float magicResist;

        public float MagicResist
        {
            get { return magicResist; }
            set { magicResist = value; }
        }

        private float damage;

        public float Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        private int experience;

        public int Experience
        {
            get { return experience; }
            set { experience += value; }
        }
        
        // ice, water, lightning, wind, physical, magic and values
        public Dictionary<string, float> vulnerability;

        private String elementType;

        public String ElementType
        {
            get { return elementType; }
            set { elementType = value; }
        }
        private String damageType;

        public String DamageType
        {
            get { return damageType; }
            set { damageType = value; }
        }
        private float speed;

        public float Speed 
        {
            get { return speed; }
            set { speed = value; }
        }

        public Being(String name, Vector3 position)

        {
            this.name = name;
            this.position = position;
            this.maxHealth = 100;
            this.currentHealth = maxHealth;
            this.armor = 0;
            this.damage = 20;
            this.experience = 0;
            this.vulnerability = new Dictionary<string,float>();
            this.elementType = null;
            this.damageType = "physical";
            this.speed = 30;
            this.magicResist = 0;
            VulnerabilityCalc();
        }

        public Being(String name, Vector3 position, float maxHealth, float armor, float damage, int xp)
        {
            this.name = name;
            this.position = position;
            this.maxHealth = maxHealth;
            this.currentHealth = this.maxHealth;
            this.armor = armor;
            this.damage = damage;
            this.experience = 0;
            this.vulnerability = new Dictionary<string, float>();
            this.elementType = null;
            this.damageType = "physical";
            this.speed = 30;
            this.magicResist = 0;
            this.experience = xp;
            VulnerabilityCalc();
        }
        public void VulnerabilityCalc()
        {
            vulnerability.Add("physical", (1.0f - ((Armor / 10) / 100)));
            vulnerability.Add("magic", (1.0f - ((MagicResist / 10) / 100)));
        }
    }
}
