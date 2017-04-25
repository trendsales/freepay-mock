using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock
{
    [Table("Subscriptions")]
    public class SubscriptionRecord
    {
        [Key]
        public int SubscriptionId { get; set; }
        public int MerchantID { get; set; }
        public int MerchantNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int Currency { get; set; }
        public string OrderID { get; set; }
        public string CardType { get; set; }
        public string SourceIP { get; set; }
        public string PANHash { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Acquirer { get; set; }
    }
}