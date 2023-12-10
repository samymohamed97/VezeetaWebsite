using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class CouponCodeDTO
    {
        public char discountcode { get; set; }
        public int discountamount { get; set; }
        public Boolean isactive { get; set; }
        public DateTime expirationedate { get; set; }
    }
}
