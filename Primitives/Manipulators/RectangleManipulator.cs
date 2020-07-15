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
    enum Direction
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

    class RectangleManipulator : Manipulator
    {
        private WireRectangle _rect;

        private Direction _currentDirection;

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

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        protected override void UpdateGeometry()
        {
            if (_rect != null)
            {
                var mesh = new MeshBuilder(false, false);
                Point3D controlPoint = new Point3D(_rect.Center.X, _rect.Top, 0);
                mesh.AddBox(controlPoint, 0.3,0.3,0);
                
                controlPoint = new Point3D(_rect.Right, _rect.Center.Y, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = new Point3D(_rect.Center.X, _rect.Bottom, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = new Point3D(_rect.Left, _rect.Center.Y, 0);
                mesh.AddBox(controlPoint, 0.3, 0.3, 0);

                controlPoint = _rect.Center;
                mesh.AddEllipsoid(controlPoint,0.2,0.2,0);

                controlPoint = new Point3D(_rect.Left, _rect.Top, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Right, _rect.Top, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Right, _rect.Bottom, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);

                controlPoint = new Point3D(_rect.Left, _rect.Bottom, 0);
                mesh.AddEllipsoid(controlPoint, 0.1, 0.1, 0);
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

            var point = _mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value;

            if (Calculator.IsInRadius(point, new Point3D(_rect.Left, _rect.Top, 0), 0.3))
            {
                _currentDirection = Primitives.Direction.TopLeft;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Right, _rect.Top, 0), 0.3))
            {
                _currentDirection = Primitives.Direction.TopRight;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Right, _rect.Bottom, 0), 0.3))
            {
                _currentDirection = Primitives.Direction.BottomRight;
            }
            else if (Calculator.IsInRadius(point, new Point3D(_rect.Left, _rect.Bottom, 0), 0.3))
            {
                _currentDirection = Primitives.Direction.BottomLeft;
            }
            else if (Math.Abs(point.Y - _rect.Top) <= 0.3)
            {
                _currentDirection = Primitives.Direction.Top;
            }
            else if (Math.Abs(point.X - _rect.Right) <= 0.3)
            {
                _currentDirection = Primitives.Direction.Right;
            }
            else if (Math.Abs(point.Y - _rect.Bottom) <= 0.3)
            {
                _currentDirection = Primitives.Direction.Bottom;
            }
            else if (Math.Abs(point.X - _rect.Left) <= 0.3)
            {
                _currentDirection = Primitives.Direction.Left;
            }
            else if (Calculator.IsInRadius(point,_rect.Center,0.3))
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
                if (_mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    var point = _mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value;

                    switch (_currentDirection)
                    {
                        case Primitives.Direction.Top:
                            _rect.Top = point.Y;
                            break;

                        case Primitives.Direction.Right:
                            _rect.Right = point.X;
                            break;

                        case Primitives.Direction.Bottom:
                            _rect.Bottom = point.Y;
                            break;

                        case Primitives.Direction.Left:
                            _rect.Left = point.X;
                            break;

                        case Primitives.Direction.Center:
                            _rect.Center = point;
                            break;

                        case Primitives.Direction.TopLeft:
                            _rect.TopLeft = point;
                            break;

                        case Primitives.Direction.TopRight:
                            _rect.TopRight = point;
                            break;

                        case Primitives.Direction.BottomRight:
                            _rect.BottomRight = point;
                            break;

                        case Primitives.Direction.BottomLeft:
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
