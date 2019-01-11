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
        CFExRepository cFExRepository;
        ObservableCollection<Visitor> visitorCollection;
        ObservableCollection<DisplaySetting> displaySettingCollection;
        ObservableCollection<DSCollumnSetting> dsColumnSettingCollection;

        DisplaySetting selectedSetting;
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

        public DisplaySetting SelectedSetting
        {
            get { return selectedSetting; }
            set { selectedSetting = value; OnPropertyChanged(nameof(SelectedSetting)); }
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

        RelayCommand selectedSettingChanged;
        public RelayCommand SelectedSettingChanged { get { return selectedSettingChanged; } }

        RelayCommand saveChanges;
        public RelayCommand SaveChanges { get { return saveChanges; } }

        RelayCommand addDataFromFileToDatabase;
        public RelayCommand AddDataFromFileToDatabase { get { return addDataFromFileToDatabase; } }

        public ViewModel()
        {
            cFExRepository = new CFExRepository();
            cFExRepository.progressChanged += ProgressChanged;
            visitorCollection = new ObservableCollection<Visitor>();
            displaySettingCollection = cFExRepository.DisplaySettingCollection;
            dsColumnSettingCollection = cFExRepository.DsColumnSettingCollection;
            selectedSetting = displaySettingCollection.Where(s => s.IsSelected == true).FirstOrDefault();
            selectedCollumnSetting = dsColumnSettingCollection.Where(s => s.IsSelected == true).FirstOrDefault();

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
        }

        private void ProgressChanged(Progress_Bar progress)
        {
            _ProgressBar.Progress = progress.Progress;
            _ProgressBar.Status = progress.Status;
            _ProgressBar.Visible = progress.Visible;
            Thread.Sleep(25);
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
