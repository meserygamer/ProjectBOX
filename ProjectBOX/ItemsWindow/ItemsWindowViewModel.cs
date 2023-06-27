using ProjectBOX.Authorization.LoginUC;
using ProjectBOX.EntityFrameworkModelFiles;
using ProjectBOX.ItemsWindowForms.CreateContainerForm;
using ProjectBOX.ItemsWindowForms.CreateItemForm;
using ProjectBOX.ItemsWindowForms.CreateItemMoveForm;
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

namespace ProjectBOX.ItemsWindow
{
    public class ItemsWindowViewModel : INotifyPropertyChanged
    {
        #region UserChoseSeeAllObject bool
        private bool _userChoseSeeAllObject;

        public bool UserChoseSeeAllObject
        {
            get => _userChoseSeeAllObject;
            set
            {
                _userChoseSeeAllObject = value;
                OnPropertyChanged("UserChoseSeeAllObject");
                if (value)
                {
                    ClearSelectedCategory();
                    ReloadItemsList();
                }
            }
        }
        #endregion
        #region Category object?
        private object? _category;

        public object? Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged("Category");
                ClearButtonSeeallObject();
                if (Category is ContainerDatum container)
                { 
                    ReloadItemsList(container);
                    Application.Current.Resources["SelectedContainerID"] = container.ContainerId;
                }
            }
        }
        #endregion
        #region UserName string?
        private string? _userName;

        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region UserEmail string?
        private string? _userEmail;

        public string? UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region CategoriesList ObservableCollection<ContainerDatum>
        private ObservableCollection<ContainerDatum> _categoriesList = new ObservableCollection<ContainerDatum>();

        public ObservableCollection<ContainerDatum> CategoriesList
        {
            get => _categoriesList;
            set
            {
                _categoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }
        #endregion
        #region ItemsList ObservableCollection<CompleteTask>
        private ObservableCollection<CompleteTask> _itemsList = new ObservableCollection<CompleteTask>();

        public ObservableCollection<CompleteTask> ItemsList
        {
            get => _itemsList;
            set
            {
                _itemsList = value;
                OnPropertyChanged("ItemsList");
            }
        }
        #endregion
        #region SelectedItemInDataGrid CompleteTask
        private CompleteTask _selectedItemInDataGrid;

        public CompleteTask SelectedItemInDataGrid
        {
            get => _selectedItemInDataGrid;
            set
            {
                _selectedItemInDataGrid = value;
                OnPropertyChanged("SelectedItemInDataGrid");
                if(value is not null)
                {
                    Application.Current.Resources["SelectedItemID"] = _selectedItemInDataGrid.ObjectId;
                    (new EditingItemFormView()).ShowDialog();
                }
            }
        }
        #endregion

        #region UserClickOnSeeAllObjectButton RelayCommand
        private RelayCommand _userClickOnSeeAllObjectButton;

        public RelayCommand UserClickOnSeeAllObjectButton
        {
            get
            {
                return _userClickOnSeeAllObjectButton ??
                  (_userClickOnSeeAllObjectButton = new RelayCommand(obj =>
                  {
                      UserChoseSeeAllObject = true;
                  }));
            }
        }
        #endregion
        #region CreateContainerClick RelayCommand
        private RelayCommand _createContainerClick;

        public RelayCommand CreateContainerClick
        {
            get
            {
                return _createContainerClick ??
                  (_createContainerClick = new RelayCommand(obj =>
                  {
                      (new CreateContainerFormView()).ShowDialog();
                  }));
            }
        }
        #endregion
        #region CreateNewItemClick RelayCommand
        private RelayCommand _createNewItemClick;

        public RelayCommand CreateNewItemClick
        {
            get
            {
                return _createNewItemClick ??
                  (_createNewItemClick = new RelayCommand(obj =>
                  {
                      (new CreateItemFormView()).ShowDialog();
                  }));
            }
        }
        #endregion
        #region CreateMovementClick RelayCommand
        private RelayCommand _createMovementClick;

        public RelayCommand CreateMovementClick
        {
            get
            {
                return _createMovementClick ??
                  (_createMovementClick = new RelayCommand(obj =>
                  {
                      (new CreateItemMoveFormView()).ShowDialog();
                  }));
            }
        }
        #endregion

        #region ClearMethodsOfCategoryChose
        private void ClearSelectedCategory()
        {
            _category = null;
            OnPropertyChanged("Category");
        }

        private void ClearButtonSeeallObject()
        {
            _userChoseSeeAllObject = false;
            OnPropertyChanged("UserChoseSeeAllObject");
        }
        #endregion

        public ItemsWindowViewModel()
        {
            SetUserAreaData();
            ReloadCategoriesAsync();
        }

        #region SetUserDataMethods
        private void SetUserAreaData()
        {
            SetUserAreaName();
            SetUserEmail();
        }

        private async void SetUserAreaName()
        {
            await Task.Run(() => {UserName = ItemsWindowModel.GetItemsWindowModel().GetUserNameFromDataBase((int)Application.Current.Resources["UserID"]);});
        }

        private async void SetUserEmail()
        {
            await Task.Run(() => { UserEmail = ItemsWindowModel.GetItemsWindowModel().GetUserEmailFromDataBase((int)Application.Current.Resources["UserID"]);});
        }
        #endregion

        private async void ReloadCategoriesAsync()
        {
            await Task.Run(() => {CategoriesList = ItemsWindowModel.GetItemsWindowModel().GetAllContainerFromDB();});
        }

        #region ReloadItemsList async void
        private async void ReloadItemsList()
        {
            await Task.Run(() => { ItemsList = ItemsWindowModel.GetItemsWindowModel().GetAllItemsFromCategory();});
        }

        private async void ReloadItemsList(ContainerDatum container)
        {
            await Task.Run(() => { ItemsList = ItemsWindowModel.GetItemsWindowModel().GetAllItemsFromCategory(container); });
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
