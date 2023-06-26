using Microsoft.Win32;
using ProjectBOX.EntityFrameworkModelFiles;
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
        #region ItemName string
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
        #region ItemDescription string
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
        #region FileName string
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

        #region ItemImage byte[]
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
        #region ImageOnBorder BitmapImage
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

        #region ObserverButtonClick RelayCommand
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
                      ItemImage = File.ReadAllBytes(FileName);
                      //ImageOnBorder = CreateBitMapImageFromByteArray(Image);
                  }));
            }
        }
        #endregion
        #region ConfirmItemChange RelayCommand
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
                          _editingItemFormModel.ChangeDataAboutItemInDB(ItemName, ItemDescription, ItemImage);
                      }
                      else
                      {
                          MessageBox.Show("Поле название предмета - пустое, предмет не изменен");
                      }
                  }));
            }
        }
        #endregion

        #region ItemHistoryChanged ObservableCollection<HistoryOfChangesObjectLocation>
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

        private EditingItemFormModel _editingItemFormModel;

        public EditingItemFormViewModel()
        {
            if (Application.Current.Resources["SelectedItemID"] is Int32 SelectedItemID)
            {
                _editingItemFormModel = new EditingItemFormModel(SelectedItemID);
                _editingItemFormModel.NotifyEndOfDownloadDataAboutItem += SetStartZnachFromDB;
                _editingItemFormModel.DownloadDataAboutItem();
            }
        }

        private BitmapImage CreateBitMapImageFromByteArray(byte[] ByteArray)
        {
            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ByteArray);
            bitmap.EndInit();
            return bitmap;
        }

        private void SetImage(byte[] imgByteArray) => ImageOnBorder = CreateBitMapImageFromByteArray(imgByteArray);

        private void SetStartZnachFromDB(ObjectDatum ItemData)
        {
            ItemName = ItemData.ObjectName;
            ItemDescription = ItemData.Description;
            ItemImage = ItemData.Image;
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
