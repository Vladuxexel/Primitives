﻿namespace Primitives.Commands
{
    /// <summary>
    /// Switching on/off drawing rectangle
    /// </summary>
    public class RectangleButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.IsRectangle = mainWindowVM.IsRectangle ? mainWindowVM.IsRectangle = false : mainWindowVM.IsRectangle = true;
            mainWindowVM.IsPolygon = false;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
