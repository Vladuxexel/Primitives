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
        private List<Point3D> _points;
        private bool _isSelected;
        private Color _brush = Colors.Green;
        private static int number = 0;

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
            Name = $"Полигон {number}";
            number++;
        }

        public WirePolygon(Point3D point)
        {
            PointsList.Add(point);
            Thickness = 3;
            Color = _brush;
            Name = $"Полигон {number}";
            number++;
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
    }
}
