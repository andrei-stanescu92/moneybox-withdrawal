using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountService accountService;

        public WithdrawMoney(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // TODO:
            Account sourceAccount = this.accountService.GetAccountByID(fromAccountId);

            var sourceAccountBalance = sourceAccount.Balance - amount;
            this.accountService.CheckAccountBalance(sourceAccountBalance, sourceAccount.User.Email);

            sourceAccount.Balance -= sourceAccount.Withdrawn;
        }
    }
}
