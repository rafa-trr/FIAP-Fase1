using FIAP.TECH.CORE.DOMAIN.Validation;
using FIAP.TECH.TESTS.Contacts.Setup;
using FluentValidation.TestHelper;

namespace FIAP.TECH.TESTS.Contacts.Unity;

[Collection(nameof(ContactFixtureCollection))]
public class ContactTests
{
    private readonly ContactTestsFixture _contactTestsFixture;
    private ContactInsertValidation? _contactInsertValidation;
    private ContactUpdateValidation? _contactUpdateValidation;

    public ContactTests(ContactTestsFixture contactTestsFixture)
    {
        _contactTestsFixture = contactTestsFixture;
    }

    [Fact(DisplayName = "Validando a entrada de dados sem erros ao criar um contato")]
    [Trait("Contato", "Validando Entrada de dados")]
    public void CreateContact_ValidateInput_NoErrors()
    {
        // Arrange
        _contactInsertValidation = _contactTestsFixture.GetValidationInsert();
        var contact = _contactTestsFixture.GetContactSucess();

        // Act
        var result = _contactInsertValidation.TestValidate(contact);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Validando a entrada de dados com erros esperados ao criar um contato")]
    [Trait("Contato", "Validando Entrada de dados")]
    public void CreateContact_ValidateInput_WithExpectedErrors()
    {
        // Arrange
        _contactInsertValidation = _contactTestsFixture.GetValidationInsert();
        var contactDTO = _contactTestsFixture.GetContactErrors();

        // Act
        var result = _contactInsertValidation.TestValidate(contactDTO);

        //Assert
        result.ShouldHaveAnyValidationError();
    }

    [Fact(DisplayName = "Validando a entrada de dados sem erros ao alterar um contato")]
    [Trait("Contato", "Validando Entrada de dados")]
    public void UpdateContact_ValidateInput_NoErrors()
    {
        // Arrange
        _contactUpdateValidation = _contactTestsFixture.GetValidationUpdate();
        var contact = _contactTestsFixture.GetContactForUpdateSucess();

        // Act
        var result = _contactUpdateValidation.TestValidate(contact);

        //Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact(DisplayName = "Validando a entrada de dados com erros esperados ao alterar um contato")]
    [Trait("Contato", "Validando Entrada de dados")]
    public void UpdateContact_ValidateInput_WithExpectedErrors()
    {
        // Arrange
        _contactUpdateValidation = _contactTestsFixture.GetValidationUpdate();
        var contact = _contactTestsFixture.GetContactForUpdateErrors();

        // Act
        var result = _contactUpdateValidation.TestValidate(contact);

        //Assert
        result.ShouldHaveAnyValidationError();
    }
}
