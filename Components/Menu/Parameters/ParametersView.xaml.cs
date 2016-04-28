using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UFIP.EngChat.Components.Parameters
{
    /// <summary>
    /// Logique d'interaction pour Parameters.xaml
    /// </summary>
    public partial class ParametersView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametersView"/> class.
        /// </summary>
        public ParametersView()
        {
            InitializeComponent();
            var dataContext = new ParametersViewModel();
            dataContext.Disposed += DataContext_Disposed;
            DataContext = dataContext;
        }

        private void DataContext_Disposed()
        {
            Close();
        }
    }
}
