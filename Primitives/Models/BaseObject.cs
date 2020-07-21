using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Primitives
{
    public abstract class BaseObject : LinesVisual3D
    {
        public List<Point3D> PointsList { get; set; } = new List<Point3D>();

        public string Name { get; set; }
        protected Types Type { get; set; }

        public virtual void AddPoint(Point3D point)
        {
            Points.Add(point);
        }

        public virtual void UpdateLastPoint(Point3D point) { }
        public virtual void UpdateManipulator() { }

        public virtual void DeleteManipulator(MainWindowVM obj) { }

        public abstract bool IsEndCreate { get; }
        public abstract bool IsSelected { get; set; }
    }
}
