using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Primitives.Commands
{
    class SelectingCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            Point mousePos = Mouse.GetPosition(mainWindowVM.viewport);

            mainWindowVM.tempCoordinates.Clear();

            var result = mainWindowVM.viewport.Viewport.FindHits(mousePos);

            foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>())
            {
                elem.IsSelected = false;
            }

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle rect)
                {
                    rect.IsSelected = true;
                    mainWindowVM.Props = rect.GetProps();
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    poly.IsSelected = true;
                    mainWindowVM.Props = poly.GetProps();
                }
            }
            else
            {
                mainWindowVM.Props = null;
            }
        }
    }
}
