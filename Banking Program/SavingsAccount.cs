using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Program
{
    internal class SavingsAccount: Account
    {
        #region CONSTRUCTORS
        public SavingsAccount():base('S',100.0,0.0)
        {
        }
        public SavingsAccount(string name, string address, double balance, AccountState state = AccountState._new) : base('S', 100.0, 0.0, name, address, balance, state)
        {
        }
        #endregion
    }
}
