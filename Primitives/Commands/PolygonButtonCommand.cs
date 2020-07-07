using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Commands
{
    class PolygonButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.clicks = 0;
            mainWindowVM.isRectangle = false;
            mainWindowVM.isPolygon = true;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
