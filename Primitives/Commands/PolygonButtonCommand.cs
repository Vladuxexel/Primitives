namespace Primitives.Commands
{
    /// <summary>
    /// Switching on/off drawing polygon
    /// </summary>
    public class PolygonButtonCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.IsPolygon = mainWindowVM.IsPolygon ? mainWindowVM.IsPolygon = false : mainWindowVM.IsPolygon = true;
            mainWindowVM.IsRectangle = false;
            mainWindowVM.tempCoordinates.Clear();
        }
    }
}
