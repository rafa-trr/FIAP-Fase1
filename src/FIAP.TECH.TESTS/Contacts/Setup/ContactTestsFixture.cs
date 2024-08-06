using AutoMapper;
using Bogus;
using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Validation;
using Moq;

namespace FIAP.TECH.TESTS.Contacts.Setup;

[CollectionDefinition("ContactFixtureCollection")]
public class ContactFixtureCollection : ICollectionFixture<ContactTestsFixture> { }

public class ContactTestsFixture
{
    public Contact _contact;
    public ContactService contactService;
    private readonly Faker _faker;

    public ContactTestsFixture()
    {
        _faker = new Faker("pt_BR");
        _contact = GetContactSucess();
        contactService = SetConfigService();
    }

    public Contact GetContactSucess()
    {
        return new Contact
        {
            Name = _faker.Person.UserName,
            DDD = "11",
            Email = _faker.Person.Email,
            PhoneNumber = _faker.Random.Replace("9#######"),
        };
    }

    public Contact GetContactErrors()
    {
        return new Contact
        {
            Name = string.Empty,
            DDD = "090",
            Email = _faker.Person.UserName,
            PhoneNumber = _faker.Random.Replace("119#######"),
        };
    }

    public Contact GetContactForUpdateSucess()
    {
        return new Contact
        {
            Id = 1,
            Name = _faker.Person.UserName,
            DDD = "13",
            Email = _faker.Person.Email,
            PhoneNumber = _faker.Random.Replace("9#######"),
        };
    }

    public Contact GetContactForUpdateErrors()
    {
        return new Contact
        {
            Id = 0,
            Name = _faker.Person.UserName,
            DDD = "13",
            Email = _faker.Person.Email,
            PhoneNumber = _faker.Random.Replace("9#######"),
        };
    }

    public ContactInsertValidation GetValidationInsert() => new();

    public ContactUpdateValidation GetValidationUpdate() => new();

    public ContactService SetConfigService()
    {
        var mapper = new Mock<IMapper>();
        var regionRepository = new Mock<IRegionRepository>();
        var contactRepository = new Mock<IContactRepository>();

        return new ContactService(mapper.Object, regionRepository.Object, contactRepository.Object);
    }
}

public static class TestsExtensions
{
    public static void AssignToken(this HttpClient httpClient, string token)
    {
        httpClient.AssignJsonMediaType();
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public static void AssignJsonMediaType(this HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }
}
