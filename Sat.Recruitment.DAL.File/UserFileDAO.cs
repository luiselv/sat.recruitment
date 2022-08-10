using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sat.Recruitment.Entities;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.DAL.File
{
    public class UserFileDAO : IUserDAO
    {
        #region Instance
        private static UserFileDAO objInstance = null;
        private UserFileDAO()
        {
        }
        public static UserFileDAO Instance
        {
            get
            {
                if (objInstance == null)
                    objInstance = new UserFileDAO();

                return objInstance;
            }
        }
        #endregion

        public bool Create(User user)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
                string text = user.Name + "," + user.Email + "," + user.Phone + "," + user.Address + "," + user.UserType + "," + user.Money.ToString();
                using(StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine("\n" + text);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                list.Add(user);
            }
            reader.Close();
            return list;
        }

        public bool CheckDuplicated(string email)
        {
            List<User> list = this.GetAll();

            return list.Select(x => x.Email.Equals(email)).Contains(true);
        }

    }
}
