using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sat.Recruitment.DAL.File;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.DAL
{
    public class FactoryDAO
    {
        #region Instancia
        private static FactoryDAO objInstance = null;
        private FactoryDAO()
        {
        }
        public static FactoryDAO Instance
        {
            get
            {
                if (objInstance == null)
                    objInstance = new FactoryDAO();

                return objInstance;
            }
        }
        #endregion

        public IUserDAO ProduceUserDAO()
        {
            IUserDAO userDAO = null;
            int intType = 1; // 1:File source   2:Other data source
            switch (intType)
            {
                case 1:
                    userDAO = UserFileDAO.Instance;
                    break;
                case 2:
                    //code for other data source
                    break;
            }
            return userDAO;
        }

    }
}
