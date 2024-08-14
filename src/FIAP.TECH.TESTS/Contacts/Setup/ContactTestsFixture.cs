using Bogus;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Validation;

namespace FIAP.TECH.TESTS.Contacts.Setup;

[CollectionDefinition("ContactFixtureCollection")]
public class ContactFixtureCollection : ICollectionFixture<ContactTestsFixture> { }

public class ContactTestsFixture
{
    public Contact _contact;
    private readonly Faker _faker;

    public ContactTestsFixture()
    {
        _faker = new Faker("pt_BR");
        _contact = GetContactSucess();
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
}