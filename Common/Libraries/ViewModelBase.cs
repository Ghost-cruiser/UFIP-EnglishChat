using System;
using System.ComponentModel;

namespace UFIP.EngChat.Common.Libraries
{
    public abstract class ViewModelBase : IDisposable, INotifyPropertyChanged
    {
        #region PROP
        public string DisplayName { get; set; }
        protected bool ThrowOnInvalidPropertyName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region METHODS
        public virtual void Dispose()
        {
            PropertyChanged = null;
        }
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region EVENTS
        #endregion

    }
}
