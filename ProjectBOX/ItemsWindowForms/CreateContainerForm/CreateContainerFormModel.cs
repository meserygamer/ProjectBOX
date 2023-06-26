using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.CreateContainerForm
{
    public class Validator
    {
        private string _itemName;
        private bool _isValid = true;

        public Validator(string? itemName)
        {
            _itemName = itemName ?? "";
        }

        public Validator CheckItemNameOnEmpty()
        {
            if (_itemName.Length == 0)
            {
                _isValid = false;
            }
            return this;
        }

        public bool Validation() => _isValid;
    }

    public class CreateContainerFormInteractionWithDataBase
    {
        private static CreateContainerFormInteractionWithDataBase _instance;

        #region CategoryName string
        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
        #endregion
        #region CategoryDescription string
        private string _categoryDescription;

        public string CategoryDescription
        {
            get { return _categoryDescription; }
            set { _categoryDescription = value; }
        }
        #endregion

        private CreateContainerFormInteractionWithDataBase() { }

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

        public static CreateContainerFormInteractionWithDataBase GetExemplar()
        {
            if(_instance is null)
            {
                return _instance = new CreateContainerFormInteractionWithDataBase();
            }
            else return _instance;
        }
    }
}
