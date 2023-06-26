using Microsoft.Win32;
using ProjectBOX.ItemsWindowForms.ItemPhotoUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.CreateItemForm
{
    class CreateItemFormViewModel : INotifyPropertyChanged
    {
        private string _itemName;
        private string _itemDescription;
        private byte[] _imageByteArray;
        private RelayCommand _createItem;

        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged("ItemName");
            }
        }

        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value;
                OnPropertyChanged("ItemDescription");
            }
        }

        public byte[] ImageByteArray
        {
            get => _imageByteArray;
            set
            {
                _imageByteArray = value;
                OnPropertyChanged("ImageByteArray");
            }
        }

        public RelayCommand CreateItem
        {
            get
            {
                return _createItem ??
                  (_createItem = new RelayCommand(obj =>
                  {
                      if(new Validator(_itemName).CheckItemNameOnEmpty().Validation())
                      {
                          (new CreateItemFormDataBase(ItemName, ItemDescription, ImageByteArray)).AddItemInDataBaseAsync();
                      }
                      else
                      {
                          MessageBox.Show("Поле название предмета - пустое, предмет не добавлен");
                      }
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
