using HelixToolkit.Wpf;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Primitives.Models;

namespace Primitives.Manipulators
{
    internal class PolygonManipulator : Manipulator
    {
        private WirePolygon _poly;

        private readonly MainWindowVM _mainWindowVM;

        public PolygonManipulator(MainWindowVM mainWindowVm)
        {
            _mainWindowVM = mainWindowVm;
        }

        public override void Bind(ModelVisual3D source)
        {
            _poly = source as WirePolygon;
            UpdateGeometry();
        }

        public override void UnBind()
        {
            _poly = null;
            Model = null;
        }

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        protected override void UpdateGeometry()
        {
            if (_poly != null)
            {
                var mesh = new MeshBuilder(false, false);
                var controlPoint = _poly.Center;
                mesh.AddEllipsoid(controlPoint, 0.2, 0.2, 0);
                Model.Geometry = mesh.ToMesh();
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseDown" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. This event data reports details about the mouse button that was pressed and the handled state.</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            CaptureMouse();
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseMove" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsMouseCaptured)
            {
                if (_mainWindowVM.Viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    var point = _mainWindowVM.Viewport.CursorOnConstructionPlanePosition.Value;

                    _poly.Center = point;

                    _mainWindowVM.Props = _poly.GetProps();
                }
                UpdateGeometry();
            }
        }
    }
}
