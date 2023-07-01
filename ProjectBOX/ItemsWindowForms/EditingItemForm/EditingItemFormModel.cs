using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.EditingItemForm
{
    /// <summary>
    /// Валидатор окна редактирования предмета
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Имя предмета
        /// </summary>
        private string _itemName;
        /// <summary>
        /// Состояние валидатора
        /// </summary>
        private bool _isValid = true;

        /// <summary>
        /// Конструктор валидатора
        /// </summary>
        /// <param name="itemName">Имя предмета</param>
        public Validator(string? itemName)
        {
            _itemName = itemName ?? "";
        }

        /// <summary>
        /// Проверка имени на не пустоту
        /// </summary>
        /// <returns></returns>
        public Validator CheckItemNameOnEmpty()
        {
            if (_itemName.Length == 0)
            {
                _isValid = false;
            }
            return this;
        }

        /// <summary>
        /// Валидация
        /// </summary>
        /// <returns>Валидность данных</returns>
        public bool Validation() => _isValid;
    }

    /// <summary>
    /// Model окна изменения предмета
    /// </summary>
    public class EditingItemFormModel
    {
        /// <summary>
        /// Номер изменяемого предмета
        /// </summary>
        private int _idOfCurrentItem;

        //Окночание загрузки предмета
        #region public event EndOfDownloadItemEventHandler NotifyEndOfDownloadDataAboutItem
        /// <summary>
        /// Делегат события окончания загрузки предмета из БД
        /// </summary>
        /// <param name="ItemData">Предмет</param>
        public delegate void EndOfDownloadItemEventHandler(ObjectDatum ItemData);
        /// <summary>
        /// Событие окнчания загрузки предмета из БД
        /// </summary>
        public event EndOfDownloadItemEventHandler NotifyEndOfDownloadDataAboutItem;
        #endregion

        /// <summary>
        /// Конструктор модели БД
        /// </summary>
        /// <param name="IDOfCurrentItem">Номер выбранного предмета</param>
        public EditingItemFormModel(int IDOfCurrentItem)
        {
            _idOfCurrentItem = IDOfCurrentItem;
        }

        /// <summary>
        /// Загрузка Информации о предмете
        /// </summary>
        public async void DownloadDataAboutItemAsync()
        {
            ObjectDatum? ItemData = new();
            await Task.Run(() => 
            {
                using (ProjectBoxDbContext DB = new())
                {
                    ItemData = DB.ObjectData.Find(_idOfCurrentItem);
                    ItemData.HistoryOfChangesObjectLocations = DB.HistoryOfChangesObjectLocations.Where(a => a.ObjectId == _idOfCurrentItem).ToList();
                    foreach(var ChangeOfItem in ItemData.HistoryOfChangesObjectLocations)
                    {
                        ChangeOfItem.Container = DB.ContainerData.Find(ChangeOfItem.ContainerId);
                        ChangeOfItem.User = DB.UserData.Find(ChangeOfItem.UserId);
                    }
                }
            });
            NotifyEndOfDownloadDataAboutItem((ObjectDatum)ItemData);
        }

        /// <summary>
        /// Изменение информации о предмете
        /// </summary>
        /// <param name="itemName">Название предмета</param>
        /// <param name="itemDescription">Описание предмета</param>
        /// <param name="itemImage">Картинка предмета</param>
        public async void ChangeDataAboutItemInDBAsync(string itemName, string? itemDescription, byte[]? itemImage)
        {
            await Task.Run(() =>
            {
                using (ProjectBoxDbContext DB = new())
                {
                    ObjectDatum? CurrentItem = DB.ObjectData.Find(_idOfCurrentItem);
                    if (CurrentItem != null)
                    {
                        CurrentItem.ObjectName = itemName;
                        CurrentItem.Image = itemImage;
                        CurrentItem.Description = itemDescription;
                    }
                    DB.SaveChanges();
                }
            });
        }
    }
}
