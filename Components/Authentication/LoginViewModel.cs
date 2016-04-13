using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UFIP.EngChat.Common.Core;
using UFIP.EngChat.Common.Libraries;

namespace UFIP.EngChat.Components.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class LoginViewModel : ViewModelBase
    {
        #region PROPERTIES        
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets the login action.
        /// </summary>
        /// <value>
        /// The login action.
        /// </value>
        public ICommand LoginAction { get; private set; }

        /// <summary>
        /// Handles authentication.
        /// </summary>
        public delegate void AuthHandler();
        /// <summary>
        /// Occurs when [authenticated].
        /// </summary>
        public event AuthHandler Authenticated;
        /// <summary>
        /// Called when [authentication].
        /// </summary>
        protected virtual void OnAuthentication()
        {
            if (Authenticated != null)
                Authenticated();
        }
        #endregion

        #region CTOR        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            LoginAction =  new RelayCommand(param => Login(param), param => CanLogin());
        }
        #endregion

        #region METHODS
        private void Login(object parameter)
        {
            var passbox = parameter as System.Windows.Controls.PasswordBox;
            var password = passbox.Password;

            try
            {
                XmppService.Initialize(Username, password);
                OnAuthentication();
            }
            catch (Exception exception)
            {
                //TODO : implement error message
                MessageBox.Show(exception.Message);
            }
           
        }

        private bool CanLogin()
        {
            return !(string.IsNullOrWhiteSpace(Username));
        }
        #endregion

        #region VIEW-MODEL-BASE        
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            Username = null;
            LoginAction = null;
        }
        #endregion

    }
}
