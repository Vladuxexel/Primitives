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
        private HelixViewport3D viewport;
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

        [Obsolete]
        public RelayCommand DrawCommand
        {
            get
            {
                return drawCommand ??
                    (drawCommand = new RelayCommand(obj =>
                    {
                        // Create a model group
                        var modelGroup = new Model3DGroup();

                        // Create a mesh builder and add a box to it
                        var meshBuilder = new MeshBuilder(false, false);
                        meshBuilder.AddBox(new Point3D(coordinate('X'), coordinate('Y'), 0), 0.1, 0.1, 0);

                        // Create a mesh from the builder (and freeze it)
                        var mesh = meshBuilder.ToMesh(true);

                        // Create some materials
                        var greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);

                        // Add 3 models to the group (using the same mesh, that's why we had to freeze it)
                        modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Material = greenMaterial });

                        // Set the property, which will be bound to the Content property of the ModelVisual3D (see MainWindow.xaml)
                        this.Model = modelGroup;
                    }));
            }
        }

        public MainWindowVM()
        {
            
        }

        [Obsolete]
        private double coordinate(char name)
        {
            int index = 0;
            double result = 0;
            string xyz = viewport.CurrentPosition.ToString();

            switch (name)
            {
                case 'X':
                    index = xyz.IndexOf(';');
                    xyz = xyz.Substring(0, index);
                    result = Convert.ToDouble(xyz);
                    break;
                case 'Y':
                    index = xyz.IndexOf(';');
                    xyz = xyz.Substring(index + 1);
                    index = xyz.IndexOf(';');
                    xyz = xyz.Substring(0, index);
                    result = Convert.ToDouble(xyz);
                    break;
                case 'Z':
                    index = xyz.IndexOf(';');
                    xyz = xyz.Substring(index + 1);
                    index = xyz.IndexOf(';');
                    xyz = xyz.Substring(index + 1);
                    result = Convert.ToDouble(xyz);
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
