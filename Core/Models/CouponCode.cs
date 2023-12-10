using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
   public class CouponCode
    {
        public int Id { get; set; }
        public char DiscountCode { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Boolean IsActive {  get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
