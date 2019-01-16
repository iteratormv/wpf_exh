using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exhibition.Model
{
    public class Visitor : INotifyPropertyChanged
    {
        int id;
        string collumn1;
        string collumn2;
        string collumn3;
        string collumn4;
        string collumn5;
        string collumn6;
        string collumn7;
        string collumn8;
        string collumn9;
        string collumn10;
        string collumn11;
        string collumn12;
        string collumn13;
        string collumn14;
        string collumn15;

        public int Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Collumn1 { get { return collumn1; } set { collumn1 = value; OnPropertyChanged(nameof(Collumn1)); } }
        public string Collumn2 { get { return collumn2; } set { collumn2 = value; OnPropertyChanged(nameof(Collumn2)); } }
        public string Collumn3 { get { return collumn3; } set { collumn3 = value; OnPropertyChanged(nameof(Collumn3)); } }
        public string Collumn4 { get { return collumn4; } set { collumn4 = value; OnPropertyChanged(nameof(Collumn4)); } }
        public string Collumn5 { get { return collumn5; } set { collumn5 = value; OnPropertyChanged(nameof(Collumn5)); } }
        public string Collumn6 { get { return collumn6; } set { collumn6 = value; OnPropertyChanged(nameof(Collumn6)); } }
        public string Collumn7 { get { return collumn7; } set { collumn7 = value; OnPropertyChanged(nameof(Collumn7)); } }
        public string Collumn8 { get { return collumn8; } set { collumn8 = value; OnPropertyChanged(nameof(Collumn8)); } }
        public string Collumn9 { get { return collumn9; } set { collumn9 = value; OnPropertyChanged(nameof(Collumn9)); } }
        public string Collumn10 { get { return collumn10; } set { collumn10 = value; OnPropertyChanged(nameof(Collumn10)); } }
        public string Collumn11 { get { return collumn11; } set { collumn11 = value; OnPropertyChanged(nameof(Collumn11)); } }
        public string Collumn12 { get { return collumn12; } set { collumn12 = value; OnPropertyChanged(nameof(Collumn12)); } }
        public string Collumn13 { get { return collumn13; } set { collumn13 = value; OnPropertyChanged(nameof(Collumn13)); } }
        public string Collumn14 { get { return collumn14; } set { collumn14 = value; OnPropertyChanged(nameof(Collumn14)); } }
        public string Collumn15 { get { return collumn15; } set { collumn15 = value; OnPropertyChanged(nameof(Collumn15)); } }

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
