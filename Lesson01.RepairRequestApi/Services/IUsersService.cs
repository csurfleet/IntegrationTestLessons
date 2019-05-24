using Lesson01.RepairRequestApi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lesson01.RepairRequestApi.Services
{
    public interface IUsersService
    {
        Task<User> Get(int id);
    }

    internal class WebApiUsersService : IUsersService
    {
        private readonly HttpClient _client;

        public WebApiUsersService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("UsersService");
        }

        public async Task<User> Get(int id)
        {
            var response = await _client.GetAsync($"api/users/{id}");

            return response.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync())
                : null;
        }
    }
}
