using Application.UseCases;
using Application.UseCases.User.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Application.UseCases
{
    [TestClass]
    public class CreateUserIntegrationTest : Testing
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public async Task CreateUser()
        {
            var command = new CreateUserCommand
            {
                Name = "Test",
                Email = "test@test.com",
                Password = "password",
                Username = "test"
            };

            await SendAsync(command);
            
            var query = new GetPokemonByEmailQuery
            {
                Email = command.Email
            };

            var user = await SendAsync(query);
            Assert.IsNotNull(user);
            Assert.AreEqual(command.Name, user?.Name);
            Assert.AreEqual(command.Email, user?.Email);
            Assert.AreEqual(command.Password, user?.Password);
            Assert.AreEqual(command.Username, user?.Username);
        }
    }
}
