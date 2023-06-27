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
    public class Validator
    {
        private ObservableCollection<ObjectDatum> _itemsForMove;
        private DateTime _movementDateTime;

        private bool IsValid = true;

        public Validator(DateTime moveDateTime, ObservableCollection<ObjectDatum> itemsForMove)
        {
            _movementDateTime = moveDateTime;
            _itemsForMove = itemsForMove;
        }

        public Validator CheckItemsCollectionOnNotEmpty()
        {
            if (_itemsForMove.Count == 0) IsValid = false;
            return this;
        }

        public Validator CheckTimeOnCurrent()
        {
            if (_movementDateTime.Year < 1900) IsValid = false;
            return this;
        }

        public bool Validate() => IsValid;
    }

    public class CreateItemMoveFormInteractionWithDB
    {
        private static CreateItemMoveFormInteractionWithDB _singleton;

        private CreateItemMoveFormInteractionWithDB() { }

        public static CreateItemMoveFormInteractionWithDB GetExampler() =>
            _singleton ?? (_singleton = new CreateItemMoveFormInteractionWithDB());

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
