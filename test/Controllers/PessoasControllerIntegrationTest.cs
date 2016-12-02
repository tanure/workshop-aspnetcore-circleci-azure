using System.Collections.Generic;
using System.Threading.Tasks;
using aspnetcore;
using Newtonsoft.Json;
using Xunit;

namespace test.Controllers
{
    public class PessoasControllerIntegrationTest : BaseIntegrationTest
    {
        private const string  BASE_URL = "/api/pessoas";

        public PessoasControllerIntegrationTest(BaseTestFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task DeveRetornarListaDePessoasVazia()
        {
            var response = await Client.GetAsync(BASE_URL);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 0);
        }

        [Fact]
        public async Task DeveRetornarListaDePessoas()
        {
            var pessoa = new Pessoa
            {
                Nome = "Albert Tanure",
                Twitter = "@alberttanure"
            };

            await TestDataContext.AddAsync(pessoa);
            await TestDataContext.SaveChangesAsync();

            var response =  await Client.GetAsync(BASE_URL);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Pessoa>>(responseString);

            Assert.Equal(data.Count, 1);
            Assert.Contains(data, x => x.Nome == pessoa.Nome);

        }
    }
}