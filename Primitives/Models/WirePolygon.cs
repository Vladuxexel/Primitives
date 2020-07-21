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
using SharpDX;
using Color = System.Windows.Media.Color;

namespace Primitives
{
    /// <summary>
    /// Class representing a wire polygon object
    /// </summary>
    public class WirePolygon : BaseObject, INotifyPropertyChanged
    {
        private bool _isSelected;
        private Color _brush = Colors.Green;
        private static int _number = 1;

        private PolygonManipulator _manipulator;

        public WirePolygon(List<Point3D> points)
        {
            PointsList = points.ToList();

            for (int i = 0; i <= PointsList.Count - 2; i++)
            {
                Points.Add(PointsList[i]);
                Points.Add(PointsList[i + 1]);
            }

            Points.Add(PointsList[PointsList.Count - 1]);
            Points.Add(PointsList[0]);

            Thickness = 3;
            Color = _brush;
            Name = $"Полигон {_number++}";
            Type = Types.Polygon;
        }

        public WirePolygon(Point3D point)
        {
            Type = Types.Polygon;
            PointsList.Add(point);
            Thickness = 3;
            Color = _brush;
            Name = $"Полигон {_number++}";
        }

        /// <summary>
        /// Method that allows to redraw current object in runtime to get imagination of current second point's position
        /// </summary>
        /// <param name="point"></param>
        public override void UpdateLastPoint(Point3D point)
        {
            Points.Clear();

            for (int i = 0; i <= PointsList.Count - 2; i++)
            {
                Points.Add(PointsList[i]);
                Points.Add(PointsList[i + 1]);
            }
            Points.Add(PointsList.Last());
            Points.Add(point);
        }

        private void Update()
        {
            Points.Clear();

            for (int i = 0; i <= PointsList.Count - 2; i++)
            {
                Points.Add(PointsList[i]);
                Points.Add(PointsList[i + 1]);
            }
            Points.Add(PointsList.Last());
            Points.Add(PointsList.First());
        }

        public Point3D Center
        {
            get
            {
                return Calculator.Centroid(PointsList);
            }
            set
            {
                var deltaX = Calculator.Centroid(PointsList).X - value.X;
                var deltaY = Calculator.Centroid(PointsList).Y - value.Y;

                for (int i = 0; i < PointsList.Count; i++)
                {
                    PointsList[i] = new Point3D(PointsList[i].X - deltaX, PointsList[i].Y - deltaY, 0);
                }
                Update();
            }
        }

        /// <summary>
        /// Returns is object ended creation or not
        /// </summary>
        public override bool IsEndCreate
        {
            get => Calculator.IsInRadius(PointsList.Last(), PointsList.First(), 0.1);
        }

        /// <summary>
        /// Method that adds new point to list of object's points
        /// </summary>
        /// <param name="point"></param>
        public override void AddPoint(Point3D point)
        {
            PointsList.Add(point);
            UpdateLastPoint(point);
        }

        /// <summary>
        /// Returns is object selected or not
        /// </summary>
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

        /// <summary>
        /// Adding information about object's perimeter to datagrid
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ViewPropsVM> GetProps()
        {
            return new ObservableCollection<ViewPropsVM>()
            {
                new ViewPropsVM("Периметр",(v=>{ }), () => Perimeter)
            };
        }

        private double Perimeter
        {
            get { return Calculator.GetPerimeter(PointsList); }
        }

        /// <summary>
        /// Sets color of selected object
        /// </summary>
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

        public void BindManipulator(MainWindowVM mainWindowVm)
        {
            _manipulator = new PolygonManipulator(mainWindowVm)
            {
                Color = Colors.Blue,
            };
            _manipulator.Bind(this);
            mainWindowVm.viewport.Children.Add(_manipulator);
        }

        public override void DeleteManipulator(MainWindowVM mainWindowVm)
        {
            if (_manipulator != null)
            {
                _manipulator.UnBind();
                mainWindowVm.viewport.Children.Remove(_manipulator);
                mainWindowVm.CurrentManipulator = null;
            }
        }
    }
}
