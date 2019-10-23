using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Services
{
    public interface IAccountService
    {
        void CheckAccountBalance(decimal sourceAccountBalance, string userEmail);
        void CheckPayLimit(decimal amountPaid, string userEmail);
        Account GetAccountByID(Guid ID);
        void Update(Account account);
    }
}
