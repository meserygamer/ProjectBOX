using ProjectBOX.Authorization.LoginUC;
using System;
using System.Collections.Generic;
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
                ClearSelectedCategory();
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
