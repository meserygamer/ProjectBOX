using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Controls;
using ProjectBOX.Authorization.LoginUC;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.ItemPhotoUC
{
    class ItemPhotoUCViewModel : FrameworkElement, INotifyPropertyChanged
    {
        private BitmapImage _imageOnBorder;
        private RelayCommand _observerButtonClick;
        private string _fileName;

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(byte[]), typeof(ItemPhotoUCViewModel));

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
                      Image = File.ReadAllBytes(FileName);
                      //ImageOnBorder = CreateBitMapImageFromByteArray(Image);
                  }));
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set { 
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public BitmapImage ImageOnBorder
        {
            get => _imageOnBorder;
            set
            {
                _imageOnBorder = value;
                OnPropertyChanged("ImageOnBorder");
            }
        }

        public byte[] Image
        {
            get { return (byte[])GetValue(ImageProperty); }
            set {
                SetValue(ImageProperty, value);
                SetImage(value);
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
