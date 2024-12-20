using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Items
{
    abstract internal class UsableItem : Item
    {
        public abstract void Use(Player player);
    }
}
