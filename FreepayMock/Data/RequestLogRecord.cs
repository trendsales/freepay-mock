using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock
{
    [Table("RequestLogs")]

    public class RequestLogRecord
    {
        [Key]
        public int RequestId { get; set; }
    }
}