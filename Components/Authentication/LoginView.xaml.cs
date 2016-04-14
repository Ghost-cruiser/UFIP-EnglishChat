using System.Windows.Controls;

namespace UFIP.EngChat.Components.Authentication
{
    /// <summary>
    /// LoginView.xaml interacts with its datacontext binding the username (:Username), 
    /// and passing its passwordBox to the bound command (:LoginAction).
    /// </summary>
    public partial class LoginView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginView"/> class.
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
        }
    }
}
