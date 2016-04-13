using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UFIP.EngChat.Common.Sources;
using UFIP.EngChat.Common.Libraries;
using UFIP.EngChat.Components.Parameters;
using UFIP.EngChat.Common.Core;

namespace UFIP.EngChat.Components.Menu
{
    /// <summary>
    /// View-Model for menu.
    /// </summary>
    public class MenuViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the client is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public static bool Connected { get; set; } = false;

        private static ICommand _parameters;
        /// <summary>
        /// Gets the parameters of the application.
        /// </summary>
        /// <value>
        /// Command : ShowParameter() - always
        /// </value>
        public static ICommand Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new RelayCommand(

                        param => ShowParameters(),
                        param => true
                    );
                }
                return _parameters;
            }

            private set
            {
                _parameters = value;
            }
        }

        private static ICommand _deconnexion;
        /// <summary>
        /// Exposes the deconnexion command.
        /// </summary>
        /// <value>
        /// Command : Deconnect() - if Connected.
        /// </value>
        public static ICommand Deconnexion
        {
            get
            {
                if (_deconnexion == null)
                {
                    _deconnexion = new RelayCommand(

                        param => Disconnect(),
                        param => CheckConnexion()
                    );
                }
                return _deconnexion;
            }

            private set
            {
                _deconnexion = value;
            }
        }

        private static ICommand _leave;
        /// <summary>
        /// Gets the leave command.
        /// </summary>
        /// <value>
        /// Command : CloseApplication() - always.
        /// </value>
        public static ICommand Leave
        {
            get
            {
                if (_leave == null)
                {
                    _leave = new RelayCommand(

                        param => CloseApplication(),
                        param => true
                    );
                }
                return _leave;
            }

            private set
            {
                _deconnexion = value;
            }
        }


        private static bool CheckConnexion()
        {
            return Connected;
        }

        /// <summary>
        /// Disconnects the client.
        /// </summary>
        private static void Disconnect()
        {
            XmppService.Client.Disconnect();
        }

        /// <summary>
        /// Shows the parameters of the application in a new window.
        /// </summary>
        private static void ShowParameters()
        {
            ParametersView view = new ParametersView();
            view.Show();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        private static void CloseApplication()
        {
            XmppService.Client.Disconnect();
            Application.Current.Shutdown();
        }
        
    }
}
