using System;
using System.Linq;
using Freepay;
using FreepayMock.Data;
using Nancy;

namespace FreepayMock.Features.WebServices
{
    public class WebServicesModule : NancyModule
    {
        public WebServicesModule(FreepayMockContext db)
        {
            Get["/webservices/public/subscriptionmanager.asmx/AuthorizeSubscription3"] = parameters =>
            {
                int subscriptionId = parameters.subscriptionId;
                string password = parameters.password;
                int amount = parameters.amount;
                string orderId = parameters.orderId;
                int currencyAsInt = parameters.currencyAsInt;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var subscription = db.Subscriptions.FirstOrDefault(x => x.SubscriptionId == subscriptionId);

                if (subscription == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = new TransactionRecord();
                transaction.SubscriptionId = subscriptionId;
                transaction.Amount = amount;
                transaction.OrderId = orderId;
                transaction.CurrencyAsInt = currencyAsInt;
                transaction.DateCreated = DateTime.Now;
                return "OK";
            };

            Get["/webservices/public/subscriptionmanager.asmx/QuerySubscription"] = parameters =>
            {
                int subscriptionId = parameters.subscriptionId;
                string password = parameters.password;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var subscription = db.Subscriptions.FirstOrDefault(x => x.SubscriptionId == subscriptionId);

                if (subscription == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var model = new SubscriptionView();
                model.Acquirer = "NetsTeller";
                model.CardType = subscription.CardType;
                model.Currency = subscription.Currency;
                model.DateCreated = subscription.DateCreated;
                model.ExpiryDate = subscription.ExpiryDate;
                model.MerchantID = merchant.MerchantId;
                model.MerchantNumber = merchant.MerchantId;
                model.OrderID = model.OrderID;
                model.PANHash = "";
                model.SourceIP = subscription.SourceIP;
                model.SubscriptionID = subscriptionId;
                return Response.AsXml(model);
            };


            //public static string GetApproveTransactionUrl(int transactionId, string password)
            Get["/webservices/public/management.asmx/ApproveTransaction"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;


                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.IsAwaitingApproval = true;
                db.Transactions.Attach(transaction);
                db.SaveChanges();

                return HttpStatusCode.OK;
            };


            //public static string GetCaptureUrl(int transactionId, string password)
            Get["/webservices/public/management.asmx/CaptureV2"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;


                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.Captured = true;
                transaction.CapturedAt = DateTime.Now;
                db.Transactions.Attach(transaction);
                db.SaveChanges();

                var model = new TransactionResult();
                model.AcquirerStatusCode = 123;
                model.IsSuccess = true;

                return Response.AsXml(model);
            };

            //public static string GetChangeCaptureAmountUrl(int transactionId, string password, int amount)
            Get["/webservices/public/management.asmx/ChangeCaptureAmount"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;
                int amount = parameters.amount;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.Amount = amount;
                db.Transactions.Attach(transaction);
                db.SaveChanges();

                return HttpStatusCode.OK;
            };

            //public static string GetCreditUrl(int transactionId, string password, int amount)
            Get["/webservices/public/management.asmx/CreditV2"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;
                int amount = parameters.amount;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.Amount = amount;
                db.Transactions.Attach(transaction);
                db.SaveChanges();

                TransactionResult result = new TransactionResult();
                result.AcquirerStatusCode = 100;
                result.IsSuccess = true;

                return Response.AsXml(result);
            };

            //public static string GetDeleteTransactionUrl(int transactionId, string password)
            Get["/webservices/public/management.asmx/DeleteTransaction"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;
                

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.IsDeleted = true;
                db.Transactions.Attach(transaction);
                db.SaveChanges();

                return HttpStatusCode.OK;
            };

            //public static string GetQueryTransactionUrl(int transactionId, string password)
            Get["/webservices/public/management.asmx/QueryTransaction"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                TransactionView model = new TransactionView();
                model.Acquirer = "NetsTeller";
                model.MerchantNumber = merchant.MerchantId;
                model.TransactionID = transaction.TransactionId;
                model.AuthorizationAmount = transaction.AuthorizationAmount;
                model.CaptureAmount = transaction.CaptureAmount;
                model.CaptureErrorCode = transaction.CaptureErrorCode;
                model.CardType = transaction.CardType;
                model.Currency = transaction.CurrencyAsInt;
                model.DateAuthorized = transaction.DateAuthorized.GetValueOrDefault();
                model.DateCaptured = transaction.DateCaptured.GetValueOrDefault();
                model.DateCreated = transaction.DateCreated;
                model.DateEarliestCapture = transaction.DateEarliestCapture;
                model.IsAwaitingApproval = transaction.IsAwaitingApproval;
                model.IsCaptured = transaction.IsCaptured;
                model.MerchantID = transaction.MerchantId;
                model.OrderID = transaction.OrderId;
                model.PANHash = transaction.PANHash;
                model.SourceIP = transaction.SourceIp;
                return "OK";
            };

            //public static string GetSetEarlistCaptureUrl(int transactionId, string password, DateTime earliestCapture)
            Get["/webservices/public/management.asmx/SetEarliestCapture"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                DateTime earliestCapture = parameters.earliestCapture;
                string password = parameters.password;

                var merchant = db.Merchants.FirstOrDefault(x => x.Password == password);

                if (merchant == null)
                {
                    return HttpStatusCode.InternalServerError;
                }

                var transaction = db.Transactions.FirstOrDefault(x => x.TransactionId == transactionId);

                if (transaction == null)
                {
                    return HttpStatusCode.NotFound;
                }

                transaction.DateEarliestCapture = earliestCapture;

                return HttpStatusCode.OK;
            };

            //public static string GetWithholdForApprovalUrl(int transactionId, string password)
            Get["/webservices/public/management.asmx/WithholdForApproval"] = parameters =>
            {
                int transactionId = parameters.transactionId;
                string password = parameters.password;



                return "Ok";
            };
        }
    }
}
