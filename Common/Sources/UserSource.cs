using UFIP.EngChat.Common.Models;

namespace UFIP.EngChat.Common.Sources
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="UFIP.EngChat.Common.Libraries.ViewModelBase" />
    public class UserSource : Libraries.ViewModelBase
    {
        #region PROP
        private static UserSource _center;
        public static UserSource Center
        {
            get
            {
                if (_center == null)
                    _center = new UserSource();
                return _center;
            }
            set
            {
                _center = value;
            }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role of the user.
        /// </value>
        public Roles Role { get; set; }

        /// <summary>
        /// Gets or sets the connected user.
        /// </summary>
        /// <value>
        /// The connected user.
        /// </value>
        public UserViewModel ConnectedUser { get; set; }

        private bool _disposed;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserSource"/> is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool Disposed
        {
            get
            {
                return _disposed;
            }
            set
            {
                _disposed = value;
                OnPropertyChanged("Disposed");
            }
        }
        #endregion

        #region CTOR
        private UserSource()
        {

        }
        #endregion
        
        public override void Dispose()
        {
            Center = null;
            Disposed = true;
            base.Dispose();
        }
    }

    public enum Roles
    {
        Administrateur = 0,
        Professeur = 1,
        Eleve = 2
    }
}
