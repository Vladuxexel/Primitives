using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Primitives
{
    class DrawCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.clicks++;
            if (mainWindowVM.isRectangle)
            {
                if (mainWindowVM.clicks == 1 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);
                }
                else if (mainWindowVM.clicks == 2 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);
                    var rect = new WireRectangle(mainWindowVM.tempCoordinates[0], mainWindowVM.tempCoordinates[1]);
                    mainWindowVM.viewport.Children.Add(rect);
                    mainWindowVM.tempCoordinates.Clear();
                    mainWindowVM.clicks = 0;
                    mainWindowVM.Model = mainWindowVM.modelGroup;
                    //rect.UpdateModel();
                }
            }
            else if (mainWindowVM.isPolygon)
            {
                MessageBox.Show("Polygon");
            }
        }
    }
}
