using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exhibition.Model
{
    public class DSCollumnSetting : INotifyPropertyChanged
    {
        int id;
        string name;
        string alias;
        bool visible;
        int width;
        bool isSelected;
        int displaySettingId;

        public int Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string Alias { get { return alias; } set { alias = value; OnPropertyChanged(nameof(Alias)); } }
        public bool Visible { get { return visible; } set { visible = value; OnPropertyChanged(nameof(Visible)); } }
        public int Width { get { return width; } set { width = value; OnPropertyChanged(nameof(Width)); } }
        public bool IsSelected { get { return isSelected; } set { isSelected = value; OnPropertyChanged(nameof(IsSelected)); } }

        public int DisplaySettingId { get { return displaySettingId; } set { displaySettingId = value; OnPropertyChanged(nameof(DisplaySettingId)); } }
        public virtual DisplaySetting DisplaySetting { get; set; }


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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
