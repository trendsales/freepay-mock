using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace FreepayMock.Features.PaymentForm
{
    public class PaymentFormModule : NancyModule
    {
        public PaymentFormModule()
        {
            Get["/payment-form"] = _ =>
            {
                return Negotiate.WithView("Index");
            };

            Get["/payment-declined"] = _ =>
            {
                return "Payment was declined";
            };

            Get["/payment-accepted"] = _ =>
            {
                return "Payment was accepted";
            };
        }
    }
}
