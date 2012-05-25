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
        private float maxHealth;
        private float currentHealth;
        private float armor;
        private float damage;
        private float experience;
        
        // ice, water, lightning, wind, physical, magic and values
        public Dictionary<string, float> vulnerability;

        private String elementType;
        private String damageType;

        private float speed { get; set; }

        public Being() {}

        public Being(Vector3 position,float maxHealth, float armor, float damage, float experience, Dictionary<string, float> vulnerability, float speed )

        {
            this.position = position;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.armor = armor;
            this.damage = damage;
            this.experience = experience;
            this.vulnerability = vulnerability;
            this.elementType = "physical";
            this.damageType = null;
            this.speed = speed;
            
        }

        public Being(float maxHealth, float armor, float damage, Dictionary<string, float> vulnerability)
        {
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.armor = armor;
            this.damage = damage;
            this.vulnerability = vulnerability;
            this.elementType = "physical";
            this.damageType = null;
        }
    }
}
