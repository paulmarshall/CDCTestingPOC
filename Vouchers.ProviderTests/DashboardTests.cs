using Microsoft.Owin.Testing;
using NUnit.Framework;
using PactNet;
using System;
using System.IO;
using Vouchers.API;

namespace Vouchers.ProviderTests
{
    [TestFixture]
    public class DashboardTests
    {
        [Test]
        public void Ensure_Provider_Conforms_To_Contract_With_Dashboard()
        {
            // Arrange
            var executingPath = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            IPactVerifier pactVerifier = new PactVerifier(()=> { },()=>{ }, new PactVerifierConfig
            {
                LogDir = $"{executingPath}/logs",
            });

            pactVerifier.ProviderState(
                providerState: "A customer with id 'd8625547-77cc-4f1b-9720-52621eee7d36' has 2 vouchers",
                setUp: () => { },
                tearDown: () => { });

            // Act + Assert
            using (var testServer = TestServer.Create<Startup>()) //NOTE: This is using the Microsoft.Owin.Testing nuget package
            {
                Uri pactFile = new Uri(new Uri(executingPath), "../../../CDCTestingPOC/Dashboard.ContractTests/bin/Debug/pacts/dashboard-vouchers_api.json");

                pactVerifier
                    .ServiceProvider("Vouchers API", testServer.HttpClient)
                    .HonoursPactWith("Dashboard")
                    .PactUri(pactFile.LocalPath)
                    .Verify(
                        description: "A GET request for vouchers for the customer with id 'd8625547-77cc-4f1b-9720-52621eee7d36'",
                        providerState: "A customer with id 'd8625547-77cc-4f1b-9720-52621eee7d36' has 2 vouchers");
            }
        }
    }
}
