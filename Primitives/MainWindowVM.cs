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

namespace Primitives
{
    class MainWindowVM
    {
        private HelixViewport3D viewport;

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
                        MessageBox.Show(viewport.CursorPosition.ToString());
                    }));
            }
        }

        public MainWindowVM()
        {
            // Create a model group
            var modelGroup = new Model3DGroup();

            // Create a mesh builder and add a box to it
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Point3D(0, 0, 1), 1, 2, 0.5);
            meshBuilder.AddBox(new Rect3D(0, 0, 1.2, 0.5, 1, 0.4));

            // Create a mesh from the builder (and freeze it)
            var mesh = meshBuilder.ToMesh(true);

            // Create some materials
            var greenMaterial = MaterialHelper.CreateMaterial(Colors.Green);
            var insideMaterial = MaterialHelper.CreateMaterial(Colors.Yellow);

            // Add 3 models to the group (using the same mesh, that's why we had to freeze it)
            modelGroup.Children.Add(new GeometryModel3D { Geometry = mesh, Material = greenMaterial, BackMaterial = insideMaterial });

            // Set the property, which will be bound to the Content property of the ModelVisual3D (see MainWindow.xaml)
            this.Model = modelGroup;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public Model3D Model { get; set; }
    }
}
