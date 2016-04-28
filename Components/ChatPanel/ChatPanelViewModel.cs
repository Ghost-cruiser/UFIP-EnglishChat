using System.Windows.Input;
using UFIP.EngChat.Common.Sources;
using UFIP.EngChat.Common.Libraries;
using UFIP.EngChat.Common.Models;
using System;
using System.Windows.Data;

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
        public UserViewModel ConnectedUser { get; private set; }
            = Common.Sources.UserSource.Center.ConnectedUser;

        /// <summary>
        /// Exposes the source of conversations. Initializes through <seealso cref="Common.Sources.ConversationsSource"></seealso>
        /// </summary>
        /// <value>
        /// The service conversations.
        /// </value>
        public ConversationsSource ServiceConversations { get; private set; }
             = ConversationsSource.Center;

        private UserViewModel _selectedContact;
        /// <summary>
        /// Gets or sets the selected contact.
        /// </summary>
        /// <value>
        /// The selected contact.
        /// </value>
        public UserViewModel SelectedContact
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
        #endregion

        #region CONSTRUCTORS
        // TODO : implement retrieve true user avatar        
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatPanelViewModel"/> class.
        /// </summary>
        public ChatPanelViewModel()
        {
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Updates the selected conversation.
        /// </summary>
        private void UpdateSelectedConversation()
        {
            ServiceConversations.SelectConversation(SelectedContact);
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
                ConnectedUser = null;

                disposedValue = true;
            }

        }
        #endregion
    }
}
