using FluentAssertions;
using Lesson01.UsersApi.Models;
using Lesson01.UsersApi.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lesson01.Tests.UsersApi
{
    public class GetUserTests
    {
        [Fact]
        public async Task GetUser_Is_Broken()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Lesson01.UsersApi.Startup>();

            using (var server = new TestServer(builder))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("api/users/12");

                response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

                var content = response.Content?.ReadAsStringAsync().Result;

                content.Should().Contain("Some error retrieving user 12, oh no");
            }
        }

        [Fact]
        public async Task GetUser_Calls_UserRepository()
        {
            // TODO: Set up user repository mock

            var builder = new WebHostBuilder()
                .UseStartup<Lesson01.UsersApi.Startup>();

            using (var server = new TestServer(builder))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("api/users/12");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content = response.Content?.ReadAsStringAsync().Result;
                content.Should().NotBeNullOrWhiteSpace();

                var userModel = JsonConvert.DeserializeObject<User>(content);
                userModel.Id.Should().Be(12);
            }

            // TODO: Verify we called the user repository
        }

        #region Cheat

        /*
         [Fact]
        public async Task GetUser_Calls_UserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.Get(12)).Returns(new User(12, new List<UserRole>()));

            var builder = new WebHostBuilder()
                .UseStartup<Lesson01.UsersApi.Startup>()
                .ConfigureTestServices(s =>
                    s.AddTransient(_ => userRepositoryMock.Object));

            using (var server = new TestServer(builder))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("api/users/12");

                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content = response.Content?.ReadAsStringAsync().Result;
                content.Should().NotBeNullOrWhiteSpace();

                var userModel = JsonConvert.DeserializeObject<User>(content);
                userModel.Id.Should().Be(12);
            }

            userRepositoryMock.Verify(m => m.Get(12), Times.Once);
        }
         */

        #endregion
    }
}
