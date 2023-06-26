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
        private static AddingItemToListInteractionsWithDataBase _singleton;

        public delegate void ListWasFormed(ObservableCollection<ObjectDatum> List);
        public event ListWasFormed ItemsListWasFormed;

        private AddingItemToListInteractionsWithDataBase() { }

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

        public static AddingItemToListInteractionsWithDataBase GetExampler()
        {
            if(_singleton is null) 
                return _singleton = new AddingItemToListInteractionsWithDataBase();
            return _singleton;
        }
    }
}
