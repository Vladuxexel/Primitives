using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Primitives
{
    class Rectangle : INotifyPropertyChanged
    {
        private double x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;//Координаты вершин
        double xc, yc, zc;//Координаты центра

        public Rectangle(string point1, string point2, MainWindowVM mainWindowVM)
        {
            x1 = Calculator.getCoordinate('X', point1);
            y1 = Calculator.getCoordinate('Y', point1);
            z1 = Calculator.getCoordinate('Z', point1);
            x3 = Calculator.getCoordinate('X', point2);
            y3 = Calculator.getCoordinate('Y', point2);
            z3 = Calculator.getCoordinate('Z', point2);
            x2 = x3; y2 = y3; z2 = z3; x4 = x1; y4 = y1; z4 = z1;
            drawRectangle(point1, point2, mainWindowVM);
        }

        private static void drawRectangle(string point1, string point2, MainWindowVM mainWindowVM)
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

        private bool isFigure(string point1, string point2, MainWindowVM mainWindowVM)
        {
            bool isInFigure = false;
            foreach (Rectangle rectangle in mainWindowVM.rectangles)
            {
                if (((Calculator.getCoordinate('X', point1) < (rectangle.x1 + rectangle.x3)) ||
                     (Calculator.getCoordinate('X', point1) > (rectangle.x1 + rectangle.x3))) &&
                    ((Calculator.getCoordinate('Y', point1) < (rectangle.y1 + rectangle.y3)) ||
                     (Calculator.getCoordinate('Y', point1) > (rectangle.y1 + rectangle.y3))) &&
                    ((Calculator.getCoordinate('Z', point1) < (rectangle.z1 + rectangle.z3)) ||
                     (Calculator.getCoordinate('Z', point1) > (rectangle.z1 + rectangle.z3)))
                )
                {
                    isInFigure = false;
                }
                else
                {
                    isInFigure = true;
                }
            }

            return isInFigure;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
