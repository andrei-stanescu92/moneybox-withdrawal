using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountService accountService;

        public TransferMoney(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var sourceAccount = this.accountService.GetAccountByID(fromAccountId);
            var destinationAccount = this.accountService.GetAccountByID(toAccountId);

            var sourceAccountBalance = sourceAccount.Balance - amount;
            this.accountService.CheckAccountBalance(sourceAccountBalance, sourceAccount.User.Email);

            var paidAmount = destinationAccount.PaidIn + amount;
            this.accountService.CheckPayLimit(paidAmount, destinationAccount.User.Email);

            sourceAccount.Balance -= amount;
            sourceAccount.Withdrawn -= amount;

            destinationAccount.Balance += amount;
            destinationAccount.PaidIn += amount;

            this.accountService.Update(sourceAccount);
            this.accountService.Update(destinationAccount);
        }
    }
}
