using System;
using System.Windows;
using System.Windows.Input;
using UFIP.EngChat.Common.Core;
using UFIP.EngChat.Common.Libraries;

namespace UFIP.EngChat.Components.Authentication
{
    /// <summary>
    /// ViewModel for LoginView.xaml : handles connexion to the server.
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
        /// Exposes the command that allow to log in.
        /// </summary>
        /// <value>
        /// Command : Login(passwordBox) - CanLogin()
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
        /// <summary>
        /// Login using the password of the passwordBox.
        /// </summary>
        /// <param name="parameter">The passwordBox of the view.</param>
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
        /// <summary>
        /// Determines whether this instance can login.
        /// </summary>
        /// <returns> <c>true</c> if the username is not null, neither empty or white spaces only ; otherwise, <c>false</c></returns>
        private bool CanLogin()
        {
            return !(string.IsNullOrWhiteSpace(Username));
        }
        #endregion

        #region VIEW-MODEL-BASE        
        private bool disposedValue = false; // Pour détecter les appels redondants

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                Username = null;
                LoginAction = null;
                Authenticated = null;

                disposedValue = true;
            }

        }
        #endregion

    }
}
