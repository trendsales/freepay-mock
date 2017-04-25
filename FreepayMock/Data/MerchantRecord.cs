using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock
{
    [Table("Merchants")]
    public class MerchantRecord
    {
        [Key]
        public int MerchantId { get; set; }
        public string Password { get; set; }
    }
}