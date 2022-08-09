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

        public bool CheckDuplicated(User newUser)
        {
            UserDAO.Instance.SetImplementer(FactoryDAO.Instance.ProduceUserDAO());
            List<User> list = UserDAO.Instance.GetAll();
            foreach (var user in list)
            {
                if (user.Email == newUser.Email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
