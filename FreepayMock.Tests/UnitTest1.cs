

using System;
using System.Threading.Tasks;
using Freepay;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace FreepayMock.Tests
{
    [TestFixture]
    public class SubscriptionClientTests
    {
        [Test]
        public async Task Test()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new SubscriptionManagerClient();
                await client.AuthorizeSubscriptionAsync(123123, "password", 20000, "orderId", Currency.DKK);
            };
        }
    }

    [TestFixture]
    public class ManagementClientTests
    {
        [Test]
        public async Task ApproveTransactionAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {

                var client = new ManagementClient(server.HttpClient);
                await client.ApproveTransactionAsync(1, "password");
            };
        }

        [Test]
        public async Task CaptureAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.CaptureAsync(1, "password");
            };
        }

        [Test]
        public async Task ChangeCaptureAmountAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.ChangeCaptureAmountAsync(1, "password", 20000);
            };
        }

        [Test]
        public async Task DeleteTransactionAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.DeleteTransactionAsync(1, "password");
            };
        }


        [Test]
        public async Task SetEarliestCaptureAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.SetEarliestCaptureAsync(1, "password", DateTime.Now);
            };
        }

        [Test]
        public async Task QueryTransactionAsync()
        {
            using (TestServer server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.QueryTransactionAsync(1, "password");
            };
        }
        [Test]
        public async Task WithholdForApprovalAsync()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var client = new ManagementClient(server.HttpClient);
                await client.WithholdForApprovalAsync(1, "password");
            };
        }
    }
}
