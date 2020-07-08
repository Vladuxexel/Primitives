using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SharpDX;
using Color = System.Windows.Media.Color;

namespace Primitives
{
    class Rectangle : MeshElement3D,  INotifyPropertyChanged
    {
        private double x1, y1, z1, x2, y2, z2, x3, y3, z3, x4, y4, z4;//Координаты вершин
        double xc, yc, zc;//Координаты центра
        private Point3D _p1;
        private Point3D _p2;

        private bool _isSelected;
        private Color _brush = Colors.Green;

        public bool IsSelected
        {
            get { return _isSelected;}
            set
            {
                _isSelected = value;
                SetSelectedColor();
            }
        }

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
            this.UpdateModel();
        }

        public Rectangle(Point3D point1, Point3D point2)
        {
            _p1 = point1;
            _p2 = point2;
        }

        protected override MeshGeometry3D Tessellate()
        {
            double x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4;
          
            x1 = _p1.X;
            x3 = _p2.X;
            y1 = _p1.Y;
            y3 = _p2.Y;
            z1 = 1;
            z3 = 1;
            x2 = x3; y2 = y3; z2 = z3; x4 = x1; y4 = y1; z4 = z1;

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D((x1 + x3) / 2, (y1 + y3) / 2, (z1 + z3) / 2), (x3 - x1), (y3 - y1), (z3 - z1));
           

            var ret = meshBuilder.ToMesh();
            this.Material = new DiffuseMaterial(new SolidColorBrush(_brush)); 


            return ret;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
