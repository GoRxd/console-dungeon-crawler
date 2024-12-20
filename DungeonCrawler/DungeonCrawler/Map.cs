using DungeonCrawler;
using DungeonCrawler.Treasures;
using SadConsole;
using SadRogue.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace SadConsoleGame
{
    internal class Map
    {
        private List<GameObject> _mapObjects;
        private ScreenSurface _mapSurface;

        public IReadOnlyList<GameObject> GameObjects => _mapObjects.AsReadOnly();
        public ScreenSurface SurfaceObject => _mapSurface;
        public GameObject UserControlledObject { get; set; }

        public Map(int mapWidth, int mapHeight)
        {
            _mapObjects = new List<GameObject>();
            _mapSurface = new ScreenSurface(mapWidth, mapHeight);
            _mapSurface.UseMouse = false;

            FillBackground();

            UserControlledObject = new Player(_mapSurface.Surface.Area.Center, _mapSurface);
            _mapObjects.Add(UserControlledObject);
            for (int i = 0; i < 5; i++)
            {
                CreateTreasureAtRandomLocation<GoldTreasure>();
                CreateTreasureAtRandomLocation<SilverTreasure>();
            }
        }

        private void CreateTreasureAtRandomLocation<T>() where T : Treasure
        {
            // Try 1000 times to get an empty map position
            for (int i = 0; i < 1000; i++)
            {
                // Get a random position
                Point randomPosition = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
                                                 Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

                // Check if any object is already positioned there, repeat the loop if found
                bool foundObject = _mapObjects.Any(obj => obj.Position == randomPosition);
                if (foundObject) continue;

                // If the code reaches here, we've got a good position, create the game object.
                Treasure treasure = (T)Activator.CreateInstance(typeof(T), randomPosition, _mapSurface);
                if (treasure != null)
                {
                    _mapObjects.Add(treasure);
                }
                break;
            }
        }

        private void FillBackground()
        {
            Color[] colors = new[] { Color.LightGreen, Color.Coral, Color.CornflowerBlue, Color.DarkGreen };
            float[] colorStops = new[] { 0f, 0.35f, 0.75f, 1f };

            Algorithms.GradientFill(_mapSurface.FontSize,
                                    _mapSurface.Surface.Area.Center,
                                    _mapSurface.Surface.Width / 3,
                                    45,
                                    _mapSurface.Surface.Area,
                                    new Gradient(colors, colorStops),
                                    (x, y, color) => _mapSurface.Surface[x, y].Background = color);
        }

        public void DrawGameObjects()
        {
            foreach (GameObject obj in _mapObjects.ToList())
            {
                if (obj != null)
                {
                    if (obj.ToDestroy)
                        _mapObjects.Remove(obj);
                    else
                        obj.DrawGameObject(_mapSurface);
                }
            }
        }

        public void HandleCollisions()
        {
            foreach (GameObject obj in _mapObjects.ToList())
            {
                if (obj == null)
                    continue;
                foreach (GameObject sec in _mapObjects.ToList())
                {
                    if (sec != null)
                    {
                        if (obj != sec)
                        {
                            if (obj.Collides(sec))
                            {
                                obj.OnCollision(sec, this);
                            }
                        }
                    }
                }
            }
        }
    }
}