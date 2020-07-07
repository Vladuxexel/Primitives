using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Primitives
{
    class DrawCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.clicks++;
            if (mainWindowVM.isRectangle)
            {
                if (mainWindowVM.clicks == 1)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.ToString());
                }
                else if (mainWindowVM.clicks == 2)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.ToString());
                    new Rectangle(mainWindowVM.tempCoordinates[0], mainWindowVM.tempCoordinates[1], mainWindowVM);
                    mainWindowVM.tempCoordinates.Clear();
                    mainWindowVM.clicks = 0;
                    mainWindowVM.Model = mainWindowVM.modelGroup;
                }
            }
            else if (mainWindowVM.isPolygon)
            {
                MessageBox.Show("Polygon");
            }
        }
    }
}
