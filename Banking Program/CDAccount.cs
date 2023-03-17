using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Program
{
    internal class CDAccount: Account
    {
        #region CONSTRUCTORS
        public CDAccount() : base('D', 500.0, 8.0)
        {
        }
        public CDAccount(string name, string address, double balance, AccountState state = AccountState._new): base('D',500.0,8.0,name,address,balance,state)
        {
        }
        #endregion
    }
}
