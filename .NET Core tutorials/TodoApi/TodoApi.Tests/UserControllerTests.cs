using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Repositories;
using Xunit;

namespace TodoApi.Tests {

    public class UserControllerTests {
        private List<User> GetTestUsers () {
            var users = new List<User> ();
            users.Add (new User () {
                FirstName = "Łukasz",
                    LastName = "Rojek",
                    Login = "lrojek",
                    Id = 1
            });
            users.Add (new User () {
                FirstName = "Jakub",
                    LastName = "Kobylański",
                    Login = "jkobylanski",
                    Id = 2
            });
            return users;
        }

        [Fact]
        public async Task GetTest () {
            var mockRepo = new Mock<IUsersRepository> ();
            mockRepo.Setup (repo => repo.GetUsersList ()).ReturnsAsync (GetTestUsers ());
            var controller = new UsersController (mockRepo.Object);

            var result = await controller.Get ();

            var viewResult = Assert.IsType<ActionResult<List<User>>> (result);
            Assert.Equal (2, 2);
            var model = Assert.IsAssignableFrom<IEnumerable<User>> (viewResult.Value);
            Assert.Equal (2, model.Count ());
        }

        [Theory]
        [InlineData (1)]
        [InlineData (2)]
        [InlineData (3)]
        public async Task GetById (int userId) {
            var mockRepo = new Mock<IUsersRepository> ();
            mockRepo.Setup (repo => repo.GetUserDetails (userId)).ReturnsAsync (GetTestUsers ().FirstOrDefault (n => n.Id == userId));
            var controller = new UsersController (mockRepo.Object);

            var result = await controller.GetByID (userId);

            var viewResult = Assert.IsType<ActionResult<User>> (result);

            Assert.IsAssignableFrom<User> (viewResult.Value);
        }
    }
}