using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Primitives
{
    class Rectangle
    {
        double x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4, xc, yc, zc;

        public static void drawRectangle(string point1, string point2, MainWindowVM mainWindowVM)
        {
            double x1, x3, y1, y3, z1, z3;

            x1 = Calculator.getCoordinate('X', point1);
            x3 = Calculator.getCoordinate('X', point2);
            y1 = Calculator.getCoordinate('Y', point1);
            y3 = Calculator.getCoordinate('Y', point2);
            z1 = Calculator.getCoordinate('Z', point1);
            z3 = Calculator.getCoordinate('Z', point2);

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D((x1 + x3) / 2, (y1 + y3) / 2, (z1 + z3) / 2), (x3 - x1), (y3 - y1), (z3 - z1));

            // Create a mesh from the builder (and freeze it)
            var mesh = meshBuilder.ToMesh(true);

            // Create some materials
            var greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);

            // Add 3 models to the group (using the same mesh, that's why we had to freeze it)
            mainWindowVM.modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Material = greenMaterial });
        }
    }
}
