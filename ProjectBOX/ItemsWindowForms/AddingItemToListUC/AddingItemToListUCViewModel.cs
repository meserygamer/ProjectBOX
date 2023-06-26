using ProjectBOX.EntityFrameworkModelFiles;
using ProjectBOX.ItemsWindowForms.CreateItemForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.AddingItemToListUC
{
    public class AddingItemToListUCViewModel : FrameworkElement, INotifyPropertyChanged
    {
        #region ListOfMovedItems ObservableCollection<ObjectDatum>
        private ObservableCollection<ObjectDatum> _listOfMovedItems = new ObservableCollection<ObjectDatum>();

        public ObservableCollection<ObjectDatum> ListOfMovedItems
        {
            get { return _listOfMovedItems; }
            set { 
                _listOfMovedItems = value;
                OnPropertyChanged("ListOfMovedItems");
            }
        }
        #endregion
        #region ListOfAllItems ObservableCollection<ObjectDatum>
        private ObservableCollection<ObjectDatum> _listOfAllItems;

        public ObservableCollection<ObjectDatum> ListOfAllItems
        {
            get { return _listOfAllItems; }
            set
            {
                _listOfAllItems = value;
                OnPropertyChanged("ListOfAllItems");
            }
        }
        #endregion

        #region SelectedItem ObjectDatum?
        private ObjectDatum? _selectedItem;

        public ObjectDatum? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private RelayCommand _clickOnAddButton;

        public RelayCommand ClickOnAddButton
        {
            get
            {
                return _clickOnAddButton ??
                  (_clickOnAddButton = new RelayCommand(obj =>
                  {
                      if(SelectedItem is not null)
                      {
                          ListOfAllItems.Remove(SelectedItem);
                          ListOfMovedItems.Add((ObjectDatum)SelectedItem);
                      }
                  }));
            }
        }

        public AddingItemToListUCViewModel()
        {
            AddingItemToListInteractionsWithDataBase.GetExampler().ItemsListWasFormed += FilingListOfAllItems;
            AddingItemToListInteractionsWithDataBase.GetExampler().FormedListOfAllItemsAsync();
        }

        private void FilingListOfAllItems(ObservableCollection<ObjectDatum> ListOfAllItemsFromDB) => ListOfAllItems = ListOfAllItemsFromDB;

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
