using Microsoft.Win32;
using ProjectBOX.ItemsWindowForms.CreateContainerForm;
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
        /// <summary>
        /// Название предмета
        /// </summary>
        #region public string ItemName
        private string _itemName;

        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged("ItemName");
            }
        }
        #endregion

        /// <summary>
        /// Описание предмета
        /// </summary>
        #region public string ItemDescription
        private string _itemDescription;

        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value;
                OnPropertyChanged("ItemDescription");
            }
        }
        #endregion

        /// <summary>
        /// байтовое представление картинки
        /// </summary>
        #region public byte[] ImageByteArray
        private byte[] _imageByteArray;

        public byte[] ImageByteArray
        {
            get => _imageByteArray;
            set
            {
                _imageByteArray = value;
                OnPropertyChanged("ImageByteArray");
            }
        }
        #endregion

        /// <summary>
        /// Обработчик кнопки создания объекта
        /// </summary>
        #region public RelayCommand CreateItem
        private RelayCommand _createItem;

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
                          ((CreateItemFormView)Application.Current.Windows[Application.Current.Windows.Count - 1]).DialogResult = true; ///
                      }
                      else
                      {
                          MessageBox.Show("Поле название предмета - пустое, предмет не добавлен");
                      }
                  }));
            }
        }
        #endregion

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
