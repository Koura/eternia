using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    class Item : Iitem
    {
        private int value;
        private String name;
        private String attribute;

        public Item(String name, int value, String attribute)
        {
            this.value = value;
            this.name = name;
            this.attribute = attribute;
        }

        public int getValue()
        {
            return this.value;
        }

        public String getName()
        {
            return this.name;
        }

        public String getAttribute()
        {
            return this.attribute;
        }
    }
}
