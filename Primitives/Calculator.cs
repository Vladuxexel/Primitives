﻿using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Primitives
{
    /// <summary>
    /// A class that represents math calculations
    /// </summary>
    static class Calculator
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
            return GetDist(point1, point2) <= radius;
        }

        /// <summary>
        /// Returns center of polygon presented as list of it's points
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Point3D Centroid(List<Point3D> list)
        {
            var centroid = new Point3D(0, 0, 0);

            var signedArea = 0.0;
            var x0 = 0.0; // Current vertex X
            var y0 = 0.0; // Current vertex Y
            var x1 = 0.0; // Next vertex X
            var y1 = 0.0; // Next vertex Y
            var a = 0.0;  // Partial signed area

            // For all vertices except last
            var i = 0;
            for (i = 0; i < list.Count - 1; ++i)
            {
                x0 = list[i].X;
                y0 = list[i].Y;
                x1 = list[i + 1].X;
                y1 = list[i + 1].Y;
                a = x0 * y1 - x1 * y0;
                signedArea += a;
                centroid.X += (x0 + x1) * a;
                centroid.Y += (y0 + y1) * a;
            }

            // Do last vertex separately to avoid performing an expensive
            // modulus operation in each iteration.
            x0 = list[i].X;
            y0 = list[i].Y;
            x1 = list[0].X;
            y1 = list[0].Y;
            a = x0 * y1 - x1 * y0;
            signedArea += a;
            centroid.X += (x0 + x1) * a;
            centroid.Y += (y0 + y1) * a;

            signedArea *= 0.5;
            centroid.X /= (6.0 * signedArea);
            centroid.Y /= (6.0 * signedArea);

            return centroid;
        }
    }
}
