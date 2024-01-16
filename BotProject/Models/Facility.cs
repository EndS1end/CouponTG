using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponsGetBot.Models
{
    public class Facility
    {
        [Key]
        public short Id { get; set; }
        public string Title { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
