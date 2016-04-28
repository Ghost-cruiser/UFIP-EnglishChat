using System;
using S22.Xmpp.Im;
using System.ComponentModel;
using UFIP.EngChat.Common.Libraries;
using System.Windows.Input;
using UFIP.EngChat.Common.Sources;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UFIP.EngChat.Common.Models;
using UFIP.EngChat.Common.Core;

namespace UFIP.EngChat.Components.ChatPanel.Conversations
{
    /// <summary>
    /// ViewModel for a conversation. Is used for the ConversationsSource.SelectedConversation only.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class ConversationViewModel : ViewModelBase
    {
        #region PROP        
        /// <summary>
        /// Gets or sets the current contact.
        /// </summary>
        /// <value>
        /// The current contact.
        /// </value>
        public UserViewModel CurrentContact { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public ObservableCollection<Message> Messages { get; set; }

        private string _messageWritten;
        /// <summary>
        /// Gets or sets the message being written by the user. Implements OnPropertyChanged.
        /// </summary>
        /// <value>
        /// The message written by the user.
        /// </value>
        public string MessageWritten {
            get { return _messageWritten; }
            set
            {
                if (value != _messageWritten)
                {
                    _messageWritten = value;
                    OnPropertyChanged("MessageWritten");
                }
            } }

        private ICommand _send;
        /// <summary>
        /// Exposes the send command.
        /// </summary>
        /// <value>
        /// Command : SendMessage() - if CanSend()
        /// </value>
        public ICommand Send
        {
            get
            {
                if (_send == null)
                {
                    _send = new RelayCommand(
                        param => SendMessage(),
                        param => CanSend()
                    );
                }
                return _send;
            }
            private set
            {
                _send = value;
            }
        }

        private Notation.NotationPanelViewModel _teacherTools;
        /// <summary>
        /// Gets the teacher tools. Instanciated if user is a teacher.
        /// </summary>
        /// <value>
        /// View-Model of the NotationPanel
        /// </value>
        public Notation.NotationPanelViewModel TeacherTools
        {
            get
            {
                return _teacherTools;
            }
            private set
            {
                _teacherTools = value;
            }
        }
        #endregion


        #region CTOR        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationViewModel"/> class.
        /// </summary>
        /// <param name="contact">The contact the conversation is currently with.</param>
        public ConversationViewModel(UserViewModel contact)
        {
            Messages = new ObservableCollection<Message>();
            CurrentContact = contact;
            MessageWritten = "";

            if (UserSource.Center.Role == Roles.Professeur)
            {
                // TODO : change CTOR -> Should be a singleton
                TeacherTools = new Notation.NotationPanelViewModel();
            }
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Adds the message received. Called by external thread.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void AddMessage(Message message)
        {
            // Call to dispatcher to trigger OnCollectionChanged
            UICollection.AddOnUI(Messages, message);
        }

        /// <summary>
        /// Try to sends the message written. MessageBox on error
        /// </summary>
        private void SendMessage()
        {
            Message message = new Message(CurrentContact.Jid, MessageWritten);
            MessageWritten = string.Empty;

            try
            {
                XmppService.Client.SendMessage(message);
                Messages.Add(message);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Determines whether the message written is empty or not.
        /// </summary>
        /// <returns><c>true</c> if message is not null, empty, or white spaces; otherwise, <c>false</c>.</returns>
        private bool CanSend()
        {
            return !string.IsNullOrWhiteSpace(MessageWritten);
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
                    if (CurrentContact != null)
                        CurrentContact.Dispose();
                    if (TeacherTools != null)
                        TeacherTools.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                Messages = null;
                MessageWritten = null;
                Send = null;

                disposedValue = true;
            }

        }
        #endregion


        #region OPERATOR        
        /// <summary>
        /// Performs an implicit conversion from <see cref="Common.Models.Conversation"/> to <see cref="ConversationViewModel"/>.
        /// </summary>
        /// <param name="conversation">The conversation to be converted.</param>
        /// <returns>
        /// The ConversationViewModel converted from the conversation.
        /// </returns>
        public static implicit operator ConversationViewModel(Common.Models.Conversation conversation)
        {
            ConversationViewModel convViewModel = new ConversationViewModel(conversation.CurrentContact);
            convViewModel.Messages = new ObservableCollection<Message>(conversation.Messages);
            return convViewModel;
        }
        #endregion

    }
}