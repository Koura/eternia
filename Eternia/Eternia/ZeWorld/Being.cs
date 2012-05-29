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
            set { currentHealth = value; }
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

        private float experience;

        public float Experience
        {
            get { return experience; }
            set { experience = value; }
        }
        
        // ice, water, lightning, wind, physical, magic and values
        public Dictionary<string, float> vulnerability;

        private String elementType;
        private String damageType;

        private float speed { get; set; }

        public Being()

        {
            this.position = new Vector3(0,0,0);
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
            
        }
    }
}
