using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using SharpDX;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Primitives.Commands;

namespace Primitives
{
    class MainWindowVM : INotifyPropertyChanged
    {
        MeshGeometry3D side1Plane = new MeshGeometry3D();
       // ObservableCollection<Rectangle> rectangles = new ObservableCollection<Rectangle>();

        public int clicks = 0;
        public bool isRectangle = false;
        public bool isPolygon = false;
        public readonly List<Point3D> tempCoordinates = new List<Point3D>();

        public MainWindowVM(HelixViewport3D viewport)
        {
            DrawCommand = new DrawCommand();
            RectangleButtonCommand = new RectangleButtonCommand();
            PolygonButtonCommand = new PolygonButtonCommand();
            this.viewport = viewport;
            //rectangles.Add(new Rectangle());
            Model = modelGroup;
            viewport.MouseDoubleClick += Viewport_MouseDoubleClick;
        }

        private void Viewport_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = viewport.Viewport.FindHits(e.GetPosition(viewport));

            foreach (var elem in viewport.Children.OfType<Rectangle>())
            {
                elem.IsSelected = false;
            }

            if (result.Any())
            {
                if (result.First().Visual is Rectangle geom)
                {
                    geom.IsSelected = true;
                }
            }
        }

        // Create a model group
        public Model3DGroup modelGroup = new Model3DGroup();

        public HelixViewport3D viewport;

        #region Commands definitions

        public RectangleButtonCommand RectangleButtonCommand { get; }
        public PolygonButtonCommand PolygonButtonCommand { get; }
        public DrawCommand DrawCommand { get; }
        #endregion

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
