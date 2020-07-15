using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace Primitives
{
    public class WireRectangle : BaseObject, INotifyPropertyChanged
    {
        private Point3D _p1, /*_p2,*/ _p3; /*_p4;*/
        private bool _isSelected;
        private Color _brush = Colors.Green;
        private readonly int number = 0;
        private bool _isEndCreated;

        public WireRectangle(Point3D point1)
        {
            _p1 = point1;
            Thickness = 3;
            Color = _brush;
            Name = $"Прямоугольник {number}";
            number++;
        }

        public override void UpdateLastPoint(Point3D point)
        {
            Points.Clear();

            var p3 = point;
            var p2 = new Point3D(p3.X, _p1.Y, 0);
            var p4 = new Point3D(_p1.X, p3.Y, 0);

            Points.Add(_p1);
            Points.Add(p2);
            Points.Add(p2);
            Points.Add(p3);
            Points.Add(p3);
            Points.Add(p4);
            Points.Add(p4);
            Points.Add(_p1);
        }

        private void Update()
        {
            UpdateLastPoint(_p3);
        }

        public double Top
        {
            get => _p1.Y;
            set
            {
                _p1 = new Point3D(_p1.X, value, _p1.Z);
                Update();
            }
        }
        public double Right
        {
            get => _p3.X;
            set
            {
                _p3 = new Point3D(value, _p3.Y, _p3.Z);
                Update();
            }
        }
        public double Bottom
        {
            get => _p3.Y;
            set
            {
                _p3 = new Point3D(_p3.X, value, _p3.Z);
                Update();
            }
        }
        public double Left
        {
            get => _p1.X;
            set
            {
                _p1 = new Point3D(value, _p1.Y, _p1.Z);
                Update();
            }
        }

        public Point3D Center
        {
            get => new Point3D((_p1.X + _p3.X) / 2, (_p1.Y + _p3.Y) / 2, 0);
            set
            {
                var deltaX = ((_p1.X + _p3.X) / 2) - value.X;
                var deltaY = ((_p1.Y + _p3.Y) / 2) - value.Y;

                _p1 = new Point3D(_p1.X - deltaX, _p1.Y - deltaY, 0);
                _p3 = new Point3D(_p3.X - deltaX, _p3.Y - deltaY, 0);
                Update();
            }
        }

        public Point3D TopLeft
        {
            get => _p1;
            set
            {
                _p1 = value;
                Update();
            }
        }
        public Point3D TopRight
        {
            get => new Point3D(_p3.X, _p1.Y, 0);
            set
            {
                var deltaX = _p3.X - value.X;
                var deltaY = _p1.Y - value.Y;
                _p1 = new Point3D(_p1.X, _p1.Y - deltaY, 0);
                _p3 = new Point3D(_p3.X - deltaX, _p3.Y, 0);
                Update();
            }
        }
        public Point3D BottomRight
        {
            get => _p3;
            set
            {
                _p3 = value;
                Update();
            }
        }
        public Point3D BottomLeft
        {
            get => new Point3D(_p1.X, _p3.Y, 0);
            set
            {
                var deltaX = _p1.X - value.X;
                var deltaY = _p3.Y - value.Y;
                _p1 = new Point3D(_p1.X - deltaX, _p1.Y, 0);
                _p3 = new Point3D(_p3.X, _p3.Y - deltaY, 0);
                Update();
            }
        }

        public override bool IsEndCreate
        {
            get => _isEndCreated;
        }

        public override void AddPoint(Point3D point)
        {
            _p3 = point;
            _isEndCreated = true;
            UpdateLastPoint(point);
        }

        public override bool IsSelected
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
                new ViewPropsVM("Длина", value=> Length = value, () => Length),
                new ViewPropsVM("Ширина", value =>  Width = value, () => Width)
            };
        }

        private double Length
        {
            get
            {
                return Calculator.GetDist(_p1, new Point3D(_p3.X, _p1.Y, _p1.Z));
            }
            set { SetLength(value); }
        }
        private double Width
        {
            get
            {
                return Calculator.GetDist(_p1, new Point3D(_p1.X, _p3.Y, _p1.Z));
            }
            set { SetWidth(value); }
        }

        private void SetLength(double length)
        {
            double delta = length - Length;
            _p3 = new Point3D(_p3.X + delta, _p3.Y, _p3.Z);
            UpdateLastPoint(_p3);
        }
        private void SetWidth(double width)
        {
            double delta = width - Width;
            _p3 = new Point3D(_p3.X, _p3.Y - delta, _p3.Z);
            UpdateLastPoint(_p3);
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
