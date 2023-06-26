using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.CreateItemForm
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
            if(_itemName.Length == 0)
            {
                _isValid = false;
            }
            return this;
        }

        public bool Validation() => _isValid;
    }

    public class CreateItemFormDataBase
    {
        private string _itemName;
        private string _itemDescription;
        private byte[] _image;

        public CreateItemFormDataBase(string itemName, string itemDescription, byte[] image)
        {
            _image = image;
            _itemName = itemName;
            _itemDescription = itemDescription;
        }

        public async void AddItemInDataBaseAsync()
        {
            await Task.Run(() =>
            {
                ObjectDatum datum = new ObjectDatum();
                datum.ObjectName = _itemName;
                datum.Description = _itemDescription;
                datum.Image = _image;
                using (ProjectBoxDbContext DB = new ProjectBoxDbContext())
                {
                    DB.Add(datum);
                    DB.SaveChanges();
                }
            });
        }
    }
}
