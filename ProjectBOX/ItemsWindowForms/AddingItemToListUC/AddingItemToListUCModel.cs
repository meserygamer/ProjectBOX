using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.AddingItemToListUC
{
    public class AddingItemToListInteractionsWithDataBase
    {
        #region SingletonPattern
        private static AddingItemToListInteractionsWithDataBase _singleton;

        private AddingItemToListInteractionsWithDataBase() { }

        public static AddingItemToListInteractionsWithDataBase GetExampler()
        {
            if (_singleton is null)
                return _singleton = new AddingItemToListInteractionsWithDataBase();
            return _singleton;
        }
        #endregion

        /// <summary>
        /// Событие формирования списка предметов из БД
        /// </summary>
        #region public event ListWasFormed ItemsListWasFormed
        public event ListWasFormed ItemsListWasFormed;
        /// <summary>
        /// Делегат ивента о формировании списка предметов из БД
        /// </summary>
        /// <param name="List">Список предметов</param>
        public delegate void ListWasFormed(ObservableCollection<ObjectDatum> List);
        #endregion

        /// <summary>
        /// Получение списка предметов из БД
        /// </summary>
        public async void FormedListOfAllItemsAsync()
        {
            ObservableCollection<ObjectDatum> ListOfAllItems;
            await Task.Run(() =>
            {
                using (ProjectBoxDbContext DB = new())
                {
                    ListOfAllItems = new ObservableCollection<ObjectDatum>(DB.ObjectData);
                }
                ItemsListWasFormed(ListOfAllItems);
            });
        }
    }
}
