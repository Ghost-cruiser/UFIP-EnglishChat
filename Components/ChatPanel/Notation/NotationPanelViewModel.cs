using System;
using System.Linq;
using S22.Xmpp.Im;
using UFIP.EngChat.Common.Libraries;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace UFIP.EngChat.Components.ChatPanel.Notation
{
    /// <summary>
    /// A view model of the panel of notation. Allow a teacher to record and note a conversation.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class NotationPanelViewModel : ViewModelBase
    {
        #region PROP        
        /// <summary>
        /// Gets or sets the note for the conversation.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public string Note { get; set; }

        private string _color;
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color representing the state of the last attempt to record =
        /// if <c>blue</c> : none, 
        /// if <c>green</c> : success, 
        /// if <c>red</c>: failure.
        /// </value>
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        /// <summary>
        /// Exposes the record command.
        /// </summary>
        /// <value>
        /// Command : RecordConversation() - CanRecord().
        /// </value>
        public ICommand Record { get; private set; }
        #endregion

        #region CTOR        
        /// <summary>
        /// Initializes a new instance of the <see cref="NotationPanelViewModel"/> class.
        /// </summary>
        public NotationPanelViewModel()
        {
            Record = new RelayCommand(
                param => RecordConversation(),
                param => CanRecord());
        }
        #endregion

        #region METHODS        
        /// <summary>
        /// Records the conversation.
        /// </summary>
        private void RecordConversation()
        {
            var context = Common.Sources.ConversationsSource.Center.SelectedConversation;
            ObservableCollection<Message> messages = context.Messages;
            var contact = context.CurrentContact;

            try
            {
                NotationService.WriteConversation(messages.ToList(), Note, contact.Jid, contact.Name);
                Color = "Green";
                MessageBox.Show("Sauvegardé !");
            }
            catch (Exception ex)
            {
                Color = "Red";
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Determines whether the conversation can be recorded.
        /// </summary>
        /// <returns></returns>
        private bool CanRecord()
        {
            return true;
        }
        #endregion

        #region VIEW-MODEL-BASE
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public override void Dispose()
        {
            Record = null;
            Color = null;
            Note = null;
        }
        #endregion
    }
}
