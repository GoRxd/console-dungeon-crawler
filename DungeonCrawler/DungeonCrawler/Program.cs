﻿using Console = SadConsole.Console;
using SadConsole;
using SadConsole.Configuration;
using SadRogue.Primitives;
using SadConsoleGame;

Settings.WindowTitle = "My SadConsole Game";

Builder configuration = new Builder()
    .SetScreenSize(120, 38)
    .SetStartingScreen<RootScreen>()
    .IsStartingScreenFocused(true)
    ;

Game.Create(configuration);
Game.Instance.Run();
Game.Instance.Dispose();