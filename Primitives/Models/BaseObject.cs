using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Primitives
{
    public abstract class BaseObject : LinesVisual3D
    {
        protected List<Point3D> PointsList { get; set; } = new List<Point3D>();

        public virtual void AddPoint(Point3D point)
        {
            Points.Add(point);
        }

        public virtual void UpdateLastPoint(Point3D point) { }

        public abstract bool IsEndCreate { get; }
    }
}
