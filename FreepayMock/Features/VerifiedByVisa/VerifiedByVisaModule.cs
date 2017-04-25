using System;
using Nancy;

namespace FreepayMock.Features.VerifiedByVisa
{
    public class VerifiedByVisaModule : NancyModule
    {
        public static Random rnd = new Random();
        public VerifiedByVisaModule()
        {
            Get["verified-by-visa"] = _ =>
            {
                var model = new Index.Model();
                model.Amount = Request.Query.amount;
                model.Currency = Request.Query.currency;
                model.Code = rnd.Next(100000, 999999);
                model.Timestamp = DateTime.Now.ToString("yyyy MMMM dd HH:mm:ss");
                return Negotiate.WithView("Index").WithModel(model);
            };

            Post["verified-by-visa"] = parameters =>
            {
                string acceptUrl = Request.Query.acceptUrl;
                string declineUrl = Request.Query.declineUrl;
                int hiddencode = Request.Form.hiddencode;
                int code = Request.Form.code;

                if (hiddencode == code)
                {
                    return Response.AsRedirect(acceptUrl);
                }
                else
                {
                    return Response.AsRedirect(declineUrl);
                }
            };
        }
    }
}