using Microsoft.Win32;
using ProjectBOX.EntityFrameworkModelFiles;
using ProjectBOX.ItemsWindowForms.CreateContainerForm;
using ProjectBOX.ItemsWindowForms.CreateItemForm;
using ProjectBOX.ItemsWindowForms.ItemPhotoUC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectBOX.ItemsWindowForms.EditingItemForm
{
    public class EditingItemFormViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        #region public string ItemName
        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
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
        #region public string? ItemDescription
        private string? _itemDescription;

        public string? ItemDescription
        {
            get { return _itemDescription; }
            set
            {
                _itemDescription = value;
                OnPropertyChanged("ItemDescription");
            }
        }
        #endregion
        /// <summary>
        /// Путь к файлу с картинкой
        /// </summary>
        #region public string FileName
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }
        #endregion

        /// <summary>
        /// Картинка в байт форме
        /// </summary>
        #region public byte[]? ItemImage
        private byte[]? _itemImage;

        public byte[]? ItemImage
        {
            get { return _itemImage; }
            set
            {
                _itemImage = value;
                SetImage(value);
                OnPropertyChanged();
            }
        }
        #endregion
        /// <summary>
        /// Битмап форма картинки
        /// </summary>
        #region public BitmapImage ImageOnBorder
        private BitmapImage _imageOnBorder;

        public BitmapImage ImageOnBorder
        {
            get => _imageOnBorder;
            set
            {
                _imageOnBorder = value;
                OnPropertyChanged("ImageOnBorder");
            }
        }
        #endregion

        /// <summary>
        /// Обработчик кнопки поиска картинки
        /// </summary>
        #region public RelayCommand ObserverButtonClick
        private RelayCommand _observerButtonClick;

        public RelayCommand ObserverButtonClick
        {
            get
            {
                return _observerButtonClick ??
                  (_observerButtonClick = new RelayCommand(obj =>
                  {
                      OpenFileDialog fileDialog = new OpenFileDialog();
                      fileDialog.ShowDialog();
                      FileName = fileDialog.FileName;
                      try
                      {
                          ItemImage = File.ReadAllBytes(FileName);
                      }
                      catch
                      {
                          return;
                      }
                  }));
            }
        }
        #endregion
        /// <summary>
        /// Обработчик кнопки подтверждение изменения предмета
        /// </summary>
        #region public RelayCommand ConfirmItemChange
        private RelayCommand _confirmItemChange;

        public RelayCommand ConfirmItemChange
        {
            get
            {
                return _confirmItemChange ??
                  (_confirmItemChange = new RelayCommand(obj =>
                  {
                      if (new Validator(ItemName).CheckItemNameOnEmpty().Validation())
                      {
                          _editingItemFormModel.ChangeDataAboutItemInDBAsync(ItemName, ItemDescription, ItemImage);
                          (Application.Current.Windows.OfType<EditingItemFormView>().FirstOrDefault()).DialogResult = true; ///
                      }
                      else
                      {
                          MessageBox.Show("Поле название предмета - пустое, предмет не изменен");
                      }
                  }));
            }
        }
        #endregion

        /// <summary>
        /// История изменений предмета
        /// </summary>
        #region public ObservableCollection<HistoryOfChangesObjectLocation>? ItemHistoryChanged
        private ObservableCollection<HistoryOfChangesObjectLocation>? _itemHistoryChanged;

        public ObservableCollection<HistoryOfChangesObjectLocation>? ItemHistoryChanged
        {
            get { return _itemHistoryChanged; }
            set
            {
                _itemHistoryChanged = value;
                OnPropertyChanged("ItemHistoryChanged");
            }
        }
        #endregion

        /// <summary>
        /// Model окна 
        /// </summary>
        private EditingItemFormModel _editingItemFormModel;

        /// <summary>
        /// Конструктор ViewModel окна
        /// </summary>
        public EditingItemFormViewModel()
        {
            if (Application.Current.Resources["SelectedItemID"] is Int32 SelectedItemID)
            {
                _editingItemFormModel = new EditingItemFormModel(SelectedItemID);
                _editingItemFormModel.NotifyEndOfDownloadDataAboutItem += SetStartZnachFromDB;
                _editingItemFormModel.DownloadDataAboutItemAsync();
            }
        }

        /// <summary>
        /// Создание битмап картинки из байт массива
        /// </summary>
        /// <param name="ByteArray">Байт массив</param>
        /// <returns>Битмап форма картинки</returns>
        private BitmapImage CreateBitMapImageFromByteArray(byte[] ByteArray)
        {
            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ByteArray);
            bitmap.EndInit();
            return bitmap;
        }

        /// <summary>
        /// Метод обновления картинки на экране
        /// </summary>
        /// <param name="imgByteArray"></param>
        private void SetImage(byte[] imgByteArray) => ImageOnBorder = CreateBitMapImageFromByteArray(imgByteArray);

        /// <summary>
        /// Установка первичных значений из БД
        /// </summary>
        /// <param name="ItemData">Item из БД</param>
        private void SetStartZnachFromDB(ObjectDatum ItemData)
        {
            ItemName = ItemData.ObjectName;
            ItemDescription = ItemData.Description;
            if(ItemData.Image is not null) ItemImage = ItemData.Image;
            ItemHistoryChanged = new ObservableCollection<HistoryOfChangesObjectLocation>(ItemData.HistoryOfChangesObjectLocations);
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
