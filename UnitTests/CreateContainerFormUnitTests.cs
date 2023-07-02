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
        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty1()
        {
            Assert.False((new Validator(null)).CheckItemNameOnEmpty().Validation());
        }

        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty2()
        {
            Assert.False((new Validator("")).CheckItemNameOnEmpty().Validation());
        }

        [Fact]
        public void CheckMothodOfCheckContainerNameOnEmpty3()
        {
            Assert.True((new Validator(CorrectNameOfContainer)).CheckItemNameOnEmpty().Validation());
        }
    }
}
