using System;
using System.Linq;
using FreepayMock.Data;
using Nancy;

namespace FreepayMock.Features.Authorize
{
    public class AuthorizeModule : NancyModule
    {
        public AuthorizeModule(FreepayMockContext db)
        {
            Post["/secure/authorize.aspx"] = parameters =>
            {
                string acceptUrl = Request.Form.AcceptURL;
                string declineUrl = Request.Form.DeclineURL;
                int subscription = Request.Form.Subscription;
                int merchantNumber = Request.Form.MerchantNumber;
                int amount = Request.Form.Amount;
                int currency = Request.Form.Currency;
                string orderNumber = Request.Form.OrderNumber;
                string cardNumber = Request.Form.CardNumber;
                string expireMonth = Request.Form.ExpireMonth;
                string expireYear = Request.Form.ExpireYear;
                string cvc = Request.Form.cvc;

                var merchant = db.Merchants.FirstOrDefault(x => x.MerchantId == merchantNumber);

                if (merchant == null)
                {
                    // TODO Log this error
                    return HttpStatusCode.InternalServerError;
                }

                var card = new CardRecord();
                card.CardNumber = cardNumber;
                card.ExpireMonth = expireMonth;
                card.ExpireYear = expireYear;
                card.CVC = cvc;

                if (subscription == 1)
                {
                    SubscriptionRecord record = new SubscriptionRecord();
                    record.MerchantNumber = merchantNumber;
                    record.Currency = currency;

                    // TODO Detect card
                    record.CardType = "CARD";
                    record.Currency = currency;
                    record.DateCreated = DateTime.Now;
                    record.Acquirer = "NetsTeller";
                    record.ExpiryDate = DateTime.Now.AddDays(30);
                    record.SourceIP = Request.UserHostAddress;
                    record.OrderID = orderNumber;

                    db.Subscriptions.Add(record);
                    db.SaveChanges();

                    card.SubscriptionId = record.SubscriptionId;

                    var transaction = new TransactionRecord();
                    transaction.Amount = amount;
                    transaction.CurrencyAsInt = currency;
                    transaction.DateEarliestCapture = DateTime.Now;
                    transaction.OrderId = orderNumber;
                    transaction.AuthorizationAmount = amount;
                    transaction.CaptureAmount = amount;
                    transaction.DateCreated = DateTime.Now;
                    transaction.DateAuthorized = DateTime.Now;
                    transaction.IsAwaitingApproval = true;
                    transaction.MerchantId = merchantNumber;
                    transaction.SubscriptionId = record.SubscriptionId;
                    transaction.IsCaptured = false;
                    transaction.IsDeleted = false;
                    transaction.PANHash = "";
                    transaction.SourceIp = Request.UserHostAddress;

                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    card.TransactionId = transaction.TransactionId;
                    card.TransactionId = transaction.TransactionId;
                    db.Cards.Add(card);
                    db.SaveChanges();
                }
                else
                {
                    var transaction = new TransactionRecord();
                    transaction.Amount = amount;
                    transaction.CurrencyAsInt = currency;
                    transaction.DateEarliestCapture = DateTime.Now;
                    transaction.DateCreated = DateTime.Now; ;
                    transaction.OrderId = orderNumber;
                    transaction.AuthorizationAmount = amount;
                    transaction.CaptureAmount = amount;
                    transaction.DateAuthorized = DateTime.Now;
                    transaction.IsAwaitingApproval = true;
                    transaction.MerchantId = merchantNumber;
                    transaction.IsCaptured = false;
                    transaction.IsDeleted = false;
                    transaction.PANHash = "";
                    transaction.SourceIp = Request.UserHostAddress;
                    db.Transactions.Add(transaction);
                    db.SaveChanges();

                    card.TransactionId = transaction.TransactionId;
                    db.Cards.Add(card);
                    db.SaveChanges();
                }

                if (subscription == 1 || (currency == 208 && amount >= 45000))
                {
                    var verifyUrl = $"/verified-by-visa?amount={amount}&currency={currency}&acceptUrl={acceptUrl}&declineUrl={declineUrl}";
                    return Response.AsRedirect(verifyUrl);
                }
                else
                {
                    return Response.AsRedirect(acceptUrl);
                }
            };
        }



    }
}
