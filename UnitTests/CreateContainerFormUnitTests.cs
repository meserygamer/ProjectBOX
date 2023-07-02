using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectBOX.ItemsWindowForms.CreateContainerForm;

namespace UnitTests
{
    public class CreateContainerFormUnitTests
    {
        const string CorrectNameOfContainer = "Stock 1";

        /// <summary>
        /// Проверка на недопустимость null имени контейнера
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty1()
        {
            Assert.False((new Validator(null)).CheckItemNameOnEmpty().Validation());
        }

        /// <summary>
        /// Проверка на недопустимость пустого имени контейнера
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty2()
        {
            Assert.False((new Validator("")).CheckItemNameOnEmpty().Validation());
        }

        /// <summary>
        /// Проверка на допустимость корректного имени контейнера
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty3()
        {
            Assert.True((new Validator(CorrectNameOfContainer)).CheckItemNameOnEmpty().Validation());
        }
    }
}
