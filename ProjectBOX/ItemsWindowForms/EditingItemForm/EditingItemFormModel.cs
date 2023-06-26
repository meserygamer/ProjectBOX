using ProjectBOX.EntityFrameworkModelFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBOX.ItemsWindowForms.EditingItemForm
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

    public class EditingItemFormModel
    {
        private int _idOfCurrentItem;

        public delegate void EndOfDownloadItemEventHandler(ObjectDatum ItemData);
        public event EndOfDownloadItemEventHandler NotifyEndOfDownloadDataAboutItem;

        public EditingItemFormModel(int IDOfCurrentItem)
        {
            _idOfCurrentItem = IDOfCurrentItem;
        }

        public async void DownloadDataAboutItem()
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

        public async void ChangeDataAboutItemInDB(string itemName, string? itemDescription, byte[]? itemImage)
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
