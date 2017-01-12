using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vouchers.Contracts.v1
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}
