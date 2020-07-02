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

namespace Primitives
{
    class MainWindowVM
    {
        private HelixViewport3D viewport;

        public MainWindowVM(HelixViewport3D viewport)
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
    }
}
