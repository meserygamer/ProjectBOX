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

namespace ProjectBOX.ItemsWindowForms.ItemPhotoUC
{
    class ItemPhotoUCViewModel : INotifyPropertyChanged
    {
        private BitmapImage _imageOnBorder;
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
                      byte[] byteImage = File.ReadAllBytes(fileDialog.FileName);
                      _imageOnBorder = new BitmapImage();
                      _imageOnBorder.BeginInit();
                      _imageOnBorder.StreamSource = new MemoryStream(byteImage);
                      _imageOnBorder.EndInit();
                      OnPropertyChanged("ImageOnBorder");
                  }));
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

        public ItemPhotoUCViewModel()
        {
            ImageOnBorder = new BitmapImage();
            ImageOnBorder.BeginInit();
            ImageOnBorder.StreamSource = new MemoryStream(File.ReadAllBytes("C:\\Users\\kiril\\Downloads\\abstraktsiia_vektor_kot_321_1920x1080.jpg"));
            ImageOnBorder.EndInit();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
