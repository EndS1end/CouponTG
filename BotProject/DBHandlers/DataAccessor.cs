using CouponsGetBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponsGetBot.DBHandlers
{
    public class DataAccessor
    {
        private readonly BotDBContext _dbContext;

        public DataAccessor(BotDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void EnsureDatabaseCreated()
        {
            _dbContext.Database.EnsureCreated();
        }

        public void InitFacilities()
        {
            var initKFC = new Facility
            {
                Id = 1,
                Title = "kfc",
                LastUpdate = DateTime.UtcNow
            };

            AddFacility(initKFC);

            var burgerKing = new Facility
            {
                Id = 2,
                Title = "burger king",
                LastUpdate = DateTime.UtcNow
            };

            AddFacility(burgerKing);

            var vkusnoITochka = new Facility
            {
                Id = 3,
                Title = "vkusno i tochka",
                LastUpdate = DateTime.UtcNow
            };

            AddFacility(vkusnoITochka);
        }

        public void AddCoupon(Coupon coupon)
        {
            _dbContext.Coupons.Add(coupon);
            _dbContext.SaveChanges();
        }

        public IQueryable<Coupon> GetAllCoupons()
        {
            return _dbContext.Coupons;
        }

        public void DeleteAllCoupons()
        {
            var allCoupons = _dbContext.Coupons.ToList();

            if (allCoupons.Any())
            {
                _dbContext.Coupons.RemoveRange(allCoupons);
                _dbContext.SaveChanges();
            }
        }

        public void AddFacility(Facility facility)
        {
            if (GetFacilityById(facility.Id) == null)
            {
                _dbContext.Facilities.Add(facility);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Facility> GetAllFacilities()
        {
            return _dbContext.Facilities;
        }

        public Facility GetFacilityById(short facilityId)
        {
            return _dbContext.Facilities.FirstOrDefault(f => f.Id == facilityId);
        }
    }

}
