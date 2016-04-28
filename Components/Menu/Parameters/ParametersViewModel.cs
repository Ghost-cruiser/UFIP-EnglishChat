using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFIP.EngChat.Common.Libraries;

namespace UFIP.EngChat.Components.Parameters
{
    public class ParametersViewModel : ViewModelBase
    {
        private string _hostname;
        public string Hostname
        {
            get
            {
                return _hostname;
            }
            set
            {
                _hostname = value;
                OnPropertyChanged("Hostname");
            }
        }

        private string _folderRecord;
        public string FolderRecord
        {
            get
            {
                return _folderRecord;
            }
            set
            {
                _folderRecord = value;
                OnPropertyChanged("FolderRecord");
            }
        }

        private string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                OnPropertyChanged("Port");
            }
        }


        private ICommand _save;
        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(

                        param => SaveParameters(),
                        param => CanSaveParameters()
                    );
                }
                return _save;
            }

            set
            {
                _save = value;
            }
        }

        private ICommand _return;
        public ICommand Return
        {
            get
            {
                if (_return == null)
                {
                    _return = new RelayCommand(

                        param => Close(),
                        param => true
                    );
                }
                return _return;
            }

            set
            {
                _return = value;
            }
        }

        private void SaveParameters()
        {
            Properties.Settings.Default.Host = Hostname;
            Properties.Settings.Default.Port = int.Parse(Port);
            Properties.Settings.Default.FolderRecord = FolderRecord;

            Close();
        }

        public delegate void DisposeHandler();
        public event DisposeHandler Disposed;

        protected virtual void OnDisposed()
        {
            if (Disposed != null)
                Disposed();
        }

        private void Close()
        {
            Dispose();
        }

        private bool CanSaveParameters()
        {
            int i;
            if (int.TryParse(Port, out i) && !string.IsNullOrWhiteSpace(FolderRecord) && !string.IsNullOrWhiteSpace(Hostname))
                return true;
            return false;

        }

        public ParametersViewModel()
        {
            Hostname = Properties.Settings.Default.Host;
            FolderRecord = Properties.Settings.Default.FolderRecord;
            Port = Properties.Settings.Default.Port.ToString();
        }

        #region VIEW-MODEL-BASE        
        private bool disposedValue = false; // Pour détecter les appels redondants

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                Hostname = null;
                FolderRecord = null;

                disposedValue = true;
                OnDisposed();
            }

        }
        #endregion
    }
}
