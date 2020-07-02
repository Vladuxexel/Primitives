using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using SharpDX;
using HelixToolkit.Wpf;
using MVVM;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Primitives
{
    class MainWindowVM : INotifyPropertyChanged
    {
        // Create a model group
        Model3DGroup modelGroup = new Model3DGroup();

        List<string> tempCootdinates = new List<string>();
        int clicks = 0;
        bool isRectangle = true;

        private HelixViewport3D viewport;


        public MainWindowVM()
        {

        }
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        Model3D model;
        public Model3D Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public MainWindowVM(HelixViewport3D viewport) : this()
        {
            this.viewport = viewport;
        }
        private RelayCommand drawCommand;

        public RelayCommand DrawCommand
        {
            get
            {
                return drawCommand ??
                    (drawCommand = new RelayCommand(obj =>
                    {
                        clicks++;
                        if (isRectangle)
                        {
                            if (clicks == 1)
                            {
                                tempCootdinates.Add(viewport.CursorOnConstructionPlanePosition.ToString());
                            }
                            else if (clicks == 2)
                            {
                                tempCootdinates.Add(viewport.CursorOnConstructionPlanePosition.ToString());
                                drawRectangle(tempCootdinates[0], tempCootdinates[1]);
                                tempCootdinates.Clear();
                                clicks = 0;
                                // Set the property, which will be bound to the Content property of the ModelVisual3D (see MainWindow.xaml)
                                Model = modelGroup;
                            }
                        }






                    }));
            }
        }

        public void drawRectangle(string point1, string point2)
        {
            double x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4;

            x1 = coordinate('X', point1);
            x3 = coordinate('X', point2);
            x2 = x3;
            x4 = x1;
            y1 = coordinate('Y', point1);
            y3 = coordinate('Y', point2);
            y2 = y3;
            y4 = y1;
            z1 = coordinate('Z', point1);
            z3 = coordinate('Z', point2);
            z2 = z3;
            z4 = z1;

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D((x3 - x1) * 0.5, (y3 - y1) * 0.5, (z3 - z1) * 0.5), (x3 - x1), (y3 - y1), (z3 - z1));

            // Create a mesh from the builder (and freeze it)
            var mesh = meshBuilder.ToMesh(true);

            // Create some materials
            var greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);

            // Add 3 models to the group (using the same mesh, that's why we had to freeze it)
            modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Material = greenMaterial });
        }

        private double coordinate(char name, string str)
        {
            int index = 0;
            double result = 0;

            switch (name)
            {
                case 'X':
                    index = str.IndexOf(';');
                    str = str.Substring(0, index);
                    result = Convert.ToDouble(str);
                    break;
                case 'Y':
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    index = str.IndexOf(';');
                    str = str.Substring(0, index);
                    result = Convert.ToDouble(str);
                    break;
                case 'Z':
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    index = str.IndexOf(';');
                    str = str.Substring(index + 1);
                    result = Convert.ToDouble(str);
                    break;
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
