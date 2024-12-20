using DungeonCrawler.Items;
using SadConsole;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Treasures
{
    internal class SilverTreasure : Treasure
    {
        public SilverTreasure(Point position, IScreenSurface hostingSurface) : base(position, hostingSurface)
        {
            Appearance = new ColoredGlyph(Color.AnsiBlack, Color.Silver, '$');
            GoldRange = (10, 20);
            PossibleLoot.Add((new HealthPotionI(), 0.75));
        }
    }
}
