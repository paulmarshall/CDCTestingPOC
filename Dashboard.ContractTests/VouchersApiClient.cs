using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Vouchers.Contracts.v1;

namespace Dashboard.ContractTests
{
    public class VouchersApiClient
    {
        public string BaseUri { get; }

        public VouchersApiClient(string baseUri = null)
        {
            this.BaseUri = baseUri ?? "http://services/v1/api/vouchers/";
        }
        
        public VouchersByCustomerResponse GetVouchersByCustomerId(Guid customerId)
        {
            string reasonPhrase = null;

            using (var client = new HttpClient { BaseAddress = new Uri(this.BaseUri) })
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/vouchers/{customerId}"))
                {
                    request.Headers.Add("Accept", "application/json");

                    using (var response = client.SendAsync(request).Result)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var status = response.StatusCode;
                        reasonPhrase = response.ReasonPhrase;

                        if (status == HttpStatusCode.OK)
                        {
                            return JsonConvert.DeserializeObject<VouchersByCustomerResponse>(content);
                        }
                    }
                }
            }

            throw new HttpRequestException(reasonPhrase);
        }
    }
}
