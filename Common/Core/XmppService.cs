using S22.Xmpp.Client;
using S22.Xmpp.Im;
using System.Threading.Tasks;
using UFIP.EngChat.Common.Sources;

namespace UFIP.EngChat.Common.Core
{

    /// <summary>
    /// Singleton exposing the xmpp library. Handles its connection and its events.
    /// Fills the contexts
    /// </summary>
    public class XmppService
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

        private XmppService(string username, string password)
        {
            // TODO : Replace by conf
            LibClient = new XmppClient(Hostname, username, password, Port, false);

            LibClient.Message += HandleMessage;
            LibClient.RosterUpdated += LibClient_RosterUpdated;
            LibClient.StatusChanged += LibClient_StatusChanged;

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
        #endregion

    }
}