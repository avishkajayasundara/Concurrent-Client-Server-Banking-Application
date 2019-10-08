using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Account: IAccountControl
    {
        int accountNumber;
        string accountType;
        double balance;
        private readonly double  initialDeposit = 500;
        private readonly double minBalance = 100;
        private readonly double maxWithdrawalLimit = 100000;
        private readonly double interest=10;
        private readonly double charges = 50;
        Mutex _lock = new Mutex();

        public Account(int accountNumber)
        {
            this.accountNumber = accountNumber;
            
            this.accountType = "Savings";
            this.balance = initialDeposit;
        }
        public Account()
        {
            this.accountNumber =0;
            this.accountType = "Savings";
            this.balance = initialDeposit;
        }

        public int AccountNumber { get => accountNumber; set => accountNumber = value; }
        public string AccountType { get => accountType; set => accountType = value; }
        public double Balance { get => balance; set => balance = value; }

        public double InitialDeposit => initialDeposit;

        public double MinBalance => minBalance;

        public double MaxWithdrawalLimit => maxWithdrawalLimit;

       

        public double checkBalance()
        {
            return balance;
        }

        public void deposit(double amount)
        {
            if (_lock.WaitOne())
            {
                try
                {
                    balance = balance + amount;
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
        }

        public void transfer(int accNo, double amount)
        {
            throw new NotImplementedException();
        }

        public string withdraw(double amount)
        {
            string response = null;
            if (_lock.WaitOne())
            {
                try
                {
                    if (amount > maxWithdrawalLimit)
                    {
                        response = "maxlimit";
                    }else if (amount > (balance + minBalance))
                    {
                        response = "nocredit"; 
                    }
                    else
                    {
                        if (amount < maxWithdrawalLimit&& amount < (balance + minBalance))
                        {
                            balance -= amount;
                            response = "sucessfull";
                        }
                    }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            return response;    
        }

        public void CreditInterest()
        {
            if (_lock.WaitOne())
            {
                try
                {
                    balance = balance + balance * (interest / 100);
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            balance = balance + balance * (interest / 100);
        }

        public void DebitCharges()
        {
            if (_lock.WaitOne())
            {
                try
                {
                    balance = balance - charges;
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            
        }
    }
}
