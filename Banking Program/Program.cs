// Project Prolog
// Name: Andrew Todd
// CS3260 Section 001
// Project: Lab_03
// Date: 09/11/22
// Purpose: This Project is the first stages of a banking program. The program impements
// the basic functionality and error checking of a bank account structure, as well as Console
// based input for testing and usabuility.
//
// I declare that the following code was written by me or provided
// by the instructor for this project. I understand that copying source
// code from any other source constitutes plagiarism, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------

namespace Banking_Program
{
    ///<summary>
    /// This class is the program entrypoint and is responsible for handling input and output for the program.
    ///</summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            AccountBank bank;
            Account _account;
            uint accountNumber;
            Console.WriteLine("How many accounts do you want in the bank?");
            do
            {
                if (uint.TryParse(Console.ReadLine(), out accountNumber))
                {
                    bank = new(accountNumber);
                    break;
                }
                else
                    Console.WriteLine("Invalid Input, could not parse number. Try again.");
            } while (true);

            for (int i = 0; i < accountNumber; i++)
            {
                Console.WriteLine("Making new account...");

                Type: do
                {
                    Console.WriteLine("Enter Account type.\nS for Savings, C for checking, or D for Certificate of Deposit");
                    string? type = Console.ReadLine();

                    type = type.ToLower();

                    switch (type)
                    {
                        case "s":
                            _account = new SavingsAccount();
                            break;
                        case "c":
                            _account = new CheckingAccount();
                            break;
                        case "d":
                            _account = new CDAccount();
                            break;
                        default:
                            Console.WriteLine($"Invalid input. {type} not recognized option.");
                            goto Type;
                    }
                    break;
                } while (true);

                do
                {
                    Console.WriteLine("Enter the name that will appear on the account.");
                    if (_account.SetName(Console.ReadLine()))
                        break;
                    else
                        Console.WriteLine("Invalid name entered. Enter a non empty name.\n");

                } while (true);

                do
                {
                    Console.WriteLine("Enter the address that will appear on the account");
                    if (_account.SetAddress(Console.ReadLine()))
                        break;
                    else
                        Console.WriteLine("Invalid address entered. Enter a non empty address.\n");

                } while (true);

                /*
                do
                {
                    Console.WriteLine("Enter the Account Number that will appear on the account");

                    if (ulong.TryParse(Console.ReadLine(), out ulong num))
                    {
                        if (_account.SetAccountNumber(num))
                            break;
                        else
                            Console.WriteLine("Invalid Account Number entered. Enter valid non-negative whole number.\n");
                    }
                    else
                        Console.WriteLine("Input could not be parsed to ulong. Enter a valid non-negative whole number.\n");

                } while (true);
                */

                do
                {
                    Console.WriteLine("Enter the Balance for the new account");

                    if (double.TryParse(Console.ReadLine(), out double num))
                    {
                        if (_account.SetBalance(num))
                            break;
                        else
                            Console.WriteLine($"Invalid Balance entered. Enter a non-negative balance greater than new account minimum {_account.MinBalance}\n");
                    }
                    else
                        Console.WriteLine("Input could not be parsed to double. Enter a valid non-negative number.\n");

                } while (true);

            State: do
                {
                    Console.WriteLine("Enter the state for the new account. Default is new (entering null will result in new)");

                    string? input = Console.ReadLine();

                    if (uint.TryParse(input, out uint num))
                    {
                        switch (num)
                        {
                            case 0:
                                Console.WriteLine("Setting state to new");
                                _account.SetState(Account.AccountState._new);
                                break;
                            case 1:
                                Console.WriteLine("Setting state to active");
                                _account.SetState(Account.AccountState._active);
                                break;
                            case 2:
                                Console.WriteLine("Setting state to underAudit");
                                _account.SetState(Account.AccountState._underAudit);
                                break;
                            case 3:
                                Console.WriteLine("Setting state to frozen");
                                _account.SetState(Account.AccountState._frozen);
                                break;
                            case 4:
                                Console.WriteLine("Setting state to closed");
                                _account.SetState(Account.AccountState._closed);
                                break;
                            default:
                                Console.WriteLine("Unrecognized Input. Try again.");
                                goto State;
                        }
                        Console.WriteLine();
                        break;
                    }
                    else if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Null or Empty string entered. Setting Account State to defualt new state");
                        _account.SetState();
                        Console.WriteLine();
                        break;
                    }

                } while (true);

                if (!_account.GenAccountNumber())
                    Console.WriteLine("Was unable to Generate Account number. Trashing Account");
                else
                    bank.StoreAccount(_account);

                Console.WriteLine();
            }

            Console.WriteLine("Accounts in Bank");
            foreach (Account account in bank.Bank)
            {
                try
                {
                    Console.WriteLine($"Account type: {account.GetAccountType()}");
                    Console.WriteLine($"Name associated with account: {account.GetName()}");
                    Console.WriteLine($"Address associated with account: {account.GetAddress()}");
                    Console.WriteLine($"Account Number: {account.GetAccountNumber()}");
                    Console.WriteLine($"Account Balance: {account.GetBalance()}");
                    Console.WriteLine($"Account State: {account.GetState().ToString()}");
                }
                catch(System.Exception e)
                {
                    Console.WriteLine($"Exeption occured in retrieving account data.\n Exeption Type {e.ToString()}");
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine();
            }
        }
    }
}