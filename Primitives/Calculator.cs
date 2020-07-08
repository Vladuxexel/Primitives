using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Primitives
{
    class Calculator
    {
        public static double GetDist(Point3D point1, Point3D point2)
        {
            double x1 = point1.X;
            double x2 = point2.X;
            double y1 = point1.Y;
            double y2 = point2.Y;

            x1 = Math.Pow((x2 - x1),2);
            y1 = Math.Pow((y2 - y1), 2);

            return Math.Sqrt(x1 + y1);
        }
    }
}
