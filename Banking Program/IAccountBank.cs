using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Program
{
    ///<summary>
    /// This interface defines the basic public functionality of any "Account Bank" type
    ///</summary>
    internal interface IAccountBank
    {
        /// <summary>
        /// Purpose: will add a new account to the implementing classe's underlying account bank collection
        /// </summary>
        /// <param name="account"> any valid, fully constructed, Account object</param>
        /// <returns>true if account is succesfully added to container, false otherwise.</returns>
        bool StoreAccount(Account account);

        /// <summary>
        /// Purpose: will return a reference to an Account object if the bank's container holds a reference to that object
        /// </summary>
        /// <param name="accountNum"> any valid non-null string that represents the account's identifying number</param>
        /// <returns>Reference to Account object if found, null otherwise.</returns>
        Account? FindAccount(string accountNum);
    }
}
