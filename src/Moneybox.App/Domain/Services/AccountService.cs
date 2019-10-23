using Moneybox.App.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly INotificationService notificationService;
        private readonly IAccountRepository accountRepository;

        public AccountService(INotificationService notificationService, IAccountRepository repository)
        {
            this.notificationService = notificationService;
            this.accountRepository = repository;
        }

        public void CheckAccountBalance(decimal sourceAccountBalance, string userEmail)
        {
            if (sourceAccountBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (sourceAccountBalance < 500m)
            {
                this.notificationService.NotifyFundsLow(userEmail);
            }
        }

        public void CheckPayLimit(decimal amountPaid, string userEmail)
        {
            if (amountPaid > AccountLimits.PayIn)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (AccountLimits.PayIn - amountPaid < 500m)
            {
                this.notificationService.NotifyApproachingPayInLimit(userEmail);
            }
        }

        public Account GetAccountByID(Guid ID)
        {
            return this.accountRepository.GetAccountById(ID);
        }

        public void Update(Account account)
        {
            this.accountRepository.Update(account);
        }
    }
}
