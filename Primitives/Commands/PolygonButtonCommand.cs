using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Commands
{
    public class PolygonButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.isPolygon = mainWindowVM.isPolygon ? mainWindowVM.isPolygon = false : mainWindowVM.isPolygon = true;
            mainWindowVM.isRectangle = false;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
