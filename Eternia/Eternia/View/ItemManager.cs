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
    public class ItemManager
    {
        private Dictionary<String, Iitem> items;
        private static ItemManager inst;

        public ItemManager()
        {
            items = new Dictionary<String, Iitem>();
            LoadContent();
        }

        public static ItemManager instance()
        {
            if (inst == null)
            {
                inst = new ItemManager();
            }
            return inst;
        }
        private void LoadContent()
        {
            Item itemAdded = new Item("Potion", 50, "hp");
            items.Add(itemAdded.getName(), itemAdded);
            itemAdded = new Item("Elixir", 50, "mp");
            items.Add(itemAdded.getName(), itemAdded);
            itemAdded = new Item("Grenade", -50, "hp");
            items.Add(itemAdded.getName(), itemAdded);
        }

        internal Iitem getItem(String name)
        {
            return items[name];
        }
    }
}
