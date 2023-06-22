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

        public ObservableCollection<ContainerDatum> GetAllContainerFromDB()
        {
            using(ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<ContainerDatum>(DB.ContainerData);
            }
        }

        public ObservableCollection <CompleteTask> GetAllItemsFromCategory(ContainerDatum container)
        {
            using(ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<CompleteTask>(DB.CompleteTasks.Where(a => a.ContainerName == container.ContainerName));
            }
        }

        public ObservableCollection<CompleteTask> GetAllItemsFromCategory()
        {
            using (ProjectBoxDbContext DB = new ProjectBoxDbContext())
            {
                return new ObservableCollection<CompleteTask>(DB.CompleteTasks);
            }
        }
    }
}
