using FluentAssertions;
using GhibliWebAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace APIGhibliTest
{
    public class publicApiTest : IClassFixture<WebApplicationFactory<GhibliWebAPI.Startup>>
    {
        public HttpClient _client { get; }

        public publicApiTest(WebApplicationFactory<GhibliWebAPI.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Return_API_Data()
        {
            var response = await _client.GetAsync("/api/Films");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var film = JsonConvert.DeserializeObject<Film[]>(await response.Content.ReadAsStringAsync());
            film.Should().HaveCount(20);
        }




    }
}
