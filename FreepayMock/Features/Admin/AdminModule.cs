using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace FreepayMock.Features.Admin
{
    public class AdminModule : NancyModule
    {
        public AdminModule(FreepayMockContext db) : base("/admin")
        {
            Get["/"] = _ =>
            {
                var recentSubscriptions = 
                    db.Subscriptions
                        .Take(10)
                        .OrderByDescending(x => x.SubscriptionId)
                        .ToList();

                var recentTransactions =
                    db.Transactions
                        .Take(10)
                        .OrderByDescending(x => x.SubscriptionId)
                        .ToList();


                return "OK";
            };

            Get["/transactions"] = _ =>
            {

                return "OK";
            };

            Get["/subscriptions"] = _ =>
            {

                return "OK";
            };

            Get["/logs"] = _ =>
            {

                return "OK";
            };
        }
    }
}
