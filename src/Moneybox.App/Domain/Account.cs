using System;

namespace Moneybox.App
{
    public class Account
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }
    }
}
