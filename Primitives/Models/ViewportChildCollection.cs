using HelixToolkit.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Primitives.Models
{
    public class ViewportChildCollection : ObservableCollection<BaseObject>
    {
        private readonly HelixViewport3D _viewport;

        public ObservableCollection<ConvertedData> ConvertedData
        {
            get
            {
                var val = this.GroupBy(t => t.GetType())
                    .Select(g => new ConvertedData
                    {
                        Type = GetName(g.Key.ToString()),
                        Items = new ObservableCollection<BaseObject>(g.ToList())
                    });
                return new ObservableCollection<ConvertedData>(val);
            }
        }

        private string GetName(string type)
        {
            switch (type)
            {
                case "Primitives.WireRectangle":
                    type = "Прямоугольники";
                    break;
                case "Primitives.WirePolygon":
                    type = "Полигоны";
                    break;
            }
            return type;
        }

        public ViewportChildCollection(HelixViewport3D viewport)
        {
            _viewport = viewport;
        }

        protected override void InsertItem(int index, BaseObject item)
        {
            _viewport.Children.Add(item);
            base.InsertItem(index, item);
            OnPropertyChanged(new PropertyChangedEventArgs("ConvertedData"));
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            OnPropertyChanged(new PropertyChangedEventArgs("ConvertedData"));
        }
    }
}
