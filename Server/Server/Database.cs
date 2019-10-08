using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Database
    {
        List<Client> clients = new List<Client>();
        List<string> loggedInClients = new List<string>();
        Mutex _lock = new Mutex();

        public object ClientSocket { get; private set; }

        public void AddClient(Client client)
        {

            if (_lock.WaitOne())
            {
                try
                {
                    clients.Add(client);
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            clients.Add(client);

        }

        public bool AuthenticateUser(string username, string password)
        {
            bool validity = false;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                    {
                        if (client.Username.Equals(username) && client.Password.Equals(password))
                        {
                            validity = true;
                        }
                    }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }

            return validity;
        }
        public void addAccount(string username, Account account)
        {
            foreach (Client client in clients)
            {
                if (_lock.WaitOne())
                {
                    try
                    {
                        if (client.Username.Equals(username))
                        {
                            client.createAccount(account);
                        }
                    }
                    finally
                    {
                        _lock.ReleaseMutex();
                    }
                }

            }
        }

        public void CreditInterest()
        {
            
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Account != null)
                        {
                            client.Account.CreditInterest();

                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }
            
        }
        public void DebitCharges()
        {
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Account != null)
                        {
                            client.Account.DebitCharges();
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }
        }

        public string ViewDetails(string username)
        {
            string details = null;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                    {
                        if (client.Username.Equals(username))
                        {
                            details = client.displayDetails();
                        }
                    }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }


            return details;
        }
        public void EditDetails(string username, string email, string password, string occupation)
        {
            foreach (Client client in clients)
            {
                if (client.Username.Equals(username))
                {
                    client.Email = email;
                    client.Password = password;
                    client.Occupation = occupation;
                }
            }
        }
        public void Deposit(string username, double amount)
        {
            
                if (_lock.WaitOne())
                {
                    try
                    {
                        foreach (Client client in clients)
                            if (client.Username.Equals(username))
                            {
                                client.Account.deposit(amount);
                            }
                    }
                    finally
                    {
                        _lock.ReleaseMutex();
                    }
                }


            
        }
        public string Withdraw(string username, double amount)
        {
            string response = null;

            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(username))
                        {
                            response = client.Account.withdraw(amount);
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }

       return response;
        }

        public void EditPassword(string v1, string v2)
        {
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(v1))
                        {
                            client.Password = v2;
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
        }

        public void Deactivate(string v)
        {
            for(int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Username.Equals(v))
                {
                    clients.Remove(clients[i]);
                }
            }
        }

        public void EditOccupation(string v1, string v2)
        {
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(v1))
                        {
                            client.Occupation = v2;
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
        }
        public void EditEmail(string v1, string v2)
        {
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(v1))
                        {
                            client.Email = v2;                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
        }
        public bool IsLoggedIn(string username)
        {
            bool loggedIn = false;
            if (_lock.WaitOne())
            {
                try
                {

                    foreach (string client in loggedInClients)
                    {
                        if (username.Equals(client))
                        {
                            loggedIn = true;
                        }

                    }

                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
            return loggedIn;



        }

        public void EditUname(string v1, string v2)
        {
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(v1))
                        {
                            client.Username = v2;
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }
            }
        }
        public double getBalance(string username)
        {
            double temp = 0;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(username))
                        {
                            temp = client.Account.Balance;
                        }
                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }
            return temp;
        }
        public bool HasAccount(string username)
        {
            bool a = true; ;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Account == null)
                        {
                            a= false;
                        }
                       
                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }
            return a;
        }
        public string Transfer(string username, int acc, double amount)
        {
            string response = null;

            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                    {
                        if (client.Username.Equals(username))
                        {
                            response = client.Account.withdraw(amount);

                        }


                    }
                    if (response.Equals("sucessfull"))
                    {
                        foreach (Client client in clients)
                        {
                            if (client.Account.AccountNumber == acc)
                            {
                                client.Account.deposit(amount);
                            }
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
        public void Logout(string username)
        {
           for(int i = 0; i < loggedInClients.Count; i++)
            {
                if (loggedInClients[i].Equals(username))
                {
                    loggedInClients.Remove(loggedInClients[i]);
                }
            }
        }
        public void AddLogin(string username)
        {
            loggedInClients.Add(username);
        }
        public bool SearchAcount(int num)
        {
            bool a = false;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Account.AccountNumber==num)
                        {
                            a = true;
                        }

                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }
            return a;
        }
        public bool ValidateUsername(string username)
        {
            bool valid = false;
            if (_lock.WaitOne())
            {
                try
                {
                    foreach (Client client in clients)
                        if (client.Username.Equals(username))
                        {
                            valid = true;
                        }

                }
                finally
                {
                    _lock.ReleaseMutex();
                }

            }

            return valid;
        }
    }
}
