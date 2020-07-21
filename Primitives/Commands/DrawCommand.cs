using HelixToolkit.Wpf;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Primitives.Commands
{
    /// <summary>
    /// Drawing a figure
    /// </summary>
    public class DrawCommand : TypedCommand<MainWindowVM>
    {
        /// <summary>
        /// Executes if there aren't selected figures and mouse isn't over existing object
        /// </summary>
        /// <param name="mainWindowVM"></param>
        /// <returns></returns>
        protected override bool CanExecute(MainWindowVM mainWindowVM)
        {
            var can = true;

            var mousePos = Mouse.GetPosition(mainWindowVM.Viewport);

            var result = mainWindowVM.Viewport.Viewport.FindHits(mousePos);

            foreach (var elem in mainWindowVM.Viewport.Children.OfType<BaseObject>())
            {
                if (elem.IsSelected)
                {
                    can = false;
                }
            }

            if (result.Any())
            {
                switch (result.First().Visual)
                {
                    case WireRectangle _:
                        can = mainWindowVM.CurrentObject is WireRectangle;
                        break;
                    case WirePolygon _:
                        can = mainWindowVM.CurrentObject is WirePolygon;
                        break;
                }
            }

            return can && mainWindowVM.Viewport.CursorOnConstructionPlanePosition.HasValue;
        }

        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var point = mainWindowVM.Viewport.CursorOnConstructionPlanePosition.Value;

            if (mainWindowVM.NearestPoint != null)
            {
                point = (Point3D)mainWindowVM.NearestPoint;
                mainWindowVM.NearestPoint = null;
            }

            if (mainWindowVM.CurrentObject == null)
            {
                if (mainWindowVM.IsRectangle)
                {
                    mainWindowVM.CurrentObject = new WireRectangle(point);
                    mainWindowVM.CurrentObject.UpdateLastPoint(point);
                    mainWindowVM.Collection.Add(mainWindowVM.CurrentObject);
                }
                else if (mainWindowVM.IsPolygon)
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
