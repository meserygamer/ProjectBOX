using ProjectBOX.Authorization.LoginUC;
using ProjectBOX.EntityFrameworkModelFiles;
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
        private bool _userChoseSeeAllObject;
        private RelayCommand _userClickOnSeeAllObjectButton;
        private object? _category;
        private ObservableCollection<ContainerDatum> _categoriesList = new ObservableCollection<ContainerDatum>();
        private ObservableCollection<CompleteTask> _itemsList = new ObservableCollection<CompleteTask>();

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

        public object? Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged("Category");
                ClearButtonSeeallObject();
                if(Category is ContainerDatum container) ReloadItemsList(container);
            }
        }

        public ObservableCollection<ContainerDatum> CategoriesList
        {
            get => _categoriesList;
            set
            {
                _categoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }

        public ObservableCollection<CompleteTask> ItemsList
        {
            get => _itemsList;
            set
            {
                _itemsList = value;
                OnPropertyChanged("ItemsList");
            }
        }

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

        public ItemsWindowViewModel()
        {
            ReloadCategoriesAsync();
        }

        private async void ReloadCategoriesAsync()
        {
            await Task.Run(() => {CategoriesList = ItemsWindowModel.GetItemsWindowModel().GetAllContainerFromDB();});
        }

        private async void ReloadItemsList()
        {
            await Task.Run(() => { ItemsList = ItemsWindowModel.GetItemsWindowModel().GetAllItemsFromCategory();});
        }

        private async void ReloadItemsList(ContainerDatum container)
        {
            await Task.Run(() => { ItemsList = ItemsWindowModel.GetItemsWindowModel().GetAllItemsFromCategory(container); });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
