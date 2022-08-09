using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sat.Recruitment.Entities;

namespace Sat.Recruitment.Interfaces
{
    public interface IUserDAO
    {
        bool Create(User user);
        List<User> GetAll();
    }
}
