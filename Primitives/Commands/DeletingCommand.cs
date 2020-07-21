using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Primitives.Commands
{
    public class DeletingCommand : TypedCommand<MainWindowVM>
    {
        protected override bool CanExecute(MainWindowVM mainWindowVM)
        {
            return mainWindowVM.MainTreeView.SelectedItem is BaseObject figure;
        }

        protected override void Execute(MainWindowVM mainWindowVM)
        {
            if (mainWindowVM.MainTreeView.SelectedItem is BaseObject figure)
            {
                figure.DeleteManipulator(mainWindowVM);
                mainWindowVM.Collection.Remove(figure);
                mainWindowVM.viewport.Children.Remove(figure);
                mainWindowVM.Props = null;
            }
        }
    }
}
