using S22.Xmpp.Client;
using S22.Xmpp.Im;
using System;
using System.Net.Security;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using UFIP.EngChat.Common.Sources;

namespace UFIP.EngChat.Common.Core
{

    /// <summary>
    /// Singleton exposing the xmpp library. Handles its connection and its events.
    /// Fills the contexts
    /// </summary>
    public class XmppService : IDisposable
    {
        #region PROPERTIES
        private XmppClient LibClient;
        /// <summary>
        /// Exposes one xmpp client.
        /// </summary>
        /// <value>
        /// The current instance of the client. Must be initialized first.
        /// </value>
        public static XmppService Client { get; private set; }

        /// <summary>
        /// Gets the hostname to connect to directly from the current parameters.
        /// </summary>
        /// <value>
        /// The hostname defined in the parameters.
        /// </value>
        private static string Hostname { get { return Properties.Settings.Default.Host; } }

        /// <summary>
        /// Gets the port to connect to directly from the current parameters.
        /// </summary>
        /// <value>
        /// The hostname defined in the parameters.
        /// </value>
        private static int Port { get { return Properties.Settings.Default.Port; } }

        private bool ContextsHydrated { get; set; } = false;

        private System.Collections.Generic.List<StatusEventArgs> AwaitingStatus { get; set; } 
            = new System.Collections.Generic.List<StatusEventArgs>();
        #endregion

        #region CONSTRUCTORS
        private XmppService()
        {
            // TODO : Remove or improve
            LibClient = new XmppClient(Hostname, Port, false);
            LibClient.Connect();
        }
        private MethodInfo method { get; set; }
        private RemoteCertificateValidationCallback cb = delegate {
            return true;
        };

        private XmppService(string username, string password)
        {
            // TODO : Replace by conf
            LibClient = new XmppClient(Hostname, username, password, Port, false);


            LibClient.Message += HandleMessage;
            LibClient.RosterUpdated += LibClient_RosterUpdated;
            LibClient.StatusChanged += LibClient_StatusChanged;

            if(!LibClient.Authenticated)
            {
                LibClient = new XmppClient(Hostname, Port, false);
                LibClient.Connect();
                LibClient.Authenticate(username, password);
            }

            LibClient.Connect();
        }

        /// <summary>
        /// Initializes the client with the specified username and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The XMPP service.</returns>
        public static XmppService Initialize(string username, string password)
        {
            if (Client == null)
            {
                Client = new XmppService(username, password);
            }
            else
            {
                if (!Client.LibClient.Connected)
                    Client.LibClient.Connect();

                if (!Client.LibClient.Authenticated)
                    Client.LibClient.Authenticate(username, password);
            }

            Client.HydrateContexts();

            return Client;
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Sends the message to the connected server.
        /// </summary>
        /// <param name="message">The message format xmpp to send.</param>
        public void SendMessage(Message message)
        {
            LibClient.SendMessage(message);
        }

        /// <summary>
        /// Disconnects the instance of the client. Dispose all sources.
        /// </summary>
        public void Disconnect()
        {
            LibClient.Close();
            LibClient = null;

            ContactsSource.Center.Dispose();
            ConversationsSource.Center.Dispose();
            UserSource.Center.Dispose();

            Client = null;

        }

        /// <summary>
        /// Hydrates the contexts : provide all datas to the singletons providing the contexts of the application.
        /// </summary>
        private void HydrateContexts()
        {
            var roster = LibClient.GetRoster();

            var contactList = ContactsSource.Center.AllContacts;

            var usercenter = UserSource.Center;
            usercenter.ConnectedUser = new Models.UserViewModel(LibClient.Jid, LibClient.Username);

            foreach (var rosterItem in roster)
            {

                if (rosterItem.Jid.Node == "echoprofesseurs")
                {
                    usercenter.Role = Roles.Professeur;
                }

                contactList.Add(rosterItem);
            }

            ContextsHydrated = true;

            LoadAwaitingStatus();
        }
        #endregion


        #region EVENTS        
        /// <summary>
        /// Handles the reception of a message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        private void HandleMessage(object sender, MessageEventArgs e)
        {
            Task task = new Task(delegate {
                ConversationsSource.Center.ReceiveMessage(e.Message, e.Jid);
            });
            task.Start();
        }

        /// <summary>
        /// Handles a change in the contact list when the event is sent by the XMPP client.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RosterUpdatedEventArgs"/> instance containing the event data.</param>
        private void LibClient_RosterUpdated(object sender, RosterUpdatedEventArgs e)
        {
            Task task = new Task(delegate {
                ContactsSource.Center.UpdateList(e.Removed, e.Item);
            });
            task.Start(); 
        }

        /// <summary>
        /// Handles a change of status when the event is triggered by the XMPP client.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="StatusEventArgs"/> instance containing the event data.</param>
        private void LibClient_StatusChanged(object sender, StatusEventArgs e)
        {
            if (ContextsHydrated)
            {
                    ContactsSource.Center.UpdateContact(e.Jid, e.Status);
            }
            else
                AwaitingStatus.Add(e);
        }

        private void loadAwaitingStatus()
        {
            foreach (var e in AwaitingStatus)
            {
                ContactsSource.Center.UpdateContact(e.Jid, e.Status);
            }

            AwaitingStatus = null;
        }
        /// <summary>
        /// Load the awaiting status threw a task using loadAwaitingStatus.
        /// </summary>
        private void LoadAwaitingStatus()
        {
                loadAwaitingStatus();
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Disconnect();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                Client = null;
                ContextsHydrated = false;

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~XmppService() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.        
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion

    }
}