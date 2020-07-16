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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;
using Primitives.Commands;
using Primitives.Models;
using Color = SharpDX.Color;
using SelectionCommand = HelixToolkit.Wpf.SelectionCommand;

namespace Primitives
{
    class MainWindowVM : BaseViewModel
    {
        public bool isRectangle = false;
        public bool isPolygon = false;

        public readonly List<Point3D> tempCoordinates = new List<Point3D>();
        public BaseObject CurrentObject { get; set; }

        public Manipulator CurrentManipulator { get; set; }

        public ViewportChildCollection Collection { get; set; }

        public List<ViewportChildCollection> Tree { get; set; }

        public MainWindowVM(HelixViewport3D viewport)
        {
            Collection = new ViewportChildCollection(viewport);
            Tree = new List<ViewportChildCollection> {Collection};
            DrawCommand = new DrawCommand();
            RectangleButtonCommand = new RectangleButtonCommand();
            PolygonButtonCommand = new PolygonButtonCommand();
            SelectingCommand = new SelectingCommand();
            this.viewport = viewport;
            var rect = new WireRectangle(new Point3D(-18,10,0));
            rect.AddPoint(new Point3D(-5,2,0));
            Collection.Add(rect);
            tempCoordinates.Add(new Point3D(0,0,0));
            tempCoordinates.Add(new Point3D(4, 4, 0));
            tempCoordinates.Add(new Point3D(8, 0, 0));
            tempCoordinates.Add(new Point3D(7, -5, 0));
            tempCoordinates.Add(new Point3D(2, -5, 0));
            var poly = new WirePolygon(tempCoordinates);
            Collection.Add(poly);
            tempCoordinates.Clear();
            viewport.MouseMove += ViewportOnMouseMove;
        }

        private void ViewportOnMouseMove(object sender, MouseEventArgs e)
        {
            if (viewport.CursorOnConstructionPlanePosition.HasValue && CurrentObject != null)
            {
                var point = viewport.CursorOnConstructionPlanePosition.Value;
                CurrentObject.UpdateLastPoint(point);
            }
        }

        private ObservableCollection<ViewPropsVM> _props = new ObservableCollection<ViewPropsVM>();
        public ObservableCollection<ViewPropsVM> Props
        {
            get => _props;
            set
            {
                _props = value;
                OnPropertyChanged(nameof(Props));
            }
        }

        public readonly HelixViewport3D viewport;

        #region Commands definitions
        public RectangleButtonCommand RectangleButtonCommand { get; }
        public PolygonButtonCommand PolygonButtonCommand { get; }
        public DrawCommand DrawCommand { get; }
        public SelectingCommand SelectingCommand { get; }
        #endregion
    }
    public enum Types
    {
        Rectangle,
        Polygon
    }
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        Center,
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft
    }
}
