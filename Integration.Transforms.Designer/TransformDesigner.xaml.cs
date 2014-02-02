using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Referee.Transforms.Designer
{
    /// <summary>
    /// Interaction logic for TransformDesigner.xaml
    /// </summary>
    public partial class TransformDesigner : UserControl
    {
        private Transform transform;

        public TransformDesigner()
        {
            InitializeComponent();
            this.transform = new Transform();
            this.DataContext = transform;            
        }

        public TransformDesigner(Transform transform)
        {
            InitializeComponent();
            this.transform = transform;
            this.DataContext = this.transform;
        }
    }
}
