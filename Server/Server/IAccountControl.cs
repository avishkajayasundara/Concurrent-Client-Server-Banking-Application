using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IAccountControl
    {
        double checkBalance();
        void deposit(double amount);
        string withdraw(double amount);

        void transfer(int accNo, double amount);

        void CreditInterest();
        void DebitCharges();

    }
}
