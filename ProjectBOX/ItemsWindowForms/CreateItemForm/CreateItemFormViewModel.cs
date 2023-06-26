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
                MessageBox.Show($"{_imageByteArray}");
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
