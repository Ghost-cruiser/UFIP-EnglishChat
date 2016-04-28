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
            _disposed = false;
        }
        #endregion
        


        #region VIEW-MODEL-BASE   

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                Disposed = true;
            }

        }
        #endregion
    }

    /// <summary>
    /// Differents possible roles for the user.
    /// </summary>
    public enum Roles
    {
        /// <summary>
        /// The administrateur
        /// </summary>
        Administrateur = 0,

        /// <summary>
        /// The professeur
        /// </summary>
        Professeur = 1,

        /// <summary>
        /// The eleve
        /// </summary>
        Eleve = 2
    }
}
