using System.Collections.Generic;

namespace UFIP.EngChat.Common.Models
{
    public class Conversation
    {
        public User CurrentContact { get; set; }
        public List<S22.Xmpp.Im.Message> Messages { get; set; } = new List<S22.Xmpp.Im.Message>();

        public Conversation()
        {
            
        }
        public Conversation(User contact)
        {
            CurrentContact = contact;
        }
    }
}
