using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock.Data
{
    [Table("Cards")]
    public class CardRecord
    {
        [Key]
        public int CardId { get; set; }
        public int SubscriptionId { get; set; }
        public int TransactionId { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CVC { get; set; }
    }
}