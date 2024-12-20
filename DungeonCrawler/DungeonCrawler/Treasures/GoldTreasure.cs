using SadConsole;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Treasures
{
    internal class GoldTreasure : Treasure
    {
        public GoldTreasure(Point position, IScreenSurface hostingSurface) : base(position, hostingSurface)
        {
            Appearance = new ColoredGlyph(Color.AnsiBlack, Color.Gold, '$');
            GoldRange = (100, 500);
        }
    }
}
