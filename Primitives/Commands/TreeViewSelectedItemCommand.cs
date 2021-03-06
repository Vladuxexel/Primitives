﻿using System.Linq;

namespace Primitives.Commands
{
    /// <summary>
    /// Selection an object on TreeView
    /// </summary>
    public class TreeViewSelectedItemCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            mainWindowVM.tempCoordinates.Clear();

            if (mainWindowVM.MainTreeView.SelectedItem is WireRectangle rect)
            {
                ResetTreeView(mainWindowVM);
                rect.BindManipulator(mainWindowVM);
                mainWindowVM.Props = rect.GetProps();
            }
            else if (mainWindowVM.MainTreeView.SelectedItem is WirePolygon poly)
            {
                ResetTreeView(mainWindowVM);
                poly.BindManipulator(mainWindowVM);
                mainWindowVM.Props = poly.GetProps();
            }
        }
        /// <summary>
        /// Resets manipulator
        /// </summary>
        /// <param name="mainWindowVM"></param>
        private void ResetTreeView(MainWindowVM mainWindowVM)
        {
            foreach (var elem in mainWindowVM.Viewport.Children.OfType<BaseObject>().ToList())
            {
                elem.DeleteManipulator(mainWindowVM);
            }
        }
    }
}
