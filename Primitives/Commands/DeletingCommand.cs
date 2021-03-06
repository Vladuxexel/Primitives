﻿namespace Primitives.Commands
{
    /// <summary>
    /// Deleting figures using TreeView
    /// </summary>
    public class DeletingCommand : TypedCommand<MainWindowVM>
    {
        protected override bool CanExecute(MainWindowVM mainWindowVM)
        {
            return mainWindowVM.MainTreeView.SelectedItem is BaseObject;
        }

        protected override void Execute(MainWindowVM mainWindowVM)
        {
            if (mainWindowVM.MainTreeView.SelectedItem is BaseObject figure)
            {
                figure.DeleteManipulator(mainWindowVM);
                mainWindowVM.Collection.Remove(figure);
                mainWindowVM.Viewport.Children.Remove(figure);
                mainWindowVM.Props = null;
            }
        }
    }
}
