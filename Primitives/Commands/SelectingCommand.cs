using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Primitives.Commands
{
    public class SelectingCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            Point mousePos = Mouse.GetPosition(mainWindowVM.viewport);

            mainWindowVM.tempCoordinates.Clear();

            var result = mainWindowVM.viewport.Viewport.FindHits(mousePos);

            Reset(mainWindowVM);

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle rect)
                {
                    Reset(mainWindowVM);
                    rect.IsSelected = true;
                    rect.BindManipulator(mainWindowVM);
                    mainWindowVM.Props = rect.GetProps();
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    Reset(mainWindowVM);
                    poly.IsSelected = true;
                    poly.BindManipulator(mainWindowVM);
                    mainWindowVM.Props = poly.GetProps();
                }
            }
            else
            {
                Reset(mainWindowVM);
                mainWindowVM.Props = null;
            }
        }

        private void Reset(MainWindowVM mainWindowVM)
        {
            foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>().ToList())
            {
                elem.IsSelected = false;
                elem.DeleteManipulator(mainWindowVM);
            }
        }
    }
}
