using NUnit.Framework;
using PactNet;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dashboard.ContractTests
{
    [TestFixture]
    public class VouchersTests
    {
        [Test]
        public void Given_A_Customer_And_Customer_Has_Vouchers_When_I_Request_Vouchers_Then_The_Vouchers_Are_Returned()
        {
            // Arrange
            var customerId = Guid.Parse("D8625547-77CC-4F1B-9720-52621EEE7D36");

            #region Pact Setup

            var executingPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            var pactBuilder = new PactBuilder(new PactConfig {
                LogDir = $"{executingPath}/logs",
                PactDir = $"{executingPath}/pacts"
            })
                .ServiceConsumer("Dashboard")
                .HasPactWith("Vouchers API");

            #endregion

            #region Pact Mock Provider Setup

            var mockProviderService = pactBuilder.MockService(1234);

            mockProviderService.Given($"A customer with id '{customerId}' has 2 vouchers")
                               .UponReceiving($"A GET request for vouchers for the customer with id '{customerId}'")
                               .With(new ProviderServiceRequest
                               {
                                   Method = HttpVerb.Get,
                                   Path = $"/api/vouchers/{customerId}",
                                   Headers = new Dictionary<string, string>
                                   {
                                       { "Accept", "application/json"}
                                   }
                               })
                               .WillRespondWith(new ProviderServiceResponse
                               {
                                   Status = 200,
                                   Headers = new Dictionary<string,string>
                                   {
                                       { "Content-Type", "application/json; charset=utf-8"}
                                   },
                                   Body = new 
                                   {
                                       Vouchers = new []
                                        {
                                           new { Code = "100", Amount = 100.0},
                                           new { Code = "200", Amount = 200.0}
                                        }
                                   }
                               });

            #endregion

            var consumer = new VouchersApiClient("http://localhost:1234");

            // Act
            var result = consumer.GetVouchersByCustomerId(customerId);

            // Assert
            Assert.That(result.Vouchers.Count, Is.EqualTo(2));

            #region Pact Teardown

            mockProviderService.VerifyInteractions();

            pactBuilder.Build();

            #endregion
        }
    }
}
