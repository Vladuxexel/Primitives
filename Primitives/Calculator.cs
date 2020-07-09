using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public static double GetPerimeter(List<Point3D> points)
        {
            double result = 0;

            for (int i = 0; i < points.Count-1; i++)
            {
                result += GetDist(points[i], points[i + 1]);
            }

            result += GetDist(points[0], points[points.Count-1]);

            return result;
        }

        public static bool IsInRadius(Point3D point1, Point3D point2, double radius)
        {
            if (GetDist(point1,point2)<=radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
