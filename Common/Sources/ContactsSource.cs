using S22.Xmpp;
using System.Linq;

namespace UFIP.EngChat.Common.Sources
{
    /// <summary>
    /// Exposes the list of contact as a singleton.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class ContactsSource : Libraries.ViewModelBase
    {
        #region PROPERTIES        
        /// <summary>
        /// Gets or sets all contacts.
        /// </summary>
        /// <value>
        /// All contacts.
        /// </value>
        public Components.Contacts.ContactList AllContacts { get; set; }

        private static ContactsSource _center;
        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        public static ContactsSource Center
        {
            get
            {
                if (_center == null)
                    _center = new ContactsSource();
                return _center;
            }
            private set
            {
                _center = value;
            }
        }

        private Models.User _selectedContact;
        /// <summary>
        /// Gets or sets the selected contact.
        /// </summary>
        /// <value>
        /// The selected contact.
        /// </value>
        public Models.User SelectedContact
        {
            get
            {
                if (_selectedContact == null)
                {
                    _selectedContact = AllContacts.FirstOrDefault();
                }
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }
        #endregion

        #region CONSTRUCTOR
        private ContactsSource()
        {
            // TODO change CTOR in contactList 
            AllContacts = new Components.Contacts.ContactList();
        }
        #endregion  

        #region METHODS        
        /// <summary>
        /// Gets a user or create it based on its JID.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <returns>A user in the Contact List.</returns>
        public Models.User GetUser(Jid jid)
        {
            foreach (var contact in AllContacts)
            {
                if (jid.Equals(contact.Jid))
                {
                    return contact;
                }
            }

            // Create a temporary contact if a non-roster user creates a conversation.
            // Allows administrator to talk to anyone.
            // Change ctor in user
            var tempContact = new Models.User(jid, "");

            AllContacts.Add(tempContact);

            return tempContact;

        }

        /// <summary>
        /// Updates the list.
        /// </summary>
        /// <param name="removed">if set to <c>true</c> the item will be removed.</param>
        /// <param name="item">The item to update.</param>
        public void UpdateList(bool removed, S22.Xmpp.Im.RosterItem item)
        {
            if (removed)
                AllContacts.Remove(item);
            else
                AllContacts.Add(item);
        }

        /// <summary>
        /// Updates the status contact.
        /// </summary>
        /// <param name="jid">The jid of the contact.</param>
        /// <param name="status">The new status.</param>
        public void UpdateContact(Jid jid, S22.Xmpp.Im.Status status)
        {
            foreach(var contact in AllContacts)
            {
                if (jid.Equals(contact.Jid))
                    contact.CurrentStatus = status;
            }
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            Center = null;
            AllContacts = null;
            SelectedContact = null;
        }
        #endregion
    }
}

