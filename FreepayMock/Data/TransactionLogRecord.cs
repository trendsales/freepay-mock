﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreepayMock.Data
{
    [Table("TransactionLogs")]
    public class TransactionLogRecord
    {
        [Key]
        public int TransactionLogId { get; set; }
        public int RequestId { get; set; }
    }
}