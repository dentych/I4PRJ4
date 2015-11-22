using System.ComponentModel;
using System.Runtime.CompilerServices;
using Backend.Annotations;

namespace Backend.Models.Datamodels
{
    public class ConnectionString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _connection; // = "Ikke forbundet";

        /// <summary>
        /// Connection string
        /// </summary>
        public string Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                OnPropertyChanged("Connection");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
