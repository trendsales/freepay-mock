using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock.Data
{
    [Table("SubscriptionLogs")]
    public class SubscriptionLogRecord
    {
        [Key]
        public int SubscpritionLogId { get; set; }
        public int RequestId { get; set; }
    }
}