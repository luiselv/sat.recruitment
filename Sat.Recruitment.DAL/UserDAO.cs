using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sat.Recruitment.Entities;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.DAL
{
    public class UserDAO
    {
        #region Instance
        private static UserDAO objInstance = null;
        private UserDAO()
        {
        }
        public static UserDAO Instance
        {
            get
            {
                if (objInstance == null)
                    objInstance = new UserDAO();

                return objInstance;
            }
        }
        #endregion

        IUserDAO implementer;
        public void SetImplementer(IUserDAO userDao)
        {
            implementer = userDao;
        }

        public bool Create(User user)
        {
            return implementer.Create(user);
        }

        public List<User> GetAll()
        {
            return implementer.GetAll();
        }
    }
}
