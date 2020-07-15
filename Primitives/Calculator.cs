using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Primitives
{
    /// <summary>
    /// A class that represents math calculations
    /// </summary>
    class Calculator
    {
        /// <summary>
        /// Returns a distance from point1 to point2
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetDist(Point3D point1, Point3D point2)
        {
            return point1.DistanceTo(point2);
        }

        /// <summary>
        /// Returns perimeter of polygon represented as a list of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static double GetPerimeter(List<Point3D> points)
        {
            double result = 0;

            for (int i = 0; i < points.Count - 2; i++)
            {
                result += GetDist(points[i], points[i + 1]);
            }

            result += GetDist(points[0], points[points.Count - 1]);

            return result;
        }

        /// <summary>
        /// Returns is point1 in radius with point2 or not
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool IsInRadius(Point3D point1, Point3D point2, double radius)
        {
            if (GetDist(point1, point2) <= radius)
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
