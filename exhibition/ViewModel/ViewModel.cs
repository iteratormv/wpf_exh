using exhibition.Model;
using exhibition.Model.Repository;
using exhibition.ViewModel.infrostructure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace exhibition.ViewModel
{
    class ViewModel : INotifyPropertyChanged
    {
        const int count_headers = 16;


        CFExRepository cFExRepository;
        ObservableCollection<Visitor> visitorCollection;
        ObservableCollection<DisplaySetting> displaySettingCollection;
        ObservableCollection<DSCollumnSetting> dsColumnSettingCollection;

        bool[] columnChecked;
        string[] alias;
        int[] width;

        DisplaySetting selectedDisplaySetting;
        DSCollumnSetting selectedCollumnSetting;
        Progress_Bar progressBar;

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

        public bool[] ColumnChecked { get { return columnChecked; } set { columnChecked = value; OnPropertyChanged(nameof(ColumnChecked)); } }
        public string[] Alias { get { return alias; } set { alias = value; OnPropertyChanged(nameof(Alias)); } }
        public int[] Width { get { return width; } set { width = value; OnPropertyChanged(nameof(Width)); } }

        public DisplaySetting SelectedDisplaySetting
        {
            get { return selectedDisplaySetting; }
            set { selectedDisplaySetting = value; OnPropertyChanged(nameof(SelectedDisplaySetting)); }
        }
        public DSCollumnSetting SelectedCollumnSetting
        {
            get { return selectedCollumnSetting; }
            set { selectedCollumnSetting = value; OnPropertyChanged(nameof(SelectedCollumnSetting)); }
        }
        public Progress_Bar _ProgressBar { get { return progressBar; } set { progressBar = value; OnPropertyChanged(nameof(_ProgressBar)); } }

        RelayCommand addCollumn;
        public RelayCommand AddCollumn { get { return addCollumn; } }

        RelayCommand removeCollumn;
        public RelayCommand RemoveCollumn { get { return removeCollumn; } }

        RelayCommand saveColumn;
        public RelayCommand SeveCollumn { get { return saveColumn; } }

        RelayCommand addSetting;
        public RelayCommand AddSetting { get { return addSetting; } }

        RelayCommand delSetting;
        public RelayCommand DelSetting { get { return delSetting; } }

        RelayCommand saveChanges;
        public RelayCommand SaveChanges { get { return saveChanges; } }

        RelayCommand addDataFromFileToDatabase;
        public RelayCommand AddDataFromFileToDatabase { get { return addDataFromFileToDatabase; } }

        RelayCommand changeDisplaySettingDefault;
        public RelayCommand ChangeDisplaySettingDefault { get { return changeDisplaySettingDefault; } }

        public ViewModel()
        {
            

            columnChecked = new bool[count_headers];
            alias = new string[count_headers];
            width = new int[count_headers];


            cFExRepository = new CFExRepository();
            cFExRepository.progressChanged += ProgressChanged;
            visitorCollection = cFExRepository.VisitorCollection;
            displaySettingCollection = cFExRepository.DisplaySettingCollection;
            selectedDisplaySetting = displaySettingCollection.Where(s => s.IsSelected == true).FirstOrDefault();
            var _dsColumnSettingCollection = cFExRepository.DsColumnSettingCollection.Where(s=>s.DisplaySettingId == selectedDisplaySetting.Id);
            dsColumnSettingCollection = new ObservableCollection<DSCollumnSetting>();
            foreach(var c in _dsColumnSettingCollection) { dsColumnSettingCollection.Add(c); }
            SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;

            initHeader();

            addDataFromFileToDatabase = new RelayCommand(c =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                _ProgressBar = new Progress_Bar { Visible = true, Progress = 10, Status = "Start" };
                if (openFileDialog.ShowDialog() == true)
                {
                    Task.Factory.StartNew(() =>
                    {
                        cFExRepository.initRepositoryFromFile(openFileDialog.FileName);
                        visitorCollection = cFExRepository.VisitorCollection;
                        _ProgressBar.Status = "All data added to database";
                        _ProgressBar.Progress = 0;
                        Thread.Sleep(3000);
                        _ProgressBar.Visible = false;
                    });
                }
            });
            addSetting = new RelayCommand(c =>
            {
                cFExRepository.addDisplaySetting();
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
            });
            delSetting = new RelayCommand(c =>
            {
                cFExRepository.delDisplaySetting(selectedDisplaySetting);
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
            });
            changeDisplaySettingDefault = new RelayCommand(c =>
            {
                cFExRepository.changeDisplaySettingDefault(SelectedDisplaySetting);
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
            });
            addCollumn = new RelayCommand(c =>
            {
                cFExRepository.addDSCollumnSetting();
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
            }, c => dsColumnSettingCollection.Count() < 17);
            saveChanges = new RelayCommand(c =>
            {
                cFExRepository.saveChanges(selectedDisplaySetting, selectedCollumnSetting);
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
                initHeader();

            });
            removeCollumn = new RelayCommand(c =>
            {
                cFExRepository.delDSCollumnSetting(selectedCollumnSetting);
                DisplaySettingCollection = cFExRepository.DisplaySettingCollection;
                DsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
                SelectedCollumnSetting = cFExRepository.SelectedDSCollumnSetting;
                SelectedDisplaySetting = cFExRepository.SelectedDisplaySetting;
            }, c=> dsColumnSettingCollection.Count() > 2);

        }

        private void ProgressChanged(Progress_Bar progress)
        {
            _ProgressBar.Progress = progress.Progress;
            _ProgressBar.Status = progress.Status;
            _ProgressBar.Visible = progress.Visible;
//            Thread.Sleep(10);
        }

        private void initHeader()
        {
            var sc = dsColumnSettingCollection.ToArray();
            //коллекция visible
            bool[] _columnChecked = new bool[sc.Count()];
            for (int i = 0; i < sc.Count(); i++) { _columnChecked[i] = dsColumnSettingCollection.Where(s => s.Id == sc[i].Id).Select(s => s.Visible).FirstOrDefault(); }
            for (int i = 0; i < count_headers; i++) { try { columnChecked[i] = _columnChecked[i]; } catch { columnChecked[i] = false; } }
            //коллекция алиасов
            string[] _alias = new string[sc.Count()];
            for (int i = 0; i < sc.Count(); i++) { _alias[i] = dsColumnSettingCollection.Where(s => s.Id == sc[i].Id).Select(s => s.Alias).FirstOrDefault(); }
            for (int i = 0; i < count_headers; i++) { try { alias[i] = _alias[i]; } catch { alias[i] = "none"; } }
            //коллекция width
            int[] _width = new int[sc.Count()];
            for (int i = 0; i < sc.Count(); i++) { _width[i] = dsColumnSettingCollection.Where(s => s.Id == sc[i].Id).Select(s => s.Width).FirstOrDefault(); }
            for (int i = 0; i < count_headers; i++) { try { width[i] = _width[i]; } catch { width[i] = 100; } }
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


    }
}
