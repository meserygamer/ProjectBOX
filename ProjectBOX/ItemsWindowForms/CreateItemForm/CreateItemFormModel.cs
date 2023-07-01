using Microsoft.VisualBasic;
using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectBOX.ItemsWindowForms.CreateItemForm
{
    /// <summary>
    /// Валидатор создания объекта
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        private string _itemName;
        /// <summary>
        /// Состояние валидатора
        /// </summary>
        private bool _isValid = true;

        /// <summary>
        /// Конструктор валидатора
        /// </summary>
        /// <param name="itemName">Название имени предмета</param>
        public Validator(string? itemName)
        {
            _itemName = itemName ?? "";
        }

        /// <summary>
        /// Проверка имени на пустоту
        /// </summary>
        /// <returns>Экземпляр валидатора</returns>
        public Validator CheckItemNameOnEmpty()
        {
            if(_itemName.Length == 0)
            {
                _isValid = false;
            }
            return this;
        }

        /// <summary>
        /// Валидация
        /// </summary>
        /// <returns>Состояние валидатора</returns>
        public bool Validation() => _isValid;
    }

    /// <summary>
    /// Класс взаимодействия с базой
    /// </summary>
    public class CreateItemFormDataBase
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        private string _itemName;
        /// <summary>
        /// Описание предмета
        /// </summary>
        private string _itemDescription;
        /// <summary>
        /// Картинка
        /// </summary>
        private byte[] _image;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="itemName">Название предмета</param>
        /// <param name="itemDescription">Описание предмета</param>
        /// <param name="image">Картинка</param>
        public CreateItemFormDataBase(string itemName, string itemDescription, byte[] image)
        {
            _image = image;
            _itemName = itemName;
            _itemDescription = itemDescription;
        }

        /// <summary>
        /// Добавление предмета в базу
        /// </summary>
        public async void AddItemInDataBaseAsync()
        {
            await Task.Run(() =>
            {
                ObjectDatum datum = new ObjectDatum();
                datum.ObjectName = _itemName;
                datum.Description = _itemDescription;
                datum.Image = _image;
                datum.HistoryOfChangesObjectLocations = new List<HistoryOfChangesObjectLocation>();
                using (ProjectBoxDbContext DB = new ProjectBoxDbContext())
                {
                    DB.Add(datum);
                    if (DB.ContainerData.Count() > 0)
                    {
                        datum.HistoryOfChangesObjectLocations.Add(new HistoryOfChangesObjectLocation { Object = datum,
                            ContainerId = 1, Description = "CreateMove", UserId = (int)Application.Current.Resources["UserID"], ChangesDate = DateTime.Now}) ;
                    }
                    DB.SaveChanges();
                }
            });
        }
    }
}
