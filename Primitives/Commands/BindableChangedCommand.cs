namespace Primitives.Commands
{
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
