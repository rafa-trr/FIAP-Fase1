using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FIAP.TECH.TESTS.Contacts.Setup;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
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


    //[Fact(DisplayName = "Validando a entrada de dados com erros esperados ao alterar um contato")]
    //[Trait("Contato", "Validando Entrada de dados")]
    //public async Task GetTweetDay_ReturnsOkResult_WithATweetObject()
    //{
    //    IMapper _mapper;
    //    IRegionRepository _regionRepository;
    //    IContactRepository _contactRepository

    //    // Arrange
    //    var mockRepo = new Mock<IRegionRepository>();
    //    var mockRepo = new Mock<IRegionRepository>();

    //    var mockRepo = new Mock<IRegionRepository>();

    //    mockRepo.Setup(repo => repo.GetByDdd(""))
    //        .ReturnsAsync(GetTestTweet(""));

    //    var controller = new ContactService(mockRepo.Object);

    //    // Act
    //    var result = await controller.GetByDdd("11");

    //    // Assert
    //    var okResult = Assert.IsType<OkObjectResult>(result);
    //}

    //private List<ContactDetailsDto> GetTestTweet(string DDD)
    //{
    //    List<ContactDetailsDto> contacts = new List<ContactDetailsDto>();
    //    contacts.Add(new ContactDetailsDto
    //    {
    //        Name = "Timotio",
    //        DDD = "11",
    //        Email = "_faker.Person.Email",
    //        PhoneNumber = "_faker.Random.Replace("
    //    });

    //    return contacts.Where(x=>x.DDD == DDD).ToList();
    //}
}
