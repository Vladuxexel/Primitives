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

            if (mainWindowVM.CurrentManipulator != null)
            {
                mainWindowVM.CurrentManipulator.UnBind();
                mainWindowVM.viewport.Children.Remove(mainWindowVM.CurrentManipulator);
                mainWindowVM.CurrentManipulator = null;
            }

            foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>().ToList())
            {
                elem.IsSelected = false;
            }

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle rect)
                {
                    rect.IsSelected = true;
                    rect.BindManipulator(mainWindowVM);
                    mainWindowVM.Props = rect.GetProps();
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    if (mainWindowVM.CurrentManipulator != null)
                    {
                        mainWindowVM.CurrentManipulator = null;
                    }
                    else
                    {
                        mainWindowVM.CurrentManipulator = new PolygonManipulator(mainWindowVM)
                        {
                            Color = Colors.Blue,
                        };

                        poly.IsSelected = true;

                        mainWindowVM.CurrentManipulator.Bind(poly);

                        if (!mainWindowVM.viewport.Children.Contains(mainWindowVM.CurrentManipulator))
                        {
                            mainWindowVM.viewport.Children.Add(mainWindowVM.CurrentManipulator);
                        }

                        mainWindowVM.Props = poly.GetProps();
                    }
                }
            }
            else
            {
                foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>().ToList())
                {
                    elem.IsSelected = false;
                    elem.DeleteManipulator(mainWindowVM);
                }
                mainWindowVM.Props = null;
            }
        }
    }
}
