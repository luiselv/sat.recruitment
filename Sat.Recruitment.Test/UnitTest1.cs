using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void CreateUser_isSuccess()
        {
            var userController = new UsersController();
            var result = userController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124").Result;

            Assert.True(result.IsSuccess);
            Assert.Contains(result.Messages, "User Created");
        }

        [Fact]
        public void CreateUser_isDuplicated()
        {
            var userController = new UsersController();
            var result = userController.CreateUser("Juan", "Juan@marmol.com", "Av. Juan G", "+5491154762312", "Normal", "1234").Result;

            Assert.False(result.IsSuccess);
            Assert.Contains(result.Messages, "The user is duplicated");
        }
    }
}
