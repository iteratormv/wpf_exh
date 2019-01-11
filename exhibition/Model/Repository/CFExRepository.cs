using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using exhibition.ViewModel.infrostructure;

namespace exhibition.Model.Repository
{
    public class CFExRepository : INotifyPropertyChanged
    {
        ExContext context;
        Progress_Bar progress;

        ObservableCollection<Visitor> visitorCollection;
        ObservableCollection<DisplaySetting> displaySettingCollection;
        ObservableCollection<DSCollumnSetting> dsColumnSettingCollection;

        public ObservableCollection<Visitor> VisitorCollection
        {
            get { return visitorCollection; }
            set { visitorCollection = value; OnPropertyChanged(nameof(VisitorCollection)); }
        }
        public ObservableCollection<DisplaySetting> DisplaySettingCollection
        {
            get { return displaySettingCollection; }
            set { displaySettingCollection = value; OnPropertyChanged(nameof(DisplaySettingCollection)); }
        }
        public ObservableCollection<DSCollumnSetting> DsColumnSettingCollection
        {
            get { return dsColumnSettingCollection; }
            set { dsColumnSettingCollection = value; OnPropertyChanged(nameof(DsColumnSettingCollection)); }
        }

        public CFExRepository()
        {
            progress = new Progress_Bar();
            displaySettingCollection = new ObservableCollection<DisplaySetting>();
            dsColumnSettingCollection = new ObservableCollection<DSCollumnSetting>();
            visitorCollection = new ObservableCollection<Visitor>();
            context = new ExContext();
            var d = context.DisplaySettings.ToList();
            if (d.Count() == 0)
            {
                //create default setting
                DisplaySetting defaultSetting = new DisplaySetting { Name = "default", IsSelected = true };
                context.DisplaySettings.Add(defaultSetting); context.SaveChanges();
                //find setting in database wich contain parameter Id
                var defaulSetting = context.DisplaySettings.Where(s => s.Name == "default").FirstOrDefault();
                //add setting to repository
                displaySettingCollection.Add(defaulSetting);
                //find value Id for DisplaySettingId in DSCollumnSetting
                var defaulSettingId = defaulSetting.Id;
                //create collumn for default setting
                //add collumn in database
                context.DSCollumnSettings.Add(new DSCollumnSetting { Name = "Id", Alias = "№", Width = 100, Visible = true, IsSelected = true, DisplaySettingId = defaulSettingId });
                context.DSCollumnSettings.Add(new DSCollumnSetting { Name = "FirstName", Alias = "Имя", Width = 100, Visible = true, IsSelected = false, DisplaySettingId = defaulSettingId });
                context.DSCollumnSettings.Add(new DSCollumnSetting { Name = "LastName", Alias = "Фамилия", Width = 100, Visible = true, IsSelected = false, DisplaySettingId = defaulSettingId });
                context.DSCollumnSettings.Add(new DSCollumnSetting { Name = "Сompany", Alias = "Компания", Width = 100, Visible = true, IsSelected = false, DisplaySettingId = defaulSettingId });
                context.DSCollumnSettings.Add(new DSCollumnSetting { Name = "Jobtitle", Alias = "Должность", Width = 100, Visible = true, IsSelected = false, DisplaySettingId = defaulSettingId });
                context.SaveChanges();
                //extract collumns from database which contain parameter Id and add its into the perository
                var _dsCollumnSettings = context.DSCollumnSettings;
                foreach (var c in _dsCollumnSettings) { dsColumnSettingCollection.Add(c); }
            }
            else
            {
                //find settings in database
                var _displaySettingCollection = context.DisplaySettings;
                //add setting to repository
                foreach (var c in _displaySettingCollection) displaySettingCollection.Add(c);
                //extract collumns from database which contain parameter Id and add its into the perository
                var _dsCollumnSettings = context.DSCollumnSettings;
                foreach (var c in _dsCollumnSettings) dsColumnSettingCollection.Add(c);
            }

        }

        public void addToRepositoryFromFile(string fileName)
        {
            ExelData exelData = new ExelData(fileName);
            exelData.setDataToCollection(context.Visitors);
            context.SaveChanges();
            foreach (var v in context.Visitors) visitorCollection.Add(v);
        }

        public void initRepositoryFromFile(string fileName)
        {
            
            ExelData exelData = new ExelData(fileName, progressChanged);
            progress.Status = "Delete old visitors";
            progress.Progress = 0;
            using (var ctx = new ExContext())
            {
                int count = 1;
                var collection = ctx.Visitors.ToList();
                var size = collection.Count()+1;
                foreach (var u in collection)
                {
                    progress.Progress = (int)(count * 100 / size);
                    progressChanged(progress);
                    ctx.Visitors.Remove(u);
                    count++;
                }
                ctx.SaveChanges();
            }
            exelData.setDataToCollection(context.Visitors, progressChanged);
            context.SaveChanges();
            progress.Status = "Add new data to collection";
            progress.Progress = 0;
            int c = 1;
            var col = context.Visitors;
            var s = col.Count()+1;
            foreach (var v in col)
            {
                progress.Progress = (int)(c * 100 / s);
                progressChanged(progress);
                visitorCollection.Add(v);
                c++;
            }
        }

        public bool addDSCollumnSetting(DSCollumnSetting dscs)
        {
            try
            {
                context.DSCollumnSettings.Add(dscs);
                context.SaveChanges();
                var _dsColumnSettingRepository = context.DSCollumnSettings; dsColumnSettingCollection.Clear();
                foreach (var c in _dsColumnSettingRepository) { dsColumnSettingCollection.Add(c); }
            }
            catch { return false; }
            return true;
        }

        public bool delDSCollumnSetting(DSCollumnSetting dscs)
        {
            try
            {
                context.DSCollumnSettings.Remove(dscs);
                context.SaveChanges();
                dsColumnSettingCollection.Remove(dscs);
            }
            catch { return false; }
            return true;
        }

        public bool addDisplaySetting(DisplaySetting ds)
        {
            try
            {
                context.DisplaySettings.Add(ds);
                context.SaveChanges();
                var _displaySettingsRepository = context.DisplaySettings; displaySettingCollection.Clear();
                foreach (var d in _displaySettingsRepository) displaySettingCollection.Add(d);
            }
            catch { return false; }
            return true;
        }

        public bool delDisplaySetting(DisplaySetting ds)
        {
            try
            {
                context.DisplaySettings.Remove(ds);
                context.SaveChanges();
                displaySettingCollection.Remove(ds);
            }
            catch { return false; }
            return true;
        }

        public bool addVisitor(Visitor visitor)
        {
            try
            {
                context.Visitors.Add(visitor);
                context.SaveChanges();
                var _visitorRepository = context.Visitors; visitorCollection.Clear();
                foreach (var v in _visitorRepository) visitorCollection.Add(v);
            }
            catch { return false; }
            return true;
        }

        public bool delVisitor(Visitor visitor)
        {
            try
            {
                context.Visitors.Remove(visitor);
                context.SaveChanges();
                visitorCollection.Remove(visitor);
            }
            catch { return false; }
            return true;
        }

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

        public event Action<Progress_Bar> progressChanged;

    }
}
