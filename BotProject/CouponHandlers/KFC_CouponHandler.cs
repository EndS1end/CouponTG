using CouponsGetBot.DBHandlers;
using CouponsGetBot.Models;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CouponsGetBot.CouponHandlers
{
    public class KFC_CouponHandler : ICouponHandler
    {
        public const string URL = "https://www.kfc.ru/couponS";
        public string GetTextMessage(DataAccessor dataAccessor)
        {
            List<Coupon> couponsToShow = GetCouponsWithCheck(dataAccessor, 1);
            string result = "";

            foreach(var coupon in couponsToShow)
            {
                result += $"{coupon.Code} ({coupon.Price} вместо {coupon.OldPrice}) - {coupon.Text} \n";
            }

            return result;
        }

        public List<Coupon> GetCouponsWithCheck(DataAccessor accessor, short facilityId)
        {
            if(accessor.GetFacilityById(facilityId).LastUpdate > DateTime.UtcNow.AddDays(1) || (accessor.GetAllCoupons().Count() < 2))
            {
                accessor.DeleteAllCoupons();

                List<Coupon> coupons = ParseCoupons();

                foreach(var coupon in coupons)
                {
                    accessor.AddCoupon(coupon);
                }

                return coupons;
            }
            else
            {
                return accessor.GetAllCoupons().ToList<Coupon>();
            }
        }

        static string ExtractImageUrl(string input)
        {
            int startIndex = input.IndexOf("&quot;");
            if (startIndex != -1)
            {
                // Находим второе вхождение &quot; начиная с позиции после первого
                int endIndex = input.IndexOf("&quot;", startIndex + 1);

                if (endIndex != -1)
                {
                    // Вырезаем подстроку между первым и вторым вхождением &quot;
                    string result = input.Substring(startIndex + 6, endIndex - startIndex - 6);
                    return result;
                }
            }

            return null;
        }

        public List<Coupon> ParseCoupons()
        {
            List<Coupon> coupons = new List<Coupon>();

            var web = new HtmlWeb();
            var doc = web.Load(URL);

            var couponNodes = doc.DocumentNode.SelectNodes("//a[@class='RTQ7M5RnH1t']");

            if (couponNodes != null)
            {
                foreach (var node in couponNodes)
                {
                    var coupon = new Coupon();

                    coupon.Id = 0;
                    coupon.FacilityType = 0;

                    var priceNodeGlobal = node.SelectSingleNode(".//div[@class='_1trEHSCHMhK']");
                    if (priceNodeGlobal != null)
                    {
                        var priceNode = priceNodeGlobal.SelectNodes(".//span");
                        if (priceNode != null)
                        {
                            coupon.Price = float.Parse(priceNode[0].InnerText);
                            coupon.OldPrice = float.Parse(priceNode[3].InnerText.Substring(0, priceNode[3].InnerText.Length - 2));
                        }
                    }
                    else continue;

                    coupon.Code = node.SelectSingleNode(".//div[@class='_2pr76I4WPmJ']").InnerText;

                    coupon.Text = node.SelectSingleNode(".//div[@class='_3POebZQSBG9']").InnerText;

                    var imageNode = node.SelectSingleNode(".//div");
                    if(imageNode != null)
                    {
                        coupon.ImageSource = "https://avatars.mds.yandex.net/get-altay/4012790/2a000001794afd7ff343b181c3fa615a89c9/XXL_height"; //ExtractImageUrl(imageNode.GetAttributeValue("style", ""));
                    }

                    coupons.Add(coupon);
                }
            }

            return coupons;
        }
    }
}
