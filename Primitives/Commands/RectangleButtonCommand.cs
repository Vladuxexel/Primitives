using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Commands
{
    class RectangleButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.clicks = 0;
            mainWindowVM.isRectangle = true;
            mainWindowVM.isPolygon = false;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
