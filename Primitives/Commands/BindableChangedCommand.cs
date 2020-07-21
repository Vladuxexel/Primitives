namespace Primitives.Commands
{
    /// <summary>
    /// Switching on/off binding to nearest point in radius
    /// </summary>
    public class BindableChangedCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVm)
        {
            mainWindowVm.IsBindable = mainWindowVm.IsBindable
                ? mainWindowVm.IsBindable = false
                : mainWindowVm.IsBindable = true;
        }
    }
}
