using S22.Xmpp.Im;
using S22.Xmpp;

namespace UFIP.EngChat.Common.Models
{
    public class User
    {
        public string Name { get; set; }
        public Jid Jid { get; set; }
        public Status CurrentStatus { get; set; }

        public object UserAvatar { get; set; }

        public User()
        {
            UserAvatar = @"/UFIP.EngChat;component/Resources/default_avatar.png";

        }

        public User(Jid jid, string username)
        {
            Jid = jid;
            Name = username; // Parse JID here for students
            CurrentStatus = new Status(Availability.Online);
            UserAvatar = @"/UFIP.EngChat;component/Resources/default_avatar.png";
        }

        public static implicit operator User(RosterItem item)
        {
            User cont = new User();
            cont.Name = item.Name;
            cont.Jid = item.Jid;
            cont.CurrentStatus = new Status(Availability.Offline);

            return cont;
        }
        
    }
}