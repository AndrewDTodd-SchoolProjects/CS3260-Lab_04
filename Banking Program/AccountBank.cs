using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Program
{
    internal class AccountBank : IAccountBank
    {
        #region MEMBERS
        private List<Account> bank = new();
        #endregion

        #region CONSTRUCTORS
        public AccountBank(uint bankCap)
        {
            bank.EnsureCapacity((int)bankCap);
        }
        #endregion

        #region PUBLIC_METHODS
        public bool StoreAccount(Account account)
        {
            try
            {
                bank.Add(account);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public Account? FindAccount(string accountNum)
        {
            foreach (Account account in bank)
            {
                if (account.GetAccountNumber() == accountNum)
                    return account;
            }

            return null;
        }

        #endregion

        #region GET_SET
        public List<Account> Bank 
        {
            get { return bank; }
        }
        #endregion
    }
}
