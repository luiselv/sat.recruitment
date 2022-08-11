using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Entities
{
    public class UserTypeContext
    {
        private IUserType oUserType;

        public enum UserTypes
        {
            Normal, Super, Premium
        }

        public UserTypeContext()
        {
            this.oUserType = new UserNormal();
        }

        public decimal GetMoney(UserTypes _type, decimal money)
        {
            switch (_type)
            {
                case UserTypes.Normal:
                    this.oUserType = new UserNormal();
                    break;
                case UserTypes.Super:
                    this.oUserType = new UserSuper();
                    break;
                case UserTypes.Premium:
                    this.oUserType = new UserPremium();
                    break;
            }
            return this.oUserType.GetMoneyGift(money);
        }
    }
}
