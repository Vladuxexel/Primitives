using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Primitives
{
    public abstract class BaseObject : LinesVisual3D
    {
        public List<Point3D> PointsList { get; set; } = new List<Point3D>();

        public string Name { get; set; }
        public Types Type { get; set; }

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
