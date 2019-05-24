using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lesson01.Tests.Stack
{
    public class RepairRequestsTests
    {
        [Fact]
        public async Task RepairRequest_QueriesUserRepository()
        {
            var userApiBuilder = new WebHostBuilder()
                .UseStartup<Lesson01.UsersApi.Startup>();

            using (var userApiServer = new TestServer(userApiBuilder))
            using (var userApiClient = userApiServer.CreateClient())
            {
                var clientFactoryMock = new Mock<IHttpClientFactory>();
                clientFactoryMock.Setup(m => m.CreateClient("UsersService")).Returns(userApiClient);

                var repairApiBuilder = new WebHostBuilder()
                    .UseStartup<Lesson01.RepairRequestApi.Startup>()
                    .ConfigureTestServices(s =>
                        s.AddSingleton(_ => clientFactoryMock.Object));

                using (var repairApiServer = new TestServer(repairApiBuilder))
                using (var repairApiClient = repairApiServer.CreateClient())
                {
                    var response = await repairApiClient.PostAsJsonAsync("api/repairs", new
                        RepairRequestApi.Models.RepairRequest(RepairRequestApi.Models.RepairType.BoilerReplacement, 10, "Street address"));

                    if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        throw new Exception(response.Content?.ReadAsStringAsync().Result);
                }
            }
        }
    }
}
