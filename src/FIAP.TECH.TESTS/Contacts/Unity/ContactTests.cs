using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FIAP.TECH.TESTS.Contacts.Setup;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

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

    [Theory(DisplayName = "Retorna os contatos por DDD")]
    [Trait("Contato", "Retorna os contatos existentes")]
    [InlineData("11")]
    [InlineData("22")]
    [InlineData("33")]
    public async Task Get_List_Contact_By_DDD(string DDD)
    {
        // Arrange
        var mockRepo = new Mock<IContactRepository>();
        //-Configura o repositorio emulando o metodo de retorno com o DDD
        mockRepo.Setup(repo => repo.GetByDdd(It.IsAny<string>()))
            .ReturnsAsync((string predicate) => _contactTestsFixture.GetListContact(predicate));

        // Act
        var result = await mockRepo.Object.GetByDdd(DDD);

        // Assert
        Assert.NotEmpty(result);
    }

    [Theory(DisplayName = "Lista deve Retornar vazia de contatos por DDD")]
    [Trait("Contato", "Lista deve retornar vazia")]
    [InlineData("00")]
    [InlineData("05")]
    [InlineData("10")]
    public async Task Get_List_Contact_By_DDD_Erros(string DDD)
    {
        // Arrange
        var mockRepo = new Mock<IContactRepository>();
        //-Configura o repositorio emulando o metodo de retorno com o DDD
        mockRepo.Setup(repo => repo.GetByDdd(It.IsAny<string>()))
            .ReturnsAsync((string predicate) => _contactTestsFixture.GetListContact(predicate));

        // Act
        var result = await mockRepo.Object.GetByDdd(DDD);

        // Assert
        Assert.Empty(result);
    }
}
