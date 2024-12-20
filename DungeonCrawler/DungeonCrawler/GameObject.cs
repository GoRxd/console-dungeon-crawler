using SadConsole;
using SadRogue.Primitives;
using System.Security.Cryptography;

namespace SadConsoleGame
{
    internal class GameObject
    {
        public bool ToDestroy { get; private set; }
        protected ColoredGlyph _mapAppearance = new ColoredGlyph();
        public Point Position { get; private set; }

        public ColoredGlyph? Appearance { get; set; }

        public GameObject(Point position, IScreenSurface hostingSurface)
        {
            Position = position;
            // Store the map cell
            hostingSurface.Surface[position].CopyAppearanceTo(_mapAppearance);
        }

        public void DrawGameObject(IScreenSurface screenSurface)
        {
            Appearance!.CopyAppearanceTo(screenSurface.Surface[Position]);
            screenSurface.IsDirty = true;
        }
        public bool Move(Point newPosition, IScreenSurface screenSurface)
        {
            // Check new position is valid
            if (!screenSurface.Surface.IsValidCell(newPosition.X, newPosition.Y)) return false;
            if (screenSurface.Surface[newPosition].Glyph == 0)
            {
                // Restore the old cell
                _mapAppearance.CopyAppearanceTo(screenSurface.Surface[Position]);

                // Store the map cell of the new position
                screenSurface.Surface[newPosition].CopyAppearanceTo(_mapAppearance);

                Position = newPosition;
                DrawGameObject(screenSurface);

                return true;
            }
            else
                return false;
        }

        public bool Collides(GameObject target)
        {
            int deltaX = Math.Abs(Position.X - target.Position.X);
            int deltaY = Math.Abs(Position.Y - target.Position.Y);

            return (deltaX == 1 && deltaY == 0) || (deltaX == 0 && deltaY == 1);
        }

        public virtual void OnCollision(GameObject source, Map map)
        {

        }

        public void Destroy(IScreenSurface screenSurface)
        {
            _mapAppearance.CopyAppearanceTo(screenSurface.Surface[Position]);
            ToDestroy = true;
        }
    }
}