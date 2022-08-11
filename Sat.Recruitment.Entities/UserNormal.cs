using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Entities
{
    public class UserNormal : IUserType
    {
        public decimal GetMoneyGift(decimal money)
        {
            if (money > 10 && money <= 100)
            {
                var percentage = Convert.ToDecimal(0.8);
                var gif = money * percentage;
                money += gif;
            }
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                var gif = money * percentage;
                money += gif;
            }
            return money;
        }
    }
}
