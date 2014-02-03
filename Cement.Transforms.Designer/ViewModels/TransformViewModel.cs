using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Glue.Transforms.Designer.ViewModels
{
    /// <summary>
    /// The transform view model 
    /// </summary>
    public class TransformViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public ObservableCollection<TransformModelViewModel> Inputs { get; set; }

        /// <summary>
        /// Gets or sets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public ObservableCollection<TransformModelViewModel> Outputs { get; set; }

        /// <summary>
        /// Gets or sets the functions.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        public ObservableCollection<FunctionViewModel> Functions { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public ObservableCollection<LinkViewModel> Links { get; set; }
        
    }
}
