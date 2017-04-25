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
