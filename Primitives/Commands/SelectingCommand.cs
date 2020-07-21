using HelixToolkit.Wpf;
using System.Linq;
using System.Windows.Input;

namespace Primitives.Commands
{
    /// <summary>
    /// Selection of object on viewport
    /// </summary>
    public class SelectingCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            var mousePos = Mouse.GetPosition(mainWindowVM.Viewport);

            mainWindowVM.tempCoordinates.Clear();

            var result = mainWindowVM.Viewport.Viewport.FindHits(mousePos);

            if (result.Any())
            {
                if (result.First().Visual is WireRectangle rect)
                {
                    rect.IsSelected = true;
                }
                else if (result.First().Visual is WirePolygon poly)
                {
                    poly.IsSelected = true;
                }
            }
            else
            {
                foreach (var elem in mainWindowVM.Viewport.Children.OfType<BaseObject>().ToList())
                {
                    elem.IsSelected = false;
                    elem.DeleteManipulator(mainWindowVM);
                }
                mainWindowVM.Props = null;
            }
        }
    }
}
