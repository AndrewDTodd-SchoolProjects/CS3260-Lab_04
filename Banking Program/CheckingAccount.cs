using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Program
{
    internal class CheckingAccount: Account
    {
        #region CONSTRUCTORS
        public CheckingAccount():base('C', 10.0,5.0)
        {
        }
        public CheckingAccount(string name, string address, double balance, AccountState state = AccountState._new): base('C', 10.0,5.0,name,address,balance,state)
        {
        }
        #endregion
    }
}
