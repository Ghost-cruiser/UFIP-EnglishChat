using S22.Xmpp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using UFIP.EngChat.Common.Models;

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
        public ObservableCollection<UserViewModel> AllContacts { get; set; }

        /// <summary>
        /// Gets or sets the connected contacts.
        /// </summary>
        /// <value>
        /// The connected contacts.
        /// </value>
        public CollectionViewSource ConnectedContacts { get; set; }

        private static ContactsSource _center;
        /// <summary>
        /// Gets the service. Singleton implementation.
        /// </summary>
        /// <value>
        /// The ContactsSource.
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

        private Models.UserViewModel _selectedContact;
        /// <summary>
        /// Gets or sets the selected contact. Implements INotifyPropertyChanged.
        /// </summary>
        /// <value>
        /// The selected contact.
        /// </value>
        public Models.UserViewModel SelectedContact
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
            AllContacts = new ObservableCollection<UserViewModel>();

            ConnectedContacts = new CollectionViewSource { Source = AllContacts, IsLiveFilteringRequested = true };
            ConnectedContacts.LiveFilteringProperties.Add("CurrentStatus");
            ConnectedContacts.Filter += ConnectedContacts_Filter;

        }

        private void ConnectedContacts_Filter(object sender, FilterEventArgs e)
        {
            var cont = e.Item as Models.UserViewModel;
            if (cont != null && cont.CurrentStatus.Availability != S22.Xmpp.Im.Availability.Offline)
                e.Accepted = true;
            else
                e.Accepted = false;
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Gets a user or create it based on its JID.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <returns>A user in the Contact List.</returns>
        public Models.UserViewModel GetUser(Jid jid)
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
            var tempContact = new Models.UserViewModel(jid, "");

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
            if (AllContacts.Count != 0)
            {
                foreach (var contact in AllContacts)
                {
                    if (jid.GetBareJid() == contact.Jid.GetBareJid())
                    {
                        contact.CurrentStatus = new S22.Xmpp.Im.Status(status.Availability, status.Messages, status.Priority);

                        break;
                    }
                }
            }
            else
            {

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

