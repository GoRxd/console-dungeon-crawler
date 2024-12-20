using DungeonCrawler.Items;
using SadConsole;
using SadConsoleGame;
using SadRogue.Primitives;

namespace DungeonCrawler
{
    internal class Player : GameObject
    {
        public int Gold { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(Point position, IScreenSurface hostingSurface) : base(position, hostingSurface)
        {
            Inventory = [];
            Appearance = new ColoredGlyph(Color.AnsiBlack, Color.Blue, 'P');
        }
    }
}