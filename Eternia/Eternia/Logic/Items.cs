using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class Items : IStrategy
    {
        private Being target;
        private String itemName;

        public Items(Being target, String itemName)
        {
            this.target = target;
            this.itemName = itemName;
        }
        public void executeStrategy()
        {
            float newHealth = target.CurrentHealth + ItemManager.instance().getItem(itemName).getValue();
            target.CurrentHealth = newHealth;
        }
    }
}
