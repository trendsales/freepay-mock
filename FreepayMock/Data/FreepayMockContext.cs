﻿using System.Data.Entity;

namespace FreepayMock.Data
{
    public class FreepayMockContext : DbContext
    {
        public FreepayMockContext() : base("name=Database")
        {
        }

        public DbSet<MerchantRecord> Merchants { get; set; }
        public DbSet<TransactionRecord> Transactions { get; set; }
        public DbSet<SubscriptionRecord> Subscriptions { get; set; }
        public DbSet<RequestLogRecord> RequestLogs { get; set; }
        public DbSet<TransactionLogRecord> TransactionLogs { get; set; }
        public DbSet<SubscriptionLogRecord> SubscriptionLogs { get; set; }
        public DbSet<CardRecord> Cards { get; set; }
    }
}
