using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelixToolkit.Wpf;

namespace Primitives.Models
{
    public class ViewportChildCollection : ObservableCollection<BaseObject>
    {
        private readonly HelixViewport3D _viewport;

        public ViewportChildCollection(HelixViewport3D viewport)
        {
            _viewport = viewport;
        }

        protected override void InsertItem(int index, BaseObject item)
        {
            _viewport.Children.Add(item);
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            _viewport.Children.RemoveAt(index);
            base.RemoveItem(index);
        }
    }
}
