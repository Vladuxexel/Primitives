using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using SharpDX;
using HelixToolkit.Wpf;

namespace Primitives
{
    class MainWindowVM
    {
        private HelixViewport3D viewport;

        public double x, y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public MainWindowVM(HelixViewport3D viewport)
        {
            this.viewport = viewport;
            viewport.MouseDown += OnMouseClick;
        }

        public Viewport MainViewport { get; set; }

        public void OnMouseClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(viewport.CursorPosition.ToString());
        }
    }
}
