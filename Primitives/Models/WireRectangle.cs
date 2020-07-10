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
    public class WireRectangle : BaseObject, INotifyPropertyChanged
    {
        private Point3D p1, p2, p3, p4;
        private bool _isSelected;
        private Color _brush = Colors.Green;

        public WireRectangle(Point3D point1)
        {
            PointsList.Add(point1);
            Points.Add(PointsList.First());
            Thickness = 3;
            Color = _brush;
        }

        public override void UpdateLastPoint(Point3D point)
        {
            Points.Clear();
            p1 = PointsList.First();
            p3 = point;
            p2 = new Point3D(p3.X, p1.Y, 0);
            p4 = new Point3D(p1.X, p3.Y, 0);
          
            Points.Add(p1);
            Points.Add(p2);
            Points.Add(p2);
            Points.Add(p3);
            Points.Add(p3);
            Points.Add(p4);
            Points.Add(p4);
            Points.Add(p1);
        }

        public override bool IsEndCreate
        {
            get => PointsList.Count >= 2;
        }

        public override void AddPoint(Point3D point)
        {
            PointsList.Add(point);
            UpdateLastPoint(point);
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
                new ViewPropsVM("Длина", value=> Length = value, () => Length),
                new ViewPropsVM("Ширина", value =>  Width = value, () => Width)
            };
        }

        private double Length
        {
            get
            {
                p1 = PointsList.First();
                p3 = PointsList.Last();
                p2 = new Point3D(p3.X, p1.Y, 0);
                return Calculator.GetDist(p1, p2);
            }
            set { SetLength(value); }
        }
        private double Width
        {
            get
            {
                p1 = PointsList.First();
                p3 = PointsList.Last();
                p4 = new Point3D(p1.X, p3.Y, 0);
                return Calculator.GetDist(p1, p4);
            }
            set { SetWidth(value); }
        }
        private void SetLength(double length)
        {
            double delta = length - Length;
            var point = PointsList.Last();
            PointsList.Remove(point);
            point = new Point3D(point.X + delta, point.Y, point.Z);
            PointsList.Add(point);

            UpdateLastPoint(point);
        }
        private void SetWidth(double width)
        {
            double delta = width - Width;
            var point = PointsList.Last();
            PointsList.Remove(point);
            point = new Point3D(point.X , point.Y - delta, point.Z);
            PointsList.Add(point);

            UpdateLastPoint(point);
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
