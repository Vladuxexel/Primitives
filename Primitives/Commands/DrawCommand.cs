using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Primitives
{
    class DrawCommand : TypedCommand<MainWindowVM>
    {
        protected override bool CanExecute(MainWindowVM mainWindowVM)
        {
            bool can = true;

            Point mousePos = Mouse.GetPosition(mainWindowVM.viewport);

            var result = mainWindowVM.viewport.Viewport.FindHits(mousePos);

            foreach (var elem in mainWindowVM.viewport.Children.OfType<BaseObject>())
            {
                if (elem.IsSelected)
                {
                    can = false;
                }
            }

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle rect)
                {
                    if (mainWindowVM.CurrentObject is WireRectangle curRect)
                    {
                        can = true;
                    }
                    else
                    {
                        can = false;
                    }
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    if (mainWindowVM.CurrentObject is WirePolygon curPoly)
                    {
                        can = true;
                    }
                    else
                    {
                        can = false;
                    }
                }
            }

            return can && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue;
        }

        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var point = mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value;
            if (mainWindowVM.CurrentObject == null)
            {
                if (mainWindowVM.isRectangle)
                {
                    mainWindowVM.CurrentObject = new WireRectangle(point);
                    mainWindowVM.CurrentObject.UpdateLastPoint(point);
                    mainWindowVM.Collection.Add(mainWindowVM.CurrentObject);
                }
                else if (mainWindowVM.isPolygon)
                {
                   mainWindowVM.CurrentObject = new WirePolygon(point);
                   mainWindowVM.CurrentObject.UpdateLastPoint(point);
                   mainWindowVM.Collection.Add(mainWindowVM.CurrentObject);
                }
            }
            else
            {
                mainWindowVM.CurrentObject.AddPoint(point);
                if (mainWindowVM.CurrentObject.IsEndCreate)
                {
                    mainWindowVM.CurrentObject = null;
                }
            }
        }
    }
}
