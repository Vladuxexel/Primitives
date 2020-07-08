using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
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
    class WireRectangle : LinesVisual3D,  INotifyPropertyChanged
    {
        private Point3D _p1, _p2, _p3, _p4;
        private bool _isSelected;
        private Color _brush = Colors.Green;

        private double _length, _width;

        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChanged("Length");
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
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

        public WireRectangle(Point3D point1, Point3D point3)
        {
            _p1 = point1;
            _p3 = point3;
            _p2.X = _p3.X;
            _p2.Y = _p1.Y;
            _p4.X = _p1.X;
            _p4.Y = _p3.Y;
            Points.Add(_p1);
            Points.Add(_p2);
            Points.Add(_p2);
            Points.Add(_p3);
            Points.Add(_p3);
            Points.Add(_p4);
            Points.Add(_p4);
            Points.Add(_p1);
            Thickness = 5;
            Color = _brush;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
