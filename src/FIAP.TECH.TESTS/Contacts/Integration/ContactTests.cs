using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.TESTS.Setup.Priority;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Xunit;


namespace FIAP.TECH.TESTS.Contacts.Integration
{
    [TestCaseOrderer("FIAP.TECH.TESTS.Setup.Priority.PriorityOrderer", "FIAP.TECH.TESTS")]
    [Collection(nameof(ContactFixtureCollection))]
    public class ContactTests
    {
        private readonly ContactTestsFixture<Program> _contactTestsFixture;


        public ContactTests(ContactTestsFixture<Program> contactTestsFixture)
        {
            _contactTestsFixture = contactTestsFixture;
        }

        #region Valido

        [Fact(DisplayName = "INTEGRACAO Criar Novo Contato"), TestPriority(1)]
        [Trait("Integração - ContactTests", "CRUD")]
        public async Task Post_Create_Valid_Contact()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _contactTestsFixture.Client.PostAsJsonAsync("/Contact", _contactTestsFixture._setContact);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "INTEGRACAO Deletar um Novo Contato"), TestPriority(2)]
        [Trait("Integração - ContactTests", "CRUD")]
        public async Task Get_All_Valid_Contact()
        {
            // Arrange

            // Act
            var response = await _contactTestsFixture.Client.GetAsync($"/Contact");

            // Assert
            response.EnsureSuccessStatusCode();
            var strResp = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<List<ContactDto>>(strResp);
            var result = jsonResponse.Where(x => x.Name == _contactTestsFixture._setContact.Name).FirstOrDefault();
            Assert.NotNull(result);
            _contactTestsFixture._setContact.Id = result.Id;
        }

        [Fact(DisplayName = "INTEGRACAO Editar um Novo Contato"), TestPriority(3)]
        [Trait("Integração - ContactTests", "CRUD")]
        public async Task Put_Edit_Valid_Contact()
        {
            // Arrange
            _contactTestsFixture._setContact.Email = "newEmail@teste.com";

            // Act
            var response = await _contactTestsFixture.Client.PutAsJsonAsync($"/Contact/{_contactTestsFixture._setContact.Id}", _contactTestsFixture._setContact);

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact(DisplayName = "INTEGRACAO Deletar um Novo Contato"), TestPriority(4)]
        [Trait("Integração - ContactTests", "CRUD")]
        public async Task Delete_Valid_Contact()
        {
            // Arrange

            // Act
            var response = await _contactTestsFixture.Client.DeleteAsync($"/Contact/{_contactTestsFixture._setContact.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
        #endregion

        [Fact(DisplayName = "INTEGRACAO Erro Esperado Criar Novo Contato"), TestPriority(5)]
        [Trait("Erro Esperado Integração - ContactTests", "CRUD")]
        public async Task Post_Create_InValid_Contact()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _contactTestsFixture.Client.PostAsJsonAsync("/Contact", _contactTestsFixture._setContactInvalid);

            // Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact(DisplayName = "INTEGRACAO Erro Esperado Deletar um Novo Contato"), TestPriority(6)]
        [Trait("Erro Esperado Integração - ContactTests", "CRUD")]
        public async Task Get_All_InValid_Contact()
        {
            // Arrange

            // Act
            var response = await _contactTestsFixture.Client.GetAsync($"/Contact");

            // Assert
            response.EnsureSuccessStatusCode();
            var strResp = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<List<ContactDto>>(strResp);
            var result = jsonResponse.Where(x => x.Name == _contactTestsFixture._setContactInvalid.Name).FirstOrDefault();
            Assert.Null(result);
        }

        [Fact(DisplayName = "INTEGRACAO Erro Esperado Editar um Novo Contato"), TestPriority(7)]
        [Trait("Erro Esperado Integração - ContactTests", "CRUD")]
        public async Task Put_Edit_InValid_Contact()
        {
            // Arrange
            _contactTestsFixture._setContact.Email = "newEmail@teste.com";

            // Act
            var response = await _contactTestsFixture.Client.PutAsJsonAsync($"/Contact/{_contactTestsFixture._setContactInvalid.Id}", _contactTestsFixture._setContactInvalid);

            // Assert
            Assert.False(response.IsSuccessStatusCode);
        }
        [Fact(DisplayName = "INTEGRACAO Erro Esperado Deletar um Novo Contato"), TestPriority(8)]
        [Trait("Erro Esperado Integração - ContactTests", "CRUD")]
        public async Task Delete_InValid_Contact()
        {
            // Arrange

            // Act
            var response = await _contactTestsFixture.Client.DeleteAsync($"/Contact/{_contactTestsFixture._setContactInvalid.Id}");

            // Assert
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}