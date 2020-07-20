using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Primitives.Commands
{
    public class TreeViewSelectedItemCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.tempCoordinates.Clear();

            if (mainWindowVM.MainTreeView.SelectedItem is WireRectangle rect)
            {
                Reset(mainWindowVM);
                rect.IsSelected = true;
                rect.BindManipulator(mainWindowVM);
                mainWindowVM.Props = rect.GetProps();
            }
            else if (mainWindowVM.MainTreeView.SelectedItem is WirePolygon poly)
            {
                Reset(mainWindowVM);
                poly.IsSelected = true;
                poly.BindManipulator(mainWindowVM);
                mainWindowVM.Props = poly.GetProps();
            }
        }
        private void Reset(MainWindowVM mainWindowVM)
        {
            foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>().ToList())
            {
                elem.DeleteManipulator(mainWindowVM);
            }
        }
    }
}
