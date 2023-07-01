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
        /// <summary>
        /// Список перемещаемых предметов
        /// </summary>
        #region public ObservableCollection<ObjectDatum> ListOfMovedItems DependencyProperty
        public static readonly DependencyProperty ListOfMovedItemsProperty =
            DependencyProperty.Register("ListOfMovedItems", typeof(ObservableCollection<ObjectDatum>), typeof(AddingItemToListUCViewModel));

        public ObservableCollection<ObjectDatum> ListOfMovedItems
        {
            get => (ObservableCollection<ObjectDatum>)GetValue(ListOfMovedItemsProperty);
            set => SetValue(ListOfMovedItemsProperty, value);
        }
        #endregion

        /// <summary>
        /// Список всех предметов
        /// </summary>
        #region public ObservableCollection<ObjectDatum> ListOfAllItems
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

        /// <summary>
        /// Введенный предмет
        /// </summary>
        #region public ObjectDatum? SelectedItem
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

        /// <summary>
        /// Обработчик кнопки "Добавить предмет"
        /// </summary>
        #region public RelayCommand ClickOnAddButton
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
        #endregion

        public AddingItemToListUCViewModel()
        {
            ListOfMovedItems = new ObservableCollection<ObjectDatum>();
            AddingItemToListInteractionsWithDataBase.GetExampler().ItemsListWasFormed += FilingListOfAllItems;
            AddingItemToListInteractionsWithDataBase.GetExampler().FormedListOfAllItemsAsync();
        }

        /// <summary>
        /// Заполнение списка предметов
        /// </summary>
        /// <param name="ListOfAllItemsFromDB">список из предметов БД</param>
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
