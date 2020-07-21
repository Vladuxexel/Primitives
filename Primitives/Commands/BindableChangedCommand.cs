using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives.Commands
{
    public class BindableChangedCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVm)
        {
            mainWindowVm.IsBindable = mainWindowVm.IsBindable
                ? mainWindowVm.IsBindable = false
                : mainWindowVm.IsBindable = true;
        }
    }
}
