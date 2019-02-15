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
        string column1;
        string column2;
        string column3;
        string column4;
        string column5;
        string column6;
        string column7;
        string column8;
        string column9;
        string column10;
        string column11;
        string column12;
        string column13;
        string column14;
        string column15;

        public int Id { get { return id; } set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string Column1 { get { return column1; } set { column1 = value; OnPropertyChanged(nameof(Column1)); } }
        public string Column2 { get { return column2; } set { column2 = value; OnPropertyChanged(nameof(Column2)); } }
        public string Column3 { get { return column3; } set { column3 = value; OnPropertyChanged(nameof(Column3)); } }
        public string Column4 { get { return column4; } set { column4 = value; OnPropertyChanged(nameof(Column4)); } }
        public string Column5 { get { return column5; } set { column5 = value; OnPropertyChanged(nameof(Column5)); } }
        public string Column6 { get { return column6; } set { column6 = value; OnPropertyChanged(nameof(Column6)); } }
        public string Column7 { get { return column7; } set { column7 = value; OnPropertyChanged(nameof(Column7)); } }
        public string Column8 { get { return column8; } set { column8 = value; OnPropertyChanged(nameof(Column8)); } }
        public string Column9 { get { return column9; } set { column9 = value; OnPropertyChanged(nameof(Column9)); } }
        public string Column10 { get { return column10; } set { column10 = value; OnPropertyChanged(nameof(Column10)); } }
        public string Column11 { get { return column11; } set { column11 = value; OnPropertyChanged(nameof(Column11)); } }
        public string Column12 { get { return column12; } set { column12 = value; OnPropertyChanged(nameof(Column12)); } }
        public string Column13 { get { return column13; } set { column13 = value; OnPropertyChanged(nameof(Column13)); } }
        public string Column14 { get { return column14; } set { column14 = value; OnPropertyChanged(nameof(Column14)); } }
        public string Column15 { get { return column15; } set { column15 = value; OnPropertyChanged(nameof(Column15)); } }

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
