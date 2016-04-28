using S22.Xmpp.Im;
using S22.Xmpp;

namespace UFIP.EngChat.Common.Models
{
    /// <summary>
    /// View-Model for a user.
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class UserViewModel : Libraries.ViewModelBase
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


        private Status _currentStatus;
        /// <summary>
        /// Gets or sets the current status of the user. 
        /// </summary>
        /// <value>
        /// The current status.
        /// </value>
        public Status CurrentStatus
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus = value;
                UserColor = getUserColor(value.Availability);
                OnPropertyChanged("CurrentStatus");
                OnPropertyChanged("State");
            }
        }

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
                OnPropertyChanged("UserColor");
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        public UserViewModel()
        {
            UserAvatar = @"/UFIP.EngChat;component/Resources/default_avatar.png";

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="username">The username.</param>
        public UserViewModel(Jid jid, string username)
        {
            Jid = jid;
            Name = username; // Parse JID here for students
            CurrentStatus = new Status(Availability.Online);
            UserAvatar = @"/UFIP.EngChat;component/Resources/default_avatar.png";
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="RosterItem"/> to <see cref="UserViewModel"/>.
        /// </summary>
        /// <param name="item">The RosterItem.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator UserViewModel(RosterItem item)
        {
            UserViewModel cont = new UserViewModel();
            cont.Name = item.Name;
            cont.Jid = item.Jid;
            cont.CurrentStatus = new Status(Availability.Offline);

            return cont;
        }
        
    }
}