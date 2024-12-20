using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Items
{
    abstract internal class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Item()
        {

        }
    }
}
