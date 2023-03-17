using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banking_Program
{
    ///<summary>
    /// This class is an implementation of the concept of a bank account. 
    /// It implements the public functionality defined by the <cref>IAccount</cref> interface for end-user interactivity.
    ///</summary>
    internal abstract class Account : IAccount
    {
        #region MEMBERS
        private string? name = null;
        private string? address = null;
        private string? accountNumber = null;
        private double balance = 0.0;
        private readonly double minBalance = 100.0;
        private double serviceFees = 0.0;
        private AccountState state;
        private readonly char? accountType = null;
        #endregion

        #region PUBLIC_ENUMS
        public enum AccountState
        {
            _new,
            _active,
            _underAudit,
            _frozen,
            _closed
        }
        #endregion

        #region CONSTRUCTORS
        public Account(char accountType, double minBalance)
        {
            AccountType = accountType;
            MinBalance = minBalance;
        }
        public Account(char accountType, double minBalance, double fees)
        {
            ServiceFee = fees;
            MinBalance = minBalance;
            AccountType = accountType;
        }
        public Account(char accountType, double minBalance, double fees, string name, string address, double balance, AccountState state = AccountState._new)
        {
            AccountType = accountType;
            MinBalance = minBalance;
            ServiceFee = fees;
            if (!SetName(name))
                throw new System.ArgumentException($"name passed in ({name}) is invalid");
            if (!SetAddress(address))
                throw new System.ArgumentException($"address passed in ({address}) is invalid");
            if(!SetBalance(balance))
                throw new System.ArgumentException($"initial balance passed in ({balance}) is invalid.\nMust be equal to or greater than min balance of {minBalance}.");
            SetState(state);

            if(!GenAccountNumber())
                throw new System.InvalidOperationException($"Unable to generate account number");
        }
        #endregion

        #region IACCOUNT_METHODS
        public virtual bool SetName(string inName)
        {
            if (inName == null || inName == "")
                return false;

            name = inName;
            return true;
        }

        public virtual string? GetName()
        {
            return name;
        }
        public virtual bool SetAddress(string inAddress)
        {
            if (inAddress == null || inAddress == "")
                return false;

            address = inAddress;
            return true;
        }
        public virtual string? GetAddress()
        {
            return address;
        }
        /*
        public bool SetAccountNumber(ulong inAccountNumber)
        {
            this.accountNumber = inAccountNumber;
            return true;
        }
        */
        public virtual bool GenAccountNumber()
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || accountType == null)
                return false;

            string nameCopy = name.ToLower().Trim();
            Regex.Replace(nameCopy, @"\s+", "");

            string addressCopy = address.ToLower().Trim();
            Regex.Replace(addressCopy, @"\s+", "");

            UInt32 hash = BitConverter.ToUInt32(BitConverter.GetBytes(nameCopy.Length), 0);
            UInt32 wrapper = hash >> 30;
            hash = BitConverter.ToUInt32(BitConverter.GetBytes((hash << 30) | wrapper), 0);

            hash += BitConverter.ToUInt32(BitConverter.GetBytes(addressCopy.Length), 0);
            wrapper = hash >> 28;
            hash = BitConverter.ToUInt32(BitConverter.GetBytes((hash << 28) | wrapper), 0);

            hash += (uint)accountType;
            wrapper = hash >> 26;
            hash = BitConverter.ToUInt32(BitConverter.GetBytes((hash << 26) | wrapper), 0);

            accountNumber = hash.ToString() + accountType;

            return true;
        }
        public virtual string? GetAccountNumber()
        {
            return this.accountNumber;
        }
        public virtual void PayInFunds(double amount)
        {
            if (amount < 0.0)
                throw new ArgumentException($"amount({amount}) is not a positive number. Use the WithdrawFunds method for decreasing the balance");

            balance += amount;
        }
        public virtual bool WithdrawFunds(double amount)
        {
            if(amount < 0.0)
                throw new ArgumentException($"amount({amount}) is not a positive number. Use the PayInFunds method for increasing the balance");

            if ((balance - amount) < 0.0)
                return false;

            balance -= amount;
            return true;
        }
        public virtual bool SetBalance(double inBalance)
        {
            if(inBalance < minBalance)
                return false;

            balance = inBalance;
            return true;
        }
        public virtual double GetBalance()
        {
            return balance;
        }
        public virtual void SetState(AccountState state = AccountState._new)
        {
            this.state = state;
        }
        public virtual AccountState GetState()
        {
            return state;
        }

        public virtual bool SetServiceFee(double fee)
        {
            if (fee < 0.0)
                return false;

            serviceFees = fee;
            return true;
        }
        public virtual double GetServiceFee()
        {
            return serviceFees;
        }
        public virtual string? GetAccountType()
        {
            if (string.IsNullOrEmpty(accountNumber))
                throw new InvalidOperationException("The account has no number");

            string accountType = accountNumber.Substring(accountNumber.Length - 1);

            accountType = accountType.ToLower();

            switch(accountType)
            {
                case "s":
                    return "Savings";
                case "c":
                    return "Checking";
                case "d":
                    return "Certificate of Deposit";
                default:
                    throw new InvalidOperationException("Unable to parse account number for Account type");
            }
        }
        #endregion

        #region GET_SET_INIT
        //public virtual double InitialBalance { get; }
        public virtual char? AccountType
        {
            get { return accountType; }
            init { accountType = value; }
        }
        public virtual double ServiceFee
        {
            get { return serviceFees; }
            set { serviceFees = (value > 0.0) ? value : 0.0; }
        }
        public virtual double MinBalance
        {
            get { return minBalance; }
            init { minBalance = (value > 0.0) ? value : 0.0; }
        }
        #endregion
    }
}
