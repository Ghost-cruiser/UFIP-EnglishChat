using System;
using S22.Xmpp.Im;
using S22.Xmpp;
using UFIP.EngChat.Common.Models;

namespace UFIP.EngChat.Components.ChatPanel.Contacts
{
    /// <summary>
    /// View-Model for a user.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class UserViewModel : Common.Libraries.ViewModelBase
    {
        #region PROP        
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the jid of the user.
        /// </summary>
        /// <value>
        /// The jid.
        /// </value>
        public Jid Jid { get; set; }

        /// <summary>
        /// Gets or sets the current status of the user. 
        /// </summary>
        /// <value>
        /// The current status.
        /// </value>
        public Status CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets the user avatar. Default only for the moment.
        /// </summary>
        /// <value>
        /// The user avatar.
        /// </value>
        public object UserAvatar { get; set; }

        private string _usercolor;
        /// <summary>
        /// Gets or sets the color expressing the status.
        /// </summary>
        /// <value>
        /// The color of the user's status.
        /// </value>
        public string UserColor
        {
            // TODO : implement full dictionnary
            get
            {
                if (_usercolor == null)
                    _usercolor = getUserColor(CurrentStatus.Availability);
                return _usercolor;
            }
            set
            {
                _usercolor = value;
                OnPropertyChanged(_usercolor);
            }
        }

        /// <summary>
        /// Return a color depending on the availability. Online / Offline only implemented for the moment.
        /// </summary>
        /// <param name="availability">The availability.</param>
        /// <returns>The color matching the status</returns>
        private string getUserColor(Availability availability)
        {
           switch (availability)
            {
                case Availability.Online:
                    return "Green";
                case Availability.Offline:
                    return "Red";
                default:
                    return "Grey";
            }
        }

        /// <summary>
        /// The default URI for the user avatar.
        /// </summary>
        private string defaultUri = @"/UFIP.EngChat;component/Resources/default_avatar.png";

        /// <summary>
        /// Gets the state of the user : its availibility and its Status message.
        /// </summary>
        /// <value>
        /// A string representing the user's state.
        /// </value>
        public string State
        {
            get
            {
                // TODO : implement full dictionnary
                var state = CurrentStatus.Availability == Availability.Online ? "Connecté" : "Indisponible";
                return state + " " + CurrentStatus.Message;
            }
        }
        #endregion

        #region CTOR        
        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        public UserViewModel()
        {
            UserAvatar = defaultUri;
            CurrentStatus = new Status(Availability.Online);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserViewModel(User user)
        {
            Jid = user.Jid;
            Name = user.Name;

            if (user.UserAvatar != null)
                UserAvatar = user.UserAvatar;
            else
                UserAvatar = defaultUri;

            if (user.CurrentStatus != null)
                CurrentStatus = user.CurrentStatus;
            else
                CurrentStatus = new Status(Availability.Offline);
        }
        #endregion        
        /// <summary>
        /// Performs an implicit conversion from <see cref="RosterItem"/> to <see cref="UserViewModel"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// A View Model of a RosterItem aka a contact.
        /// </returns>
        public static implicit operator UserViewModel(RosterItem item)
        {
            UserViewModel cont = new UserViewModel();
            cont.Name = item.Name;
            cont.Jid = item.Jid;
            cont.CurrentStatus = new Status(Availability.Offline);

            return cont;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="UserViewModel"/> to <see cref="User"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// A View Model of a RosterItem aka a contact.
        /// </returns>
        public static implicit operator User(UserViewModel item)
        {
            User cont = new User();
            cont.Name = item.Name;
            cont.Jid = item.Jid;
            cont.CurrentStatus = item.CurrentStatus;

            return cont;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}