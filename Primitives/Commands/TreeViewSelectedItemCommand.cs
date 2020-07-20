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

            if (mainWindowVM.CurrentManipulator != null)
            {
                mainWindowVM.CurrentManipulator.UnBind();
                mainWindowVM.viewport.Children.Remove(mainWindowVM.CurrentManipulator);
                mainWindowVM.CurrentManipulator = null;
            }

            if (mainWindowVM.MainTreeView.SelectedItem is WireRectangle)
            {
                mainWindowVM.CurrentManipulator = new RectangleManipulator(mainWindowVM)
                {
                    Color = Colors.Blue,
                };
            }
            else if (mainWindowVM.MainTreeView.SelectedItem is WirePolygon)
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

            if (mainWindowVM.MainTreeView.SelectedItem is WireRectangle rect)
            {
                rect.IsSelected = true;
                mainWindowVM.Props = rect.GetProps();

               /// mainWindowVM.CurrentManipulator.Bind(rect);

               // /if (!mainWindowVM.viewport.Children.Contains(mainWindowVM.CurrentManipulator))
               // /{
               //     mainWindowVM.viewport.Children.Add(mainWindowVM.CurrentManipulator);
               // /}
            }
            else if (mainWindowVM.MainTreeView.SelectedItem is WirePolygon poly)
            {
                poly.IsSelected = true;
                mainWindowVM.Props = poly.GetProps();

                mainWindowVM.CurrentManipulator.Bind(poly);

                if (!mainWindowVM.viewport.Children.Contains(mainWindowVM.CurrentManipulator))
                {
                    mainWindowVM.viewport.Children.Add(mainWindowVM.CurrentManipulator);
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
                mainWindowVM.Props = null;
            }
        }
    }
}
