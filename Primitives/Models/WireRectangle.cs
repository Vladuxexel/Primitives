﻿using Primitives.Manipulators;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Color = System.Windows.Media.Color;

namespace Primitives
{
    /// <summary>
    /// Class representing a wire rectangle object
    /// </summary>
    public sealed class WireRectangle : BaseObject, INotifyPropertyChanged
    {
        private Point3D _p1, _p3;
        private bool _isSelected;
        private Color _brush = Colors.Green;
        private static int _number = 1;
        private bool _isEndCreated;

        private RectangleManipulator _manipulator;

        public WireRectangle(Point3D point1)
        {
            _p1 = point1;
            Thickness = 3;
            Color = _brush;
            Name = $"Прямоугольник {_number++}";
            Type = Types.Rectangle;
        }

        /// <summary>
        /// Updating rectangle's manipulator
        /// </summary>
        public override void UpdateManipulator()
        {
            _manipulator.Update();
        }

        /// <summary>
        /// Method that allows to redraw current rectangle in runtime to get imagination of current second point's position
        /// </summary>
        /// <param name="point"></param>
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

        /// <summary>
        /// Rectangle's points for replacing manipulator
        /// </summary>
        public double Top
        {
            get => _p1.Y;
            set
            {
                _p1 = new Point3D(_p1.X, value, _p1.Z);
                UpdateLastPoint(_p3);
            }
        }
        public double Right
        {
            get => _p3.X;
            set
            {
                _p3 = new Point3D(value, _p3.Y, _p3.Z);
                UpdateLastPoint(_p3);
            }
        }
        public double Bottom
        {
            get => _p3.Y;
            set
            {
                _p3 = new Point3D(_p3.X, value, _p3.Z);
                UpdateLastPoint(_p3);
            }
        }
        public double Left
        {
            get => _p1.X;
            set
            {
                _p1 = new Point3D(value, _p1.Y, _p1.Z);
                UpdateLastPoint(_p3);
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
                UpdateLastPoint(_p3);
            }
        }
        public Point3D TopLeft
        {
            get => _p1;
            set
            {
                _p1 = value;
                UpdateLastPoint(_p3);
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
                UpdateLastPoint(_p3);
            }
        }
        public Point3D BottomRight
        {
            get => _p3;
            set
            {
                _p3 = value;
                UpdateLastPoint(_p3);
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
                UpdateLastPoint(_p3);
            }
        }

        /// <summary>
        /// Returns is object ended creation or not
        /// </summary>
        public override bool IsEndCreate => _isEndCreated;

        /// <summary>
        /// Method that adds new point to list of object's points
        /// </summary>
        /// <param name="point"></param>
        public override void AddPoint(Point3D point)
        {
            _p3 = point;
            _isEndCreated = true;
            UpdateLastPoint(point);
        }

        /// <summary>
        /// Returns is object selected or not
        /// </summary>
        public override bool IsSelected
        {
            get => _isSelected;
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
                new ViewPropsVM("Длина", value=> Length = value, () => Length),
                new ViewPropsVM("Ширина", value =>  Width = value, () => Width)
            };
        }

        /// <summary>
        /// Returns length and width of rectangle
        /// </summary>
        private double Length
        {
            get => Calculator.GetDist(_p1, new Point3D(_p3.X, _p1.Y, _p1.Z));
            set => SetLength(value);
        }
        private double Width
        {
            get => Calculator.GetDist(_p1, new Point3D(_p1.X, _p3.Y, _p1.Z));
            set => SetWidth(value);
        }

        /// <summary>
        /// Sets length of selected rectangle
        /// </summary>
        /// <param name="length"></param>
        private void SetLength(double length)
        {
            var delta = length - Length;
            _p3 = new Point3D(_p3.X + delta, _p3.Y, _p3.Z);
            UpdateLastPoint(_p3);
            UpdateManipulator();
        }

        /// <summary>
        /// Sets width of selected rectangle
        /// </summary>
        /// <param name="width"></param>
        private void SetWidth(double width)
        {
            var delta = width - Width;
            _p3 = new Point3D(_p3.X, _p3.Y - delta, _p3.Z);
            UpdateLastPoint(_p3);
            UpdateManipulator();
        }

        /// <summary>
        /// Sets color of selected rectangle
        /// </summary>
        private void SetSelectedColor()
        {
            _brush = _isSelected ? Colors.Red : Colors.Green;
            Color = _brush;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Binding a manipulator to it's object
        /// </summary>
        /// <param name="mainWindowVm"></param>
        public void BindManipulator(MainWindowVM mainWindowVm)
        {
            _manipulator = new RectangleManipulator(mainWindowVm)
            {
                Color = Colors.Blue,
            };
            _manipulator.Bind(this);
            mainWindowVm.Viewport.Children.Add(_manipulator);
        }

        /// <summary>
        /// Deletes a manipulator from it's object
        /// </summary>
        /// <param name="mainWindowVm"></param>
        public override void DeleteManipulator(MainWindowVM mainWindowVm)
        {
            if (_manipulator != null)
            {
                _manipulator.UnBind();
                mainWindowVm.Viewport.Children.Remove(_manipulator);
                mainWindowVm.CurrentManipulator = null;
            }
        }
    }
}
