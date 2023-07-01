using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindow
{
    class ItemsWindowModel
    {
        #region SingletonPattern
        private static ItemsWindowModel _instance;

        private ItemsWindowModel() { }

        public static ItemsWindowModel GetItemsWindowModel()
        {
            if (_instance == null)
            {
                _instance = new ItemsWindowModel();
            }
            return _instance;
        }
        #endregion

        /// <summary>
        /// Получение списка контейнеров с сервера
        /// </summary>
        /// <returns>Список контейнеров</returns>
        public ObservableCollection<ContainerDatum> GetAllContainerFromDB()
        {
            using(ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<ContainerDatum>(DB.ContainerData);
            }
        }

        //Получение с сервера всех предметов
        #region GetAllItemsFromCategory ObservableCollection <CompleteTask>
        /// <summary>
        /// Получения списка предметов из БД
        /// </summary>
        /// <param name="container">имя контейнера</param>
        /// <returns>Список предметов</returns>
        public ObservableCollection <CompleteTask> GetAllItemsFromCategory(ContainerDatum container)
        {
            using(ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<CompleteTask>(DB.CompleteTasks.Where(a => a.ContainerName == container.ContainerName));
            }
        }

        /// <summary>
        /// Получение всех предметов с сервера
        /// </summary>
        /// <returns>Список предметов</returns>
        public ObservableCollection<CompleteTask> GetAllItemsFromCategory()
        {
            using (ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<CompleteTask>(DB.CompleteTasks);
            }
        }
        #endregion

        /// <summary>
        /// Поучение имени пользователя из БД
        /// </summary>
        /// <param name="userID">Номер пользователя</param>
        /// <returns>имя пользователя</returns>
        public string? GetUserNameFromDataBase(int userID)
        {
            using (ProjectBoxDbContext DB = new())
            {
                return DB.UserData.Find(userID)?.UserName;
            }
        }

        /// <summary>
        /// Получение электронной почты из БД
        /// </summary>
        /// <param name="userID">Номер пользователя</param>
        /// <returns>электронная почта</returns>
        public string? GetUserEmailFromDataBase(int userID)
        {
            using (ProjectBoxDbContext DB = new())
            {
                return DB.UserData.Find(userID)?.Email;
            }
        }
    }
}
