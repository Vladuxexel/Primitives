using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    class DrawRectangleCommand : TypedCommand<MainWindowVM>
    {
        int clicks = 0;
        bool isRectangle = true;
        readonly List<string> tempCoordinates = new List<string>();

        protected override void Execute(MainWindowVM mainWindowVM)
        {
            clicks++;
            if (isRectangle)
            {
                if (clicks == 1)
                {
                    tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.ToString());
                }
                else if (clicks == 2)
                {
                    tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.ToString());
                    Rectangle.drawRectangle(tempCoordinates[0], tempCoordinates[1], mainWindowVM);
                    tempCoordinates.Clear();
                    clicks = 0;
                    mainWindowVM.Model = mainWindowVM.modelGroup;
                }
            }
        }
    }
}
