using FIAP.TECH.CORE.DOMAIN.Validation;
using FIAP.TECH.TESTS.Contacts.Setup;
using FluentValidation.TestHelper;


namespace FIAP.TECH.TESTS.Contacts.Unity
{
    [Collection(nameof(ContactFixtureCollection))]
    public class ContactTests
    {
        private readonly ContactTestsFixture _contactTestsFixture;
        private ContactInsertValidation _contactInsertValidation;


        public ContactTests(ContactTestsFixture contactTestsFixture)
        {
            _contactTestsFixture = contactTestsFixture;
        }

        [Fact(DisplayName = "Validando a entrada de dados conforme o especificado")]
        [Trait("Contato", "Validando Entrada de dados sem erros")]
        public void Validate_Data_Input_No_Erros()
        {
            // Arrange
            _contactInsertValidation = _contactTestsFixture.GetValidationInsert();
            var contactDTO = _contactTestsFixture.GetContactSucess();

            // Act
            var result = _contactInsertValidation.TestValidate(contactDTO);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Validando a entrada de dados conforme o especificado")]
        [Trait("Contato", "Validando Entrada de dados com erros esperados")]
        public void Validate_Data_Input_Erros()
        {
            // Arrange
            _contactInsertValidation = _contactTestsFixture.GetValidationInsert();
            var contactDTO = _contactTestsFixture.GetContactErrors();

            // Act
            var result = _contactInsertValidation.TestValidate(contactDTO);

            //Assert
            result.ShouldHaveAnyValidationError();
        }

    }
}
