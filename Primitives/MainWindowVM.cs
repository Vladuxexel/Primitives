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
using System.Threading;
using System.Windows.Threading;
using Primitives.Commands;
using Color = SharpDX.Color;
using SelectionCommand = HelixToolkit.Wpf.SelectionCommand;

namespace Primitives
{
    class MainWindowVM : INotifyPropertyChanged
    {
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
            SelectingCommand = new SelectingCommand();
            this.viewport = viewport;
            var rect = new WireRectangle(new Point3D(-18,10,0), new Point3D(-3, 1, 0));
            viewport.Children.Add(rect);
            //rectangles.Add(new Rectangle());
            Model = modelGroup;
        }

        // Create a model group
        public Model3DGroup modelGroup = new Model3DGroup();

        public HelixViewport3D viewport;

        #region Commands definitions

        public RectangleButtonCommand RectangleButtonCommand { get; }
        public PolygonButtonCommand PolygonButtonCommand { get; }
        public DrawCommand DrawCommand { get; }
        public SelectingCommand SelectingCommand { get; }
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
