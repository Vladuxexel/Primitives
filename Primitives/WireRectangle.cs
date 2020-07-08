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
    public class WireRectangle : LinesVisual3D, INotifyPropertyChanged
    {

        public ObservableCollection<ViewPropsVM> GetProps()
        { 
            return new ObservableCollection<ViewPropsVM>()
            {
                new ViewPropsVM("Ширина", value =>  Length = value, () => Length),
                new ViewPropsVM("Высота", value =>  Width = value, () => Width)
            };
        } 


        private Point3D _p1, _p2, _p3, _p4;
        private bool _isSelected;
        private Color _brush = Colors.Green;

        public double Length
        {
            get { return Calculator.GetDist(_p1, _p2); }
            set { SetLength(value); }
        }
        public double Width
        {
            get { return Calculator.GetDist(_p1, _p4); }
            set { SetWidth(value); }
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

        private void SetLength(double length)
        {
            double delta = 0;
            double curLength = Calculator.GetDist(_p1, _p2);

            delta = length - curLength;

            if (delta > 0)
            {
                _p2.X += delta;
                _p3.X += delta;
            }
            else
            {
                _p2.X -= Math.Abs(delta);
                _p3.X -= Math.Abs(delta);
            }
            Points.Clear();
            Points.Add(_p1);
            Points.Add(_p2);
            Points.Add(_p2);
            Points.Add(_p3);
            Points.Add(_p3);
            Points.Add(_p4);
            Points.Add(_p4);
            Points.Add(_p1);
        }
        private void SetWidth(double width)
        {
            double delta = 0;
            double curWidth = Calculator.GetDist(_p1, _p4);

            delta = width - curWidth;

            if (delta > 0)
            {
                _p3.Y -= delta;
                _p4.Y -= delta;
            }
            else
            {
                _p3.Y += Math.Abs(delta);
                _p4.Y += Math.Abs(delta);
            }
            Points.Clear();
            Points.Add(_p1);
            Points.Add(_p2);
            Points.Add(_p2);
            Points.Add(_p3);
            Points.Add(_p3);
            Points.Add(_p4);
            Points.Add(_p4);
            Points.Add(_p1);
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
