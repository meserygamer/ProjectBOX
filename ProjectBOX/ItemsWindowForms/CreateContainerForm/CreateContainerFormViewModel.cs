using ProjectBOX.ItemsWindowForms.EditingItemForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.CreateContainerForm
{
    public class CreateContainerFormViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Название контейнера
        /// </summary>
        #region public string CategoryName
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set {
                _categoryName = value;
                OnPropertyChanged();
            }
        }
        #endregion
        /// <summary>
        /// Описание контейнера
        /// </summary>
        #region public string CategoryDescription
        private string _categoryDescription;

        public string CategoryDescription
        {
            get { return _categoryDescription; }
            set
            {
                _categoryDescription = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Создание окна контейнер
        /// </summary>
        #region public RelayCommand CreateCategoryClick
        private RelayCommand _createCategoryClick;

        public RelayCommand CreateCategoryClick
        {
            get
            {
                return _createCategoryClick ??
                  (_createCategoryClick = new RelayCommand(obj =>
                  {
                      if (new Validator(_categoryName).CheckItemNameOnEmpty().Validation())
                      {
                          CreateContainerFormInteractionWithDataBase.GetExemplar().CategoryName = _categoryName;
                          CreateContainerFormInteractionWithDataBase.GetExemplar().CategoryDescription = _categoryDescription;
                          CreateContainerFormInteractionWithDataBase.GetExemplar().AddCategoryInDataBaseAsync();
                          ((CreateContainerFormView)Application.Current.Windows[Application.Current.Windows.Count - 1]).DialogResult = true; ///
                      }
                      else
                      {
                          MessageBox.Show("Поле названия контейнера - пустое, контейнер не добавлен");
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
