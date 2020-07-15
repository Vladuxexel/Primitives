using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace Primitives
{
    class PolygonManipulator : Manipulator
    {
        /// <summary>
        /// The last point.
        /// </summary>
        private Point3D lastPoint;

        private WirePolygon _poly;

        private Direction _currentDirection;

        #region MetaDirection

        /// <summary>
        /// Identifies the <see cref="Direction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(Vector3D),
            typeof(TranslateManipulator),
            new UIPropertyMetadata(UpdateGeometry));

        /// <summary>
        /// Gets or sets the direction of the translation.
        /// </summary>
        /// <value> The direction. </value>
        public Vector3D Direction
        {
            get
            {
                return (Vector3D)this.GetValue(DirectionProperty);
            }

            set
            {
                this.SetValue(DirectionProperty, value);
            }
        }

        #endregion

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
                Point3D controlPoint;

                controlPoint = _poly.Center;
                mesh.AddEllipsoid(controlPoint, 0.2, 0.2, 0);
                Model.Geometry = mesh.ToMesh();
            }
        }

        private MainWindowVM _mainWindowVM;

        public PolygonManipulator(MainWindowVM mainWindowVm)
        {
            _mainWindowVM = mainWindowVm;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseDown" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. This event data reports details about the mouse button that was pressed and the handled state.</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            var point = _mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value;

            if (Calculator.IsInRadius(point, _poly.Center, 0.3))
            {
                _currentDirection = Primitives.Direction.Center;
            }
            this.CaptureMouse();
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseMove" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.IsMouseCaptured)
            {
                var hitPlaneOrigin = this.ToWorld(this.Position);
                var p = e.GetPosition(this.ParentViewport);
                var nearestPoint = this.GetNearestPoint(p, hitPlaneOrigin, this.HitPlaneNormal);
                if (nearestPoint == null)
                {
                    return;
                }

                var delta = this.ToLocal(nearestPoint.Value) - this.lastPoint;
                this.Value += Vector3D.DotProduct(delta, this.Direction);

                nearestPoint = this.GetNearestPoint(p, hitPlaneOrigin, this.HitPlaneNormal);
                if (nearestPoint != null)
                {
                    this.lastPoint = this.ToLocal(nearestPoint.Value);
                }

                if (_mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    var point = _mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value;

                    _poly.Center = point;

                    _mainWindowVM.Props = _poly.GetProps();
                }
                UpdateGeometry();
            }
        }

        /// <summary>
        /// Gets the nearest point on the translation axis.
        /// </summary>
        /// <param name="position">
        /// The position (in screen coordinates).
        /// </param>
        /// <param name="hitPlaneOrigin">
        /// The hit plane origin (world coordinate system).
        /// </param>
        /// <param name="hitPlaneNormal">
        /// The hit plane normal (world coordinate system).
        /// </param>
        /// <returns>
        /// The nearest point (world coordinates) or null if no point could be found.
        /// </returns>
        private Point3D? GetNearestPoint(Point position, Point3D hitPlaneOrigin, Vector3D hitPlaneNormal)
        {
            var hpp = this.GetHitPlanePoint(position, hitPlaneOrigin, hitPlaneNormal);
            if (hpp == null)
            {
                return null;
            }

            var ray = new Ray3D(this.ToWorld(this.Position), this.ToWorld(this.Direction));
            return ray.GetNearest(hpp.Value);
        }
    }
}
