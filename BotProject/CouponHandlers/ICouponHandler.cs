using CouponsGetBot.DBHandlers;
using CouponsGetBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace CouponsGetBot.CouponHandlers
{
    public interface ICouponHandler
    {
        string GetTextMessage(DataAccessor dbContext);

        List<Coupon> ParseCoupons();
    }
}
