using System.Collections.Generic;

namespace UFIP.EngChat.Common.Models
{
    public class Conversation
    {
        public UserViewModel CurrentContact { get; set; }
        public List<S22.Xmpp.Im.Message> Messages { get; set; } = new List<S22.Xmpp.Im.Message>();

        public Conversation()
        {
            
        }
        public Conversation(UserViewModel contact)
        {
            CurrentContact = contact;
        }
    }
}
