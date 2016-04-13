using UFIP.EngChat.Common.Models;

namespace UFIP.EngChat.Common.Sources
{
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

        public Roles Role { get; set; }
        public User ConnectedUser { get; set; }

        public bool _disposed;
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
