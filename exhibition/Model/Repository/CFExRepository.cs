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

        DisplaySetting selectedDisplaySetting;
        DSCollumnSetting selectedDSCollumnSetting;
        ObservableCollection<Visitor> visitorCollection;
        ObservableCollection<DisplaySetting> displaySettingCollection;
        ObservableCollection<DSCollumnSetting> dsColumnSettingCollection;

        public DisplaySetting SelectedDisplaySetting { get { return selectedDisplaySetting; }set {
                selectedDisplaySetting = value; OnPropertyChanged(nameof(SelectedDisplaySetting)); } }
        public DSCollumnSetting SelectedDSCollumnSetting { get { return selectedDSCollumnSetting; } set {
                selectedDSCollumnSetting = value; OnPropertyChanged(nameof(SelectedDSCollumnSetting)); } }
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
                selectedDisplaySetting = context.DisplaySettings.Where(s => s.IsSelected == true).FirstOrDefault();
                var _dsCollumnSettings = context.DSCollumnSettings.Where(s=>s.DisplaySettingId == selectedDisplaySetting.Id);
                foreach (var c in _dsCollumnSettings) dsColumnSettingCollection.Add(c);
            }

            selectedDisplaySetting = context.DisplaySettings.Where(s => s.IsSelected == true).FirstOrDefault();
            selectedDSCollumnSetting = context.DSCollumnSettings.Where(s => s.IsSelected == true).FirstOrDefault();

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

        public bool addDSCollumnSetting()
        {

            bool result = false;
            try
            {
                var dsid = context.DisplaySettings.Where(s => s.IsSelected == true).Select(s => s.Id).FirstOrDefault();
                string _name = "NewCollumn1";
                string _alias = "NewAlias1";
                while (context.DSCollumnSettings.Where(s => s.Name == _name).Select(s => s).Count() > 0)
                {
                    string d_name = _name.Trim(new char[] { 'N', 'e', 'w', 'C', 'o', 'l', 'u', 'm', 'n' });
                    int d = int.Parse(d_name) + 1;
                    _name = "NewCollumn" + d.ToString();
                    _alias = "NewAlias" + d.ToString();
                }
                SelectedDisplaySetting = DisplaySettingCollection.Where(s => s.IsSelected == true).FirstOrDefault();
                var old_selected_collumn = context.DSCollumnSettings.Where(s => s.IsSelected == true).FirstOrDefault();
                old_selected_collumn.IsSelected = false;
                SelectedDSCollumnSetting = new DSCollumnSetting() { DisplaySettingId = dsid, Name = _name, Alias = _alias, Visible = true, IsSelected = true, Width = 100 };
                context.DSCollumnSettings.Add(SelectedDSCollumnSetting);
                context.SaveChanges();
                var _colStors = context.DSCollumnSettings.Where(s => s.DisplaySettingId == dsid).Select(s => s).ToList();
                var _colStor = context.DSCollumnSettings.Where(s => s.Name == _name).Select(s => s).FirstOrDefault();
                dsColumnSettingCollection.Clear();
                foreach (var c in _colStors) { dsColumnSettingCollection.Add(c); }

                //string _alias = "NewAlias1";
                //while (context.DSCollumnSettings.Where(s => s.Alias == _alias).Select(s => s).Count() > 0)
                //{
                //    string d_alias = _alias.Trim(new char[] { 'N', 'e', 'w', 'A', 'l', 'i', 'a', 's' });
                //    int d = int.Parse(d_alias) + 1;
                //    _alias = "NewAlias" + d.ToString();
                //}
                //selectedDSCollumnSetting = new DSCollumnSetting() { DisplaySettingId = _colStor.Id, Alias = _alias, Visible = true, Width = 100 };
                //context.ColSets.Add(selectedSetting);
                //context.SaveChanges();
                //var _colsets = context.DisplaySettings.ToList();
                //colSets.Clear();
                //foreach (var c in _colsets) { colSets.Add(c); }
                result = true;

            }

            catch (Exception e) { Console.WriteLine(e.ToString()); }

            return result;



            //try
            //{
            //    context.DSCollumnSettings.Add(dscs);
            //    context.SaveChanges();
            //    var _dsColumnSettingRepository = context.DSCollumnSettings; dsColumnSettingCollection.Clear();
            //    foreach (var c in _dsColumnSettingRepository) { dsColumnSettingCollection.Add(c); }
            //}
            //catch { return false; }
            //return true;
        }

        public bool delDSCollumnSetting(DSCollumnSetting dscs)
        {


            bool result = false;
            try
            {
         //       var d_se = context.DSCollumnSettings.Where(s => s.DisplaySettingId == dscs.Id).Select(s => s).FirstOrDefault();
         //       context.ColSets.Remove(d_se);
                context.DSCollumnSettings.Remove(dscs);
                dsColumnSettingCollection.Remove(dscs);
                context.SaveChanges();
                SelectedDisplaySetting = displaySettingCollection.Where(s => s.IsSelected == true).FirstOrDefault();
                var _dsColumnSettingCollection = context.DSCollumnSettings.Where(c => c.DisplaySettingId == SelectedDisplaySetting.Id);
                dsColumnSettingCollection.Clear();
                foreach(var c in _dsColumnSettingCollection) { dsColumnSettingCollection.Add(c); }
                SelectedDSCollumnSetting = DsColumnSettingCollection.FirstOrDefault();
                var new_selected_collumn_setting = context.DSCollumnSettings.Where(c => c.Id == SelectedDSCollumnSetting.Id).FirstOrDefault();
                new_selected_collumn_setting.IsSelected = true;
                context.SaveChanges();
  //             SelectedSetting = colSets.Where(s => s.ColStorId == SelectedCollumn.Id).Select(s => s).FirstOrDefault();
                result = true;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            return result;



            //try
            //{
            //    context.DSCollumnSettings.Remove(dscs);
            //    context.SaveChanges();
            //    dsColumnSettingCollection.Remove(dscs);
            //}
            //catch { return false; }
            //return true;
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

        public bool addDisplaySetting()
        {
            try
            {
                var cur_set = context.DisplaySettings.Where(s => s.IsSelected == true).Select(s => s).FirstOrDefault();
                string new_set_name = "NewSetting1";
                while (context.DisplaySettings.Where(s => s.Name == new_set_name).Count() > 0)
                {
                    string d_name = new_set_name.Trim(new char[] { 'N', 'e', 'w', 'S', 't', 'i', 'n', 'g' });
                    int d = int.Parse(d_name) + 1;
                    new_set_name = "NewSetting" + d.ToString();
                }
                if (cur_set != null) cur_set.IsSelected = false;

                var n_set = new DisplaySetting() { Name = new_set_name, IsSelected = true };
                context.DisplaySettings.Add(n_set);
                context.SaveChanges();
                SelectedDisplaySetting = context.DisplaySettings.Where(s => s.IsSelected == true).Select(s => s).FirstOrDefault();
                var _displaySettingsRepository = context.DisplaySettings; displaySettingCollection.Clear();
                foreach (var d in _displaySettingsRepository) displaySettingCollection.Add(d);
                addDSCollumnSetting();
            }
            catch { return false; }
            return true;
        }

        public bool delDisplaySetting(DisplaySetting ds)
        {
            bool result = false;
            try
            {
                DisplaySetting s_ds = context.DisplaySettings.Where(s => s.IsSelected == true).FirstOrDefault();
                context.DisplaySettings.Remove(ds);
                var del_cs = context.DSCollumnSettings.Where(s => s.DisplaySettingId == ds.Id);
                foreach(var c in del_cs) { context.DSCollumnSettings.Remove(c); }
                context.SaveChanges();
                if (ds.IsSelected == true)
                {
                    s_ds = context.DisplaySettings.FirstOrDefault();
                    s_ds.IsSelected = true;
                    context.SaveChanges();
                }
                DisplaySettingCollection.Remove(ds);
                var _colstors = context.DSCollumnSettings.Where(s => s.DisplaySettingId == s_ds.Id).Select(s => s).ToList();
                DsColumnSettingCollection.Clear();
                foreach (var c in _colstors) { DsColumnSettingCollection.Add(c); }
                SelectedDSCollumnSetting = DsColumnSettingCollection.FirstOrDefault();
                var new_selected_collumn_setting = context.DSCollumnSettings.Where(s => s.Id == SelectedDSCollumnSetting.Id).FirstOrDefault();
                new_selected_collumn_setting.IsSelected = true;
                context.SaveChanges();
                SelectedDisplaySetting = s_ds;
                result = true;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            return result;

            //try
            //{
            //    context.DisplaySettings.Remove(ds);
            //    context.SaveChanges();
            //    displaySettingCollection.Remove(ds);
            //}
            //catch { return false; }
            //return true;
        }

        public bool changeDisplaySettingDefault(DisplaySetting currentDefaulSetting)
        {
            bool result = false;

            try
            {
                var old_selected_column_setting = context.DSCollumnSettings.Where(c => c.IsSelected == true).FirstOrDefault();
                var cur_sete = context.DisplaySettings.Where(s => s.IsSelected == true).FirstOrDefault();
                cur_sete.IsSelected = false;
                context.SaveChanges();

                cur_sete = context.DisplaySettings.Where(s => s.Id == currentDefaulSetting.Id).FirstOrDefault();
                cur_sete.IsSelected = true;
                context.SaveChanges();
                
                old_selected_column_setting.IsSelected = false;
                context.SaveChanges();
                SelectedDisplaySetting = cur_sete;
                var _dsCollumnSettingCollection = context.DSCollumnSettings.Where(s => s.DisplaySettingId == SelectedDisplaySetting.Id);
                DsColumnSettingCollection.Clear();
                foreach (var c in _dsCollumnSettingCollection) DsColumnSettingCollection.Add(c);
                selectedDSCollumnSetting = DsColumnSettingCollection.FirstOrDefault();
                var new_selected_column_setting = context.DSCollumnSettings.Where(s => s.Id == SelectedDSCollumnSetting.Id).FirstOrDefault();
                new_selected_column_setting.IsSelected = true;
                context.SaveChanges();
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }

            return result;
        }

        public bool saveChanges(DisplaySetting  displaySetting, DSCollumnSetting dSCollumnSetting )
        {
            bool result = false;
            try
            {
                selectedDisplaySetting = displaySetting;
                selectedDSCollumnSetting = dSCollumnSetting;
                var old_selected_collumn_setting = context.DSCollumnSettings.Where(c => c.IsSelected == true).FirstOrDefault();
                old_selected_collumn_setting.IsSelected = false;
                context.SaveChanges();
                var new_selected_collumn_setting = context.DSCollumnSettings.Where(c => c.Id == selectedDSCollumnSetting.Id).FirstOrDefault();
                new_selected_collumn_setting.IsSelected = true;
                context.SaveChanges();
                var _dsColumnSettingCollection = context.DSCollumnSettings.Where(s => s.DisplaySettingId == selectedDisplaySetting.Id);
                dsColumnSettingCollection.Clear();
                foreach (var c in _dsColumnSettingCollection) dsColumnSettingCollection.Add(c);
                context.SaveChanges();
                result = true;
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            return result;
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
