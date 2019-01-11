using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exhibition.ViewModel.infrostructure
{
    public class Progress_Bar : INotifyPropertyChanged
    {
        string status;
        int progress;
        bool visible;

        public string Status { get { return status; } set { status = value; OnPropertyChanged(nameof(Status)); }}
        public int Progress { get { return progress; } set { progress = value; OnPropertyChanged(nameof(Progress)); } }
        public bool Visible { get { return visible; } set { visible = value; OnPropertyChanged(nameof(Visible)); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyName);
        }
    }
}
