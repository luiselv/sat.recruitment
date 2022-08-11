using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Sat.Recruitment.BL;
using Sat.Recruitment.Entities;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly UserManagement userManagement = new UserManagement();
        private Result result;

        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            try
            {
                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Address = address,
                    Phone = phone,
                    UserType = userType,
                    Money = decimal.Parse(money)
                };

                result = userManagement.ValidateErrors(newUser);
                if (!result.IsSuccess)
                    return result;

                newUser.Email = userManagement.NormalizeEmail(newUser.Email);

                if (!userManagement.CheckDuplicated(newUser.Email))
                {
                    if (userManagement.CreateUser(newUser))
                    {
                        return new Result()
                        {
                            IsSuccess = true,
                            Messages = "User Created"
                        };
                    }
                    else
                    {
                        return new Result()
                        {
                            IsSuccess = false,
                            Messages = "Error while creating user"
                        };
                    }
                }
                else
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        Messages = "The user is duplicated"
                    };
                }
            }
            catch(Exception ex)
            {
                return new Result()
                {
                    IsSuccess = false,
                    Messages = ex.Message
                };
            }
        }
    }
}
