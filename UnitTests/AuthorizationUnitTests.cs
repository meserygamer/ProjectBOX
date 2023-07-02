using ProjectBOX.Authorization.RegistrationUC;

namespace UnitTests
{
    public class RegistrationUnitTests
    {
        const string RightPassword = "Test123";
        const string RightLogin = "Mesery";
        /// <summary>
        /// �������� �� �������� ������� �������� �������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckLoginOnValid1()
        {
            Assert.False((new RegistrationData("Jime", RightPassword, RightPassword).Validator().CheckLoginOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� �������� ������ ����� ��������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckLoginOnValid2()
        {
            Assert.False((new RegistrationData("������", RightPassword, RightPassword).Validator().CheckLoginOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� ������ ������ ��������������� ���� �������� 
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckLoginOnValid3()
        {
            Assert.True((new RegistrationData(RightLogin, RightPassword, RightPassword).Validator().CheckLoginOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� �������������� ������ ��� ������� ����
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordOnValid1()
        {
            Assert.False((new RegistrationData(RightLogin, "test123", "test123").Validator().CheckPasswordOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� �������������� �������� �������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordOnValid2()
        {
            Assert.False((new RegistrationData(RightLogin, "Test1", "Test1").Validator().CheckPasswordOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� �������������� ������� ��� ����
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordOnValid3()
        {
            Assert.False((new RegistrationData(RightLogin, "Testte", "Testte").Validator().CheckPasswordOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� ������������ ����������� ������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordOnValid4()
        {
            Assert.True((new RegistrationData(RightLogin, RightPassword, RightPassword).Validator().CheckPasswordOnValid().Validate()));
        }

        /// <summary>
        /// �������� �� �������������� �� ������������� �������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordsOnEquals1()
        {
            Assert.False((new RegistrationData(RightLogin, RightPassword, "Test321").Validator().CheckPasswordsOnEquals().Validate()));
        }

        /// <summary>
        /// �������� �� ������������ ������������� �������
        /// </summary>
        [Fact]
        public void CheckMothodOfCheckPasswordsOnEquals2()
        {
            Assert.True((new RegistrationData(RightLogin, RightPassword, RightPassword).Validator().CheckPasswordsOnEquals().Validate()));
        }
    }
}