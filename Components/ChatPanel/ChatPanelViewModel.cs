using System.Windows.Input;
using UFIP.EngChat.Common.Sources;
using UFIP.EngChat.Common.Libraries;
using UFIP.EngChat.Common.Models;
using System;

namespace UFIP.EngChat.Components.ChatPanel
{
    /// <summary>
    /// A view model of the Chat. MainView of the components.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class ChatPanelViewModel : ViewModelBase
    {
        #region PROPERTIES        
        /// <summary>
        /// Gets the connected user. Initializes through <seealso cref="Common.Sources.UserSource"/>
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public User ConnectedUser { get; private set; }
            = Common.Sources.UserSource.Center.ConnectedUser;

        /// <summary>
        /// Exposes the source of conversations. Initializes through <seealso cref="Common.Sources.ConversationsSource"></seealso>
        /// </summary>
        /// <value>
        /// The service conversations.
        /// </value>
        public ConversationsSource ServiceConversations { get; private set; }
             = ConversationsSource.Center;

        private User _selectedContact;
        /// <summary>
        /// Gets or sets the selected contact.
        /// </summary>
        /// <value>
        /// The selected contact.
        /// </value>
        public User SelectedContact
        {
      
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;

                UpdateSelectedConversation();
            }
        }

        private ViewModelBase _viewSource;
        /// <summary>
        /// Gets the view source. Implements OnPropertyChanged.
        /// </summary>
        /// <value>
        /// The view-model to display.
        /// </value>
        public ViewModelBase ViewSource
        {
            get
            {
                return _viewSource;
            }

            private set
            {
                _viewSource = value;
                OnPropertyChanged("ViewSource");
            }
        }
        #endregion

        #region COMMANDS
        private ICommand _command;
        /// <summary>
        /// Gets the ViewContact.
        /// </summary>
        /// <value>
        /// Command : SwitchViewSource() - if IsContactListSelected()
        /// </value>
        public ICommand ViewContact
        {
            get
            {
                if (_command == null)
                {
                    _command = new RelayCommand(
                        param => SwitchViewSource(),
                        param => IsContactListSelected()
                    );
                }
                return _command;
            }
            private set
            {
                _command = value;
            }
        }

        private bool IsContactListSelected()
        {
            throw new NotImplementedException();
        }

        private void SwitchViewSource()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the selected conversation.
        /// </summary>
        private void UpdateSelectedConversation()
        {
            ServiceConversations.SelectConversation(SelectedContact);
        }
        #endregion

        #region CONSTRUCTORS
        // TODO : implement retrieve true user avatar

        #endregion

        #region METHODS            
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            ConnectedUser = null;
        }
        #endregion
    }
}
