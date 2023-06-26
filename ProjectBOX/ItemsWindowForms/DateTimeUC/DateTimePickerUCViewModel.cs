using ProjectBOX.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.DateTimeUC
{
    public class DateTimePickerUCViewModel : FrameworkElement, INotifyPropertyChanged
    {
        #region Date string
        private string _date;

        public string Date
        {
            get { return _date; }
            set {
                _date = value;
                OnPropertyChanged();
                UpdateDateTime();
            }
        }
        #endregion
        #region SelectedMinutes int?
        private int? _selectedMinutes;

        public int? SelectedMinutes
        {
            get { return _selectedMinutes; }
            set
            {
                _selectedMinutes = value;
                OnPropertyChanged();
                UpdateDateTime();
            }
        }
        #endregion
        #region SelectedHours int?
        private int? _selectedHours;

        public int? SelectedHours
        {
            get { return _selectedHours; }
            set
            {
                _selectedHours = value;
                OnPropertyChanged();
                UpdateDateTime();
            }
        }
        #endregion
        //#region CurrentDateTime DateTime
        //private DateTime _currentDateTime;

        //public DateTime CurrentDateTime
        //{
        //    get { return _currentDateTime; }
        //    set {
        //        _currentDateTime = value;
        //        OnPropertyChanged();
        //    }
        //}
        //#endregion
        #region CurrentDateTime DateTime DependencyProperty
        public static readonly DependencyProperty CurrentDateTimeProperty;

        static DateTimePickerUCViewModel()
        {
            CurrentDateTimeProperty = DependencyProperty.Register("CurrentDateTime", typeof(DateTime), typeof(DateTimePickerUCViewModel));
        }

        public DateTime CurrentDateTime
        {
            get { return (DateTime)GetValue(CurrentDateTimeProperty); }
            set { SetValue(CurrentDateTimeProperty, value); }
        }
        #endregion

        public ObservableCollection<int> Hours {get; } = new ObservableCollection<int>(Enumerable.Range(1, 24));
        public ObservableCollection<int> Minutes {get; } = new ObservableCollection<int>(Enumerable.Range(1, 60));

        private void UpdateDateTime()
        {
            if(Date is not null && Date != "" && SelectedHours is not null && SelectedMinutes is not null)
            CurrentDateTime = new DateTime(Convert.ToInt32(Date.Split(".")[2]),
                Convert.ToInt32(Date.Split(".")[1]),
                Convert.ToInt32(Date.Split(".")[0]),
                (int)SelectedHours, (int)SelectedMinutes, 0);
        }

        #region INotifyPropertyChanged Realize
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
