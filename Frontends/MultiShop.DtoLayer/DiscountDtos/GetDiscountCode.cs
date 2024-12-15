using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.DiscountDtos
{
    public class GetDiscountCode
    {
        public int CouponId { get; set; }
        public string Code { get; set; } // kupon kodu
        public int Rate { get; set; } // kupon indirim oranı
        public bool IsActive { get; set; } // aktif mi pasif mi
        public DateTime ValidDate { get; set; } // kupon geçerlilik tarihi
    }
}
