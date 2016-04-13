using S22.Xmpp;
using S22.Xmpp.Im;
using System.Collections.ObjectModel;
using System.Linq;
using UFIP.EngChat.Common.Models;

namespace UFIP.EngChat.Common.Sources
{
    /// <summary>
    /// Exposes as a singleton all conversations. 
    /// Allow components to be bound to it by implementing the ViewModelBase, and therefore, the INotifyPropertyChanged.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class ConversationsSource : Libraries.ViewModelBase
    {
        #region PROPERTIES        
        /// <summary>
        /// Gets or sets all conversations.
        /// </summary>
        /// <value>
        /// Observable Collection of all the conversations.
        /// </value>
        public ObservableCollection<Conversation> AllConversations { get; set; }

        private static ConversationsSource _center;
        /// <summary>
        /// Gets the service. Instanciate itself
        /// </summary>
        /// <value>
        /// The ConversationsSource.
        /// </value>
        public static ConversationsSource Center
        {
            get
            {
                if (_center == null)
                    _center = new ConversationsSource();
                return _center;
            }
            private set
            {
                _center = value;
            }
        }

        private Components.ChatPanel.Conversations.ConversationViewModel _selectedConversation;
        /// <summary>
        /// Gets or sets the selected conversation.
        /// </summary>
        /// <value>
        /// View model of the selected conversation.
        /// </value>
        public Components.ChatPanel.Conversations.ConversationViewModel SelectedConversation
        {
            get
            {
                return _selectedConversation;
            }
            private set
            {
                if (_selectedConversation != null && AllConversations != null)
                {

                    var conv = FindByJid(_selectedConversation.CurrentContact.Jid);
                    if (conv != null)
                        conv.Messages = _selectedConversation.Messages.ToList();
                }

                _selectedConversation = value;

                OnPropertyChanged("SelectedConversation");
            }
        }
        #endregion

        #region CONSTRUCTOR
        private ConversationsSource()
        {
            AllConversations = new ObservableCollection<Conversation>();
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Handles the reception of a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="jid">The jid of the sender.</param>
        internal void ReceiveMessage(Message message, Jid jid)
        {
            if (jid.Node.Equals(SelectedConversation.CurrentContact.Jid.Node))
            {
                SelectedConversation.AddMessage(message);
            }
            else
            {
                var conversation = FindByJid(jid);

                if (conversation != null)
                {
                    conversation.Messages.Add(message);
                }
                else
                    CreateConversation(ContactsSource.Center.GetUser(jid));
            }
        }

        /// <summary>
        /// Creates a conversation for a contact.
        /// </summary>
        /// <param name="contact">The contact.</param>
        public void CreateConversation(User contact)
        {
            AllConversations.Add(new Conversation(contact));
        }

        /// <summary>
        /// Selects the conversation using the list of contact if it was not instanciated yet.
        /// Modification of the selected conversation must use this fonction
        /// </summary>
        /// <param name="contact">The contact to chat with.</param>
        public void SelectConversation(User contact)
        {
            var conversation = FindByJid(contact.Jid);

            if (conversation != null)
            {
                SelectedConversation = conversation;
            }

            else
            {
                conversation = new Conversation(contact);
                AllConversations.Add(conversation);
                SelectedConversation = conversation;
            }
        }

        private Conversation FindByJid(Jid jid)
        {
            return AllConversations.Where(
                        t => t.CurrentContact.Jid.Node.Equals(jid.Node)).FirstOrDefault();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Center = null;
            AllConversations = null;
            SelectedConversation = null;
        }
        #endregion
    }
}

