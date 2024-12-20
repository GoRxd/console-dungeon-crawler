using SadConsole;
using SadRogue.Primitives;
using SadConsole.Input;
using System;
using System.Runtime.InteropServices;

namespace SadConsoleGame
{
    internal class RootScreen : ScreenObject
    {
        private Map _map;

        private DateTime _lastMoveTimeHorizontal; // Czas ostatniego ruchu poziomego
        private DateTime _lastMoveTimeVertical;   // Czas ostatniego ruchu pionowego

        private readonly TimeSpan _horizontalMoveCooldown = TimeSpan.FromMilliseconds(50); // Cooldown dla ruchu poziomego
        private readonly TimeSpan _verticalMoveCooldown = TimeSpan.FromMilliseconds(150);   // Cooldown dla ruchu pionowego

        public RootScreen()
        {
            _map = new Map(Game.Instance.ScreenCellsX, Game.Instance.ScreenCellsY - 5);
            Children.Add(_map.SurfaceObject);
            _lastMoveTimeHorizontal = DateTime.Now;
            _lastMoveTimeVertical = DateTime.Now;
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);
            _map.DrawGameObjects();
            _map.HandleCollisions();
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        public override bool ProcessKeyboard(Keyboard keyboard)
        {
            bool handled = false;
            DateTime currentTime = DateTime.Now;
            if (GetAsyncKeyState((int)Keys.Up) != 0 &&
                currentTime - _lastMoveTimeVertical >= _verticalMoveCooldown)
            {
                _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Up, _map.SurfaceObject);
                _lastMoveTimeVertical = currentTime;
                handled = true;
            }

            if (GetAsyncKeyState((int)Keys.Down) != 0 &&
                currentTime - _lastMoveTimeVertical >= _verticalMoveCooldown)
            {
                _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Down, _map.SurfaceObject);
                _lastMoveTimeVertical = currentTime;
                handled = true;
            }

            if (GetAsyncKeyState((int)Keys.Left) != 0 &&
                currentTime - _lastMoveTimeHorizontal >= _horizontalMoveCooldown)
            {
                _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Left, _map.SurfaceObject);
                _lastMoveTimeHorizontal = currentTime;
                handled = true;
            }

            if (GetAsyncKeyState((int)Keys.Right) != 0 &&
                currentTime - _lastMoveTimeHorizontal >= _horizontalMoveCooldown)
            {
                _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Right, _map.SurfaceObject);
                _lastMoveTimeHorizontal = currentTime;
                handled = true;
            }

            return handled;
        }
    }
}
