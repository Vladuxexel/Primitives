using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Primitives.Models;

namespace Primitives.Manipulators
{
    internal class RectangleManipulator : Manipulator
    {
        private WireRectangle _rect;

        private Directions _currentDirection;

        private readonly MainWindowVM _mainWindowVM;

        public RectangleManipulator(MainWindowVM mainWindowVm)
        {
            _mainWindowVM = mainWindowVm;
        }

        public override void Bind(ModelVisual3D source)
        {
            _rect = source as WireRectangle;
            UpdateGeometry();
        }

        public override void UnBind()
        {
            _rect = null;
            Model = null;
        }

        public void Update()
        {
            UpdateGeometry();
        }

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        protected override void UpdateGeometry()
        {
            if (_rect != null)
            {
                var mesh = new MeshBuilder(false, false);
                Point3D controlPoint = new Point3D(_rect.Center.X, _rect.Top, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = new Point3D(_rect.Right, _rect.Center.Y, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = new Point3D(_rect.Center.X, _rect.Bottom, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = new Point3D(_rect.Left, _rect.Center.Y, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = _rect.Center;
                mesh.AddEllipsoid(controlPoint, 0.2, 0.2, 0);

                controlPoint = new Point3D(_rect.Left, _rect.Top, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Right, _rect.Top, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Right, _rect.Bottom, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Left, _rect.Bottom, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);
                Model.Geometry = mesh.ToMesh();
                Model.Material = new DiffuseMaterial(new SolidColorBrush(Color));
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.MouseDown" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs" /> that contains the event data. This event data reports details about the mouse button that was pressed and the handled state.</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            var point = _mainWindowVM.Viewport.CursorOnConstructionPlanePosition.Value;

            if (Calculator.IsInRadius(point, new Point3D(_rect.Left, _rect.Top, 0), 0.3))
            {
                _currentDirection = Directions.TopLeft;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Right, _rect.Top, 0), 0.3))
            {
                _currentDirection = Directions.TopRight;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Right, _rect.Bottom, 0), 0.3))
            {
                _currentDirection = Directions.BottomRight;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Left, _rect.Bottom, 0), 0.3))
            {
                _currentDirection = Directions.BottomLeft;
            }
            else if (Math.Abs(point.Y - _rect.Top) <= 0.3)
            {
                _currentDirection = Directions.Top;
            }
            else if (Math.Abs(point.X - _rect.Right) <= 0.3)
            {
                _currentDirection = Directions.Right;
            }
            else if (Math.Abs(point.Y - _rect.Bottom) <= 0.3)
            {
                _currentDirection = Directions.Bottom;
            }
            else if (Math.Abs(point.X - _rect.Left) <= 0.3)
            {
                _currentDirection = Directions.Left;
            }
            else if (Calculator.IsInRadius(point, _rect.Center, 0.3))
            {
                _currentDirection = Directions.Center;
            }
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

                    switch (_currentDirection)
                    {
                        case Directions.Top:
                            _rect.Top = point.Y;
                            break;

                        case Directions.Right:
                            _rect.Right = point.X;
                            break;

                        case Directions.Bottom:
                            _rect.Bottom = point.Y;
                            break;

                        case Directions.Left:
                            _rect.Left = point.X;
                            break;

                        case Directions.Center:
                            _rect.Center = point;
                            break;

                        case Directions.TopLeft:
                            _rect.TopLeft = point;
                            break;

                        case Directions.TopRight:
                            _rect.TopRight = point;
                            break;

                        case Directions.BottomRight:
                            _rect.BottomRight = point;
                            break;

                        case Directions.BottomLeft:
                            _rect.BottomLeft = point;
                            break;
                    }
                    _mainWindowVM.Props = _rect.GetProps();
                }
                UpdateGeometry();
            }
        }
    }
}
