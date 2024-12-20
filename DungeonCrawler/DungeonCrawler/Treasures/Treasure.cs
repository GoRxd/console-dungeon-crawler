using DungeonCrawler;
using DungeonCrawler.Items;
using SadConsole;
using SadConsoleGame;
using SadRogue.Primitives;
using System;
using System.Collections.Generic;

namespace DungeonCrawler.Treasures
{
    abstract internal class Treasure : GameObject
    {
        public Point GoldRange { get; protected set; }
        public List<(Item item, double chance)> PossibleLoot { get; protected set; } // Each item with its chance
        private readonly Random random = new Random();

        public Treasure(Point position, IScreenSurface hostingSurface) : base(position, hostingSurface)
        {
            PossibleLoot = new List<(Item item, double chance)>();
        }

        public override void OnCollision(GameObject source, Map map)
        {
            if (source is Player player)
            {
                // Award random gold to the player
                player.Gold += random.Next(GoldRange.X, GoldRange.Y);
                System.Console.WriteLine($"Player's new Gold amount: {player.Gold}");

                // Add random loot to the player's inventory
                Loot(player);

                // Destroy the treasure object
                Destroy(map.SurfaceObject);
            }
        }

        private void Loot(Player player)
        {
            if (PossibleLoot == null || PossibleLoot.Count == 0)
            {
                System.Console.WriteLine("No loot available in this treasure.");
                return;
            }

            foreach (var (item, chance) in PossibleLoot)
            {
                if (random.NextDouble() <= chance)
                {
                    player.Inventory.Add(item);
                    System.Console.WriteLine($"Player received: {item.Name}");
                }
            }
        }
    }
}
