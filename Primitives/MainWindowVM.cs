using System;
using System.Collections.Generic;
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

namespace Primitives
{
    class MainWindowVM : INotifyPropertyChanged
    {
        // Create a model group
        public Model3DGroup modelGroup = new Model3DGroup();

        public DrawRectangleCommand DrawRectangleCommand { get; }

        public HelixViewport3D viewport;
        
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
        public MainWindowVM(HelixViewport3D viewport)
        {
            DrawRectangleCommand = new DrawRectangleCommand();
            this.viewport = viewport;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
