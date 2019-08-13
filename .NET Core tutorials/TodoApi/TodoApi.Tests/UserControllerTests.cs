using System;
using System.Collections.Generic;
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
            var controller = new UsersController(mockRepo.Object);
            
            var result = await controller.Get();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<object>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.);
        }
    }
}