using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Entities
{
    public class UserPremium : IUserType
    {
        public decimal GetMoneyGift(decimal money)
        {
            if (money > 100)
            {
                var gif = money * 2;
                money += gif;
            }
            return money;
        }
    }
}
