using Bogus;
using FIAP.TECH.CORE.APPLICATION.Authentication;

using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Validation;
using FIAP.TECH.TESTS.Setup;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using Xunit;

namespace FIAP.TECH.TESTS.Contacts;

[CollectionDefinition(nameof(ContactFixtureCollection))]
public class ContactFixtureCollection : ICollectionFixture<ContactTestsFixture<Program>> { }

[CollectionDefinition(nameof(ContactFixtureCollection))]
public class LoginTestsFixtureCollection : ICollectionFixture<ContactTestsFixture<ProgramAuth>> { }

public class ContactTestsFixture<TProgram> : IDisposable where TProgram : class
{
    public Contact _contact;
    private readonly Faker _faker;
    private string token;
    public readonly CustomWebApplicationFactory<TProgram> contactAppFactory;
    public HttpClient Client;
    public ContactDto _setContact;
    public ContactDto _setContactInvalid;

    public ContactTestsFixture()
    {
        contactAppFactory = new CustomWebApplicationFactory<TProgram>();

        _faker = new Faker("pt_BR");
        _contact = GetContactSucess();
        Client = contactAppFactory.CreateClient();
        _setContact = GetContactDtoSucess();
        _setContactInvalid = GetContactDtoInvalid();
        token = GenerateLogin().Result;
        Client.AssignToken(token);
    }

    public ContactDto GetContactDtoSucess()
    {
        return new ContactDto
        {
            Name = _faker.Person.UserName,
            DDD = "11",
            Email = _faker.Person.Email,
            PhoneNumber = _faker.Random.Replace("9#######"),
        };
    }

    public ContactDto GetContactDtoInvalid()
    {
        return new ContactDto
        {
            Name = _faker.Person.UserName,
            DDD = "0",
            Email = _faker.Person.Email,
            PhoneNumber = _faker.Random.Replace("9####################"),
        };
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

    public List<Contact> GetListContact(string DDD)
    {
        var ddd = new List<string> { "11", "22", "33" };

        var contacts = new Faker<Contact>()
            .RuleFor(p => p.Name, _faker.Person.UserName)
            .RuleFor(p => p.DDD, f => f.PickRandom(ddd))
            .RuleFor(p => p.Email, _faker.Person.Email)
            .RuleFor(p => p.PhoneNumber, _faker.Random.Replace("9#######"));

        return contacts.Generate(100).Where(x => x.DDD == DDD).ToList();
    }

    public async Task<string> GenerateLogin()
    {
        var authenticateRequest = new AuthenticateRequest { Email = "tester@fiaptest.com.br", Password = "Senha@123" };

        var AuthAppFactory = new CustomWebApplicationFactory<ProgramAuth>();
        var AuthClient = AuthAppFactory.CreateClient();
        var response = await AuthClient.PostAsJsonAsync("/Login", authenticateRequest);
        response.EnsureSuccessStatusCode();
        var strResp = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AuthenticateResponse>(strResp).token;
    }

    public void Dispose()
    {
    }
}


public class AuthenticateResponse
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string token { get; set; }
}

