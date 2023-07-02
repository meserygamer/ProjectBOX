using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ProjectBOX.EntityFrameworkModelFiles;
using ProjectBOX.ItemsWindowForms.CreateItemMoveForm;

namespace UnitTests
{
    public class CreateItemMoveFormUnitTests
    {
        public ObservableCollection<ObjectDatum> EmptyCollection = new ObservableCollection<ObjectDatum>();
        public ObservableCollection<ObjectDatum> FullCollection = new ObservableCollection<ObjectDatum>((new ObjectDatum[1] {new ObjectDatum()}));
        public DateTime CorrectDateTime = new DateTime(2023, 07, 2);
        public DateTime InCorrectDateTime = new DateTime(103, 07, 2);

        /// <summary>
        /// Проверка на недопустимость пустых коллекций предметов
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckItemsCollectionOnNotEmpty1()
        {
            Assert.False((new Validator(CorrectDateTime, EmptyCollection)).CheckItemsCollectionOnNotEmpty().Validate());
        }

        /// <summary>
        /// Проверка допустимость непустых коллекций предметов
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckItemsCollectionOnNotEmpty2()
        {
            Assert.True((new Validator(CorrectDateTime, FullCollection)).CheckItemsCollectionOnNotEmpty().Validate());
        }

        /// <summary>
        /// Проверка на не допустимость неправильных дат
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckTimeOnCurrent1()
        {
            Assert.False((new Validator(InCorrectDateTime, FullCollection)).CheckTimeOnCurrent().Validate());
        }

        /// <summary>
        /// Проверка на допустимость правильных дат
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckTimeOnCurrent2()
        {
            Assert.True((new Validator(CorrectDateTime, FullCollection)).CheckTimeOnCurrent().Validate());
        }
    }
}
