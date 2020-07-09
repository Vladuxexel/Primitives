using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SharpDX;
using Color = System.Windows.Media.Color;

namespace Primitives
{
    public class WirePolygon : BaseObject, INotifyPropertyChanged
    {
        private List<Point3D> _points;
        private bool _isSelected;
        private Color _brush = Colors.Green;

        public WirePolygon(List<Point3D> points)
        {
            _points = points.ToList();

            for (int i = 0; i <= points.Count-2; i++)
            {
                Points.Add(points[i]);
                Points.Add(points[i+1]);
            }

            Points.Add(points[points.Count-1]);
            Points.Add(points[0]);

            Thickness = 5;
            Color = _brush;
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetSelectedColor();
                OnPropertyChanged("IsSelected");
            }
        }

        public ObservableCollection<ViewPropsVM> GetProps()
        {
            return new ObservableCollection<ViewPropsVM>()
            {
                new ViewPropsVM("Периметр",(v=>{ }), () => Perimeter)
            };
        }

        private double Perimeter
        {
            get { return Calculator.GetPerimeter(_points); }
        }

        public void DrawLines(List<Point3D> points)
        {
            
        }

        private void SetSelectedColor()
        {
            if (_isSelected)
            {
                _brush = Colors.Red;
            }
            else
            {
                _brush = Colors.Green;
            }

            Color = _brush;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
