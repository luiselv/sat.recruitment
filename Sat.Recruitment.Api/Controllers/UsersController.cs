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
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Messages { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly UserManagement mgt = new UserManagement();
        private List<User> _users = new List<User>();
        private Result result;

        public UsersController()
        {
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            result = ValidateErrors(name, email, address, phone, userType, money);

            if (!result.IsSuccess)
                return result;

            var newUser = new User
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = decimal.Parse(money)
            };

            //Normalize email
            newUser.Email = Util.NormalizeEmail(newUser.Email);

            _users = mgt.GetAll();          

            try
            {
                if (!mgt.CheckDuplicated(newUser))
                {
                    if (mgt.CreateUser(newUser))
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

        //Validate errors
        private Result ValidateErrors(string name, string email, string address, string phone, string userType, string money)
        {
            Result result = new Result
            {
                IsSuccess = true,
                Messages = ""
            };

            if (name == null)
            {
                result.IsSuccess = false;
                result.Messages += "The name is required";
            }
            else if (email == null)
            {
                result.IsSuccess = false;
                result.Messages += "The email is required";
            }
            else if (address == null)
            {
                result.IsSuccess = false;
                result.Messages += "The address is required";
            }
            else if (phone == null)
            {
                result.IsSuccess = false;
                result.Messages += "The phone is required";
            }
            else if (userType == null)
            {
                result.IsSuccess = false;
                result.Messages += "The user type is required";
            }
            else if (money == null)
            {
                result.IsSuccess = false;
                result.Messages += "The money is required";

            }
            return result;
        }
    }
}
