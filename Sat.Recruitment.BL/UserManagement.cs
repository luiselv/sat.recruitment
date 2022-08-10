using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sat.Recruitment.DAL;
using Sat.Recruitment.Entities;

namespace Sat.Recruitment.BL
{
    public class UserManagement
    {
        public bool CreateUser(User user)
        {
            decimal money = user.Money;
            try
            {
                switch (user.UserType)
                {
                    case "Normal":
                        if (money > 10 && money <= 100)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = money * percentage;
                            user.Money += gif;
                        }
                        if (money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.12);
                            var gif = money * percentage;
                            user.Money += gif;
                        }
                        break;

                    case "SuperUser":
                        if (money > 100)
                        {
                            var percentage = Convert.ToDecimal(0.20);
                            var gif = money * percentage;
                            user.Money += gif;
                        }
                        break;

                    case "Premium":
                        if (money > 100)
                        {
                            var gif = money * 2;
                            user.Money += gif;
                        }
                        break;
                }
                
                UserDAO.Instance.SetImplementer(FactoryDAO.Instance.ProduceUserDAO());
                return UserDAO.Instance.Create(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public List<User> GetAll()
        {
            UserDAO.Instance.SetImplementer(FactoryDAO.Instance.ProduceUserDAO());
            return UserDAO.Instance.GetAll();
        }

        public bool CheckDuplicated(string email)
        {
            UserDAO.Instance.SetImplementer(FactoryDAO.Instance.ProduceUserDAO());
            return UserDAO.Instance.CheckDuplicated(email);
        }

        //Validate errors
        public Result ValidateErrors(User user)
        {
            Result result = new Result
            {
                IsSuccess = true,
                Messages = ""
            };

            if (user.Name == null)
            {
                result.IsSuccess = false;
                result.Messages += "The name is required";
            }
            else if (user.Email == null)
            {
                result.IsSuccess = false;
                result.Messages += " The email is required";
            }
            else if (user.Address == null)
            {
                result.IsSuccess = false;
                result.Messages += " The address is required";
            }
            else if (user.Phone == null)
            {
                result.IsSuccess = false;
                result.Messages += " The phone is required";
            }
            else if (user.UserType == null)
            {
                result.IsSuccess = false;
                result.Messages += " The user type is required";
            }
            else if (user.Money == 0)
            {
                result.IsSuccess = false;
                result.Messages += " The money is required";

            }
            return result;
        }

        public string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
