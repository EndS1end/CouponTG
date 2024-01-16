using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponsGetBot.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public int FacilityType { get; set; }
        public float? Price { get; set; }
        public float? OldPrice { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }
    }
}
