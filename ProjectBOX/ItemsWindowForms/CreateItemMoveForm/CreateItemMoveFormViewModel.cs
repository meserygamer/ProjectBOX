using ProjectBOX.EntityFrameworkModelFiles;
using ProjectBOX.ItemsWindowForms.AddingItemToListUC;
using ProjectBOX.ItemsWindowForms.EditingItemForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.CreateItemMoveForm
{
    public class CreateItemMoveFormViewModel : DependencyObject, INotifyPropertyChanged
    {
        #region UserDateTime DateTime
        private DateTime _userDateTime;

        public DateTime UserDateTime
        {
            get { return _userDateTime; }
            set {
                _userDateTime = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region MovementDescription string
        private string _movementDescription;

        public string MovementDescription
        {
            get { return _movementDescription; }
            set
            {
                _movementDescription = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ListOfMovedItems ObservableCollection<ObjectDatum> DependencyProperty
        public static readonly DependencyProperty ListOfMovedItemsProperty =
            DependencyProperty.Register("ListOfMovedItems", typeof(ObservableCollection<ObjectDatum>), typeof(CreateItemMoveFormViewModel));

        public ObservableCollection<ObjectDatum> ListOfMovedItems
        {
            get => (ObservableCollection<ObjectDatum>)GetValue(ListOfMovedItemsProperty);
            set => SetValue(ListOfMovedItemsProperty, value);
        }
        #endregion

        #region ClickOnCreateMoveButton RelayCommand
        private RelayCommand _clickOnCreateMoveButton;

        public RelayCommand ClickOnCreateMoveButton
        {
            get
            {
                return _clickOnCreateMoveButton ??
                  (_clickOnCreateMoveButton = new RelayCommand(obj =>
                  {
                      Validator MovementValidator = new Validator(UserDateTime, ListOfMovedItems);
                      if(!MovementValidator.CheckTimeOnCurrent().Validate())
                      {
                          MessageBox.Show("Введенная дата некорректна! Повторите ввод даты");
                          return;
                      }
                      if(!MovementValidator.CheckItemsCollectionOnNotEmpty().Validate())
                      {
                          MessageBox.Show("Список объектов к которым будет применяться перемещение - пуст! Введите по крайней мере один объект");
                          return;
                      }
                      (CreateItemMoveFormInteractionWithDB.GetExampler()).AddMovementInDataBase(UserDateTime,
                          (int)Application.Current.Resources["UserID"], (int)Application.Current.Resources["SelectedContainerID"],
                          MovementDescription, ListOfMovedItems);
                  }));
            }
        }
        #endregion

        public CreateItemMoveFormViewModel()
        {
            ListOfMovedItems = new ObservableCollection<ObjectDatum>();
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
