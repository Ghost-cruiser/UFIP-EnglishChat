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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UFIP.EngChat.Components.Authentication
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public delegate void AuthHandler(string username, string password);
        public event AuthHandler Authentication;
         
        public LoginView()
        {
            InitializeComponent();
            //DataContext = new LoginViewModel();
        }

        protected virtual void OnAuthentication()
        {
            Authentication(tb_username.Text, pb_password.Password);
        }

        private void btn_connection_Click(object sender, RoutedEventArgs e)
        {
            OnAuthentication();
        }
    }
}
