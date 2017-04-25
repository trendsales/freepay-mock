using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock.Data
{
    [Table("Transactions")]
    public class TransactionRecord
    {
        [Key]
        public int TransactionId { get; set; }
        public int? SubscriptionId { get; set; }
        public int Amount { get; set; }
        public string OrderId { get; set; }
        public int CurrencyAsInt { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Captured { get; set; } 
        public DateTime? CapturedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int AuthorizationAmount { get; set; }
        public int CaptureAmount { get; set; }
        public int CaptureErrorCode { get; set; }
        public string CardType { get; set; }
        public DateTime? DateAuthorized { get; set; }
        public DateTime? DateCaptured { get; set; }
        public DateTime DateEarliestCapture { get; set; }
        public bool IsAwaitingApproval { get; set; }
        public bool IsCaptured { get; set; }
        public int MerchantId { get; set; }
        public string PANHash { get; set; }
        public string SourceIp { get; set; }
    }
}