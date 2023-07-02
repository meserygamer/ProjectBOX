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
        /// <summary>
        /// Отображаемая картинка
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
        /// Путь к файлу
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
        /// Обработчик кнопки обзора в системе
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
                          Image = File.ReadAllBytes(FileName);
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
        /// Байтовое представление картинки
        /// </summary>
        #region public byte[] Image DependencyProperty
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(byte[]), typeof(ItemPhotoUCViewModel));

        public byte[] Image
        {
            get { return (byte[])GetValue(ImageProperty); }
            set
            {
                SetValue(ImageProperty, value);
                SetImage(value);
            }
        }
        #endregion

        /// <summary>
        /// Создание картинки из массива байтов
        /// </summary>
        /// <param name="ByteArray">Массив байтов</param>
        /// <returns>БитМап форма картинки</returns>
        private BitmapImage CreateBitMapImageFromByteArray(byte[] ByteArray)
        {
            BitmapImage bitmap = new();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(ByteArray);
            bitmap.EndInit();
            return bitmap;
        }

        /// <summary>
        /// Метод установки картинки в рамку
        /// </summary>
        /// <param name="imgByteArray">Байтовая форма картинки</param>
        private void SetImage(byte[] imgByteArray) => ImageOnBorder = CreateBitMapImageFromByteArray(imgByteArray);

        #region INotifyPropertyChanged realize
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
