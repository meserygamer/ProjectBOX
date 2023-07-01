using Microsoft.Extensions.Options;
using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.CreateItemMoveForm
{
    /// <summary>
    /// Валидатор полей перемещения
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Перемещаемые предметы
        /// </summary>
        private ObservableCollection<ObjectDatum> _itemsForMove;
        /// <summary>
        /// Дата время перемещения
        /// </summary>
        private DateTime _movementDateTime;

        /// <summary>
        /// Состояние валидатора
        /// </summary>
        private bool IsValid = true;

        /// <summary>
        /// Конструктор валидатора
        /// </summary>
        /// <param name="moveDateTime">Дата время перемещения</param>
        /// <param name="itemsForMove">Перемещаемые предметы</param>
        public Validator(DateTime moveDateTime, ObservableCollection<ObjectDatum> itemsForMove)
        {
            _movementDateTime = moveDateTime;
            _itemsForMove = itemsForMove;
        }

        /// <summary>
        /// Проверка коллекции предметов на наличие
        /// </summary>
        /// <returns>Экземпляр валидатора</returns>
        public Validator CheckItemsCollectionOnNotEmpty()
        {
            if (_itemsForMove.Count == 0) IsValid = false;
            return this;
        }

        /// <summary>
        /// Проверка времени на корректность
        /// </summary>
        /// <returns>Экземпляр валидатора</returns>
        public Validator CheckTimeOnCurrent()
        {
            if (_movementDateTime.Year < 1900) IsValid = false;
            return this;
        }

        /// <summary>
        /// Валидация
        /// </summary>
        /// <returns>Результат валидации</returns>
        public bool Validate() => IsValid;
    }

    public class CreateItemMoveFormInteractionWithDB
    {
        #region SingletonPattern
        private static CreateItemMoveFormInteractionWithDB _singleton;

        private CreateItemMoveFormInteractionWithDB() { }

        public static CreateItemMoveFormInteractionWithDB GetExampler() =>
            _singleton ?? (_singleton = new CreateItemMoveFormInteractionWithDB());
        #endregion

        /// <summary>
        /// Доабвление перемещения в БД
        /// </summary>
        /// <param name="movementDateTime">Дата-время перемещения</param>
        /// <param name="UserID">номер пользователя</param>
        /// <param name="MovementTargetContainer">Целевой контейнер</param>
        /// <param name="MovementDescription">Описание перемещения</param>
        /// <param name="MovementItems">Перемещаемые предметы</param>
        public async void AddMovementInDataBase(DateTime movementDateTime, int UserID,
            int MovementTargetContainer, string MovementDescription,
            ObservableCollection<ObjectDatum> MovementItems)
        {
            await Task.Run(() => 
            {
                using (ProjectBoxDbContext DB = new())
                {
                    foreach (ObjectDatum MovementItem in MovementItems)
                    {
                        DB.HistoryOfChangesObjectLocations.Add(new HistoryOfChangesObjectLocation{UserId = UserID,
                            ObjectId = MovementItem.ObjectId, ContainerId = MovementTargetContainer,
                            Description = MovementDescription, ChangesDate = movementDateTime});
                    }
                    DB.SaveChanges();
                }
            });
        }
    }
}
