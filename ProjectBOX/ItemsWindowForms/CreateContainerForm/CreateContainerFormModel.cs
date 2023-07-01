using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.CreateContainerForm
{
    /// <summary>
    /// Валидатор окна создания контейнера
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Имя контейнера
        /// </summary>
        private string _containerName;
        /// <summary>
        /// Состояние валидатора
        /// </summary>
        private bool _isValid = true;

        /// <summary>
        /// Конструктор валидатора
        /// </summary>
        /// <param name="ContainerName">Имя контейнера</param>
        public Validator(string? ContainerName)
        {
            _containerName = ContainerName ?? "";
        }

        /// <summary>
        /// Проверка имени контейнера на не пустоту
        /// </summary>
        /// <returns>Экземпляр валидатора</returns>
        public Validator CheckItemNameOnEmpty()
        {
            if (_containerName.Length == 0)
            {
                _isValid = false;
            }
            return this;
        }

        /// <summary>
        /// Проверка на уникальность контейнера
        /// </summary>
        /// <returns>Экземпляр валидатора</returns>
        public Validator CheckOnUnique()
        {
            using (ProjectBoxDbContext DB = new())
            {
                if(DB.ContainerData.Where(a => a.ContainerName == _containerName).Count() != 0)
                    _isValid = false;
            }
            return this;
        }

        /// <summary>
        /// Валидация
        /// </summary>
        /// <returns>Результат валидации</returns>
        public bool Validation() => _isValid;
    }

    /// <summary>
    /// Связь окна с БД
    /// </summary>
    public class CreateContainerFormInteractionWithDataBase
    {
        #region singletonPattern
        private static CreateContainerFormInteractionWithDataBase _instance;

        private CreateContainerFormInteractionWithDataBase() { }

        public static CreateContainerFormInteractionWithDataBase GetExemplar()
        {
            if (_instance is null)
            {
                return _instance = new CreateContainerFormInteractionWithDataBase();
            }
            else return _instance;
        }
        #endregion

        /// <summary>
        /// Имя контейнера
        /// </summary>
        #region public string CategoryName
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
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
            set { _categoryDescription = value; }
        }
        #endregion

        /// <summary>
        /// Добавление контейнера в базу данных
        /// </summary>
        public async void AddCategoryInDataBaseAsync()
        {
            await Task.Run(() =>
            {
                ContainerDatum Container = new() {ContainerName = CategoryName, Description = CategoryDescription};
                using(ProjectBoxDbContext DB = new())
                {
                    DB.ContainerData.Add(Container);
                    DB.SaveChanges();
                }
            });
        }
    }
}
