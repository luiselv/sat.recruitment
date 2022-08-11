using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Entities
{
    public class UserSuper : IUserType
    {
        public decimal GetMoneyGift(decimal money)
        {
            if (money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = money * percentage;
                money += gif;
            }
            return money;
        }
    }
}
