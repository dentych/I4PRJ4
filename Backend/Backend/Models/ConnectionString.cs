using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Backend.Annotations;

namespace Backend.Models
{
    public class ConnectionString : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _connection = "Ikke forbundet";


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
