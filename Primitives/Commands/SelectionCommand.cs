using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Primitives.Commands
{
    class SelectingCommand : TypedCommand<MainWindowVM>
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

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle)
                {
                    mainWindowVM.CurrentManipulator = new RectangleManipulator(mainWindowVM)
                    {
                        Color = Colors.Blue,
                    };
                }
                else if (result.First().Visual is WirePolygon)
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
                    }
                }
            }

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

                    mainWindowVM.CurrentManipulator.Bind(rect);
                    if (!mainWindowVM.viewport.Children.Contains(mainWindowVM.CurrentManipulator))
                    {
                        mainWindowVM.viewport.Children.Add(mainWindowVM.CurrentManipulator);
                    }
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    poly.IsSelected = true;
                    mainWindowVM.Props = poly.GetProps();

                    mainWindowVM.CurrentManipulator.Bind(poly);
                    if (!mainWindowVM.viewport.Children.Contains(mainWindowVM.CurrentManipulator))
                    {
                        mainWindowVM.viewport.Children.Add(mainWindowVM.CurrentManipulator);
                    }
                }
            }
            else
            {
                if (mainWindowVM.CurrentManipulator != null)
                {
                    mainWindowVM.CurrentManipulator.UnBind();
                    mainWindowVM.viewport.Children.Remove(mainWindowVM.CurrentManipulator);
                    mainWindowVM.CurrentManipulator = null;
                }
            }
        }
    }
}
