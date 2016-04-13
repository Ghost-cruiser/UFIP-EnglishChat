using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFIP.EngChat.Common.Sources;
using UFIP.EngChat.Common.Libraries;
using UFIP.EngChat.Components;

namespace UFIP.EngChat
{
    /// <summary>
    /// Main View-Model of the application.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class MainWindowViewModel : ViewModelBase
    {
        #region PROP
        private Components.Authentication.LoginViewModel _login;
        private Components.Authentication.LoginViewModel Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }
        private Components.ChatPanel.ChatPanelViewModel ChatPanel { get; set; }
        private Components.Menu.MenuViewModel Menu { get; set; }

        private ViewModelBase _sourceView;
        /// <summary>
        /// Gets or sets the source view of the main panel. Implements OnPropertyChanged
        /// </summary>
        /// <value>
        /// The active ViewModel.
        /// </value>
        public ViewModelBase SourceView
        {
            get
            {
                return _sourceView;
            }
            set
            {
                if (_sourceView != null)
                {
                    _sourceView.Dispose();
                }
                
                _sourceView = value;
                OnPropertyChanged("SourceView");
            }
        }
        #endregion

        #region CTOR        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            UserSource.Center.PropertyChanged += Center_PropertyChanged;

            Login = new Components.Authentication.LoginViewModel();
            Login.Authenticated += On_Authentication;
            SourceView = Login;
        }

        #endregion

        #region EVENT HANDLERS        
        /// <summary>
        /// Handles the success of the connectinon.
        /// </summary>
        private void On_Authentication()
        {
            Components.Menu.MenuViewModel.Connected = true;
            ChatPanel = new Components.ChatPanel.ChatPanelViewModel();
            SourceView = ChatPanel;
        }

        /// <summary>
        /// Handles the the disposal of the source user, traducting a deconnection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Center_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Disposed")
            {
                Login = new Components.Authentication.LoginViewModel();
                Login.Authenticated += On_Authentication;
                SourceView = Login;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            SourceView = null;
        }
        #endregion
    }
}
