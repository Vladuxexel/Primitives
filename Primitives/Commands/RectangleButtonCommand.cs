using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Commands
{
    public class RectangleButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.isRectangle = mainWindowVM.isRectangle ? mainWindowVM.isRectangle = false : mainWindowVM.isRectangle = true;
            mainWindowVM.isPolygon = false;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
