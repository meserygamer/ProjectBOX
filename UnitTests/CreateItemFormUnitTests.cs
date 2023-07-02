using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBOX.ItemsWindowForms.CreateItemForm;

namespace UnitTests
{
    public class CreateItemFormUnitTests
    {
        const string CorrectNameOfItem = "Box 1";

        /// <summary>
        /// Проверка на недопустимость null имени предмета
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckItemNameOnEmpty1()
        {
            Assert.False((new Validator(null)).CheckItemNameOnEmpty().Validation());
        }

        /// <summary>
        /// Проверка на недопустимость пустого имени предмета
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckItemNameOnEmpty2()
        {
            Assert.False((new Validator("")).CheckItemNameOnEmpty().Validation());
        }

        /// <summary>
        /// Проверка на допустимость корректного имени
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckItemNameOnEmpty3()
        {
            Assert.True((new Validator(CorrectNameOfItem)).CheckItemNameOnEmpty().Validation());
        }
    }
}
