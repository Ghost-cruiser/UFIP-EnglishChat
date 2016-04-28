using System.Collections.Generic;

namespace UFIP.EngChat.Common.Models
{
    /// <summary>
    /// Conversation between two users.
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// Gets or sets the current contact.
        /// </summary>
        /// <value>
        /// The current contact.
        /// </value>
        public UserViewModel CurrentContact { get; set; }
        /// <summary>
        /// Gets or sets the messages. Initializes automaticly.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public List<S22.Xmpp.Im.Message> Messages { get; set; } = new List<S22.Xmpp.Im.Message>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        public Conversation()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        /// <param name="contact">The current contact.</param>
        public Conversation(UserViewModel contact)
        {
            CurrentContact = contact;
        }
    }
}
