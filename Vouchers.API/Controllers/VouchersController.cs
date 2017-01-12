using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Vouchers.Contracts.v1;

namespace Vouchers.API.Controllers
{
    [RoutePrefix("api/vouchers")]
    public class VouchersController : ApiController
    {
        [HttpGet]
        [Route("{customerId}")]
        public VouchersByCustomerResponse GetVouchersByCustomerId(string customerId)
        {
            return new VouchersByCustomerResponse
            {
                Vouchers = new List<Voucher>
                            {
                                new Voucher { Code = "100", Amount = 100.00M},
                                new Voucher { Code = "200", Amount = 200.00M}
                            }.ToArray()
            };
        }
    }
}
