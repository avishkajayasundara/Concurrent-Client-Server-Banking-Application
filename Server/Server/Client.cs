using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Server
{
    public class Client: IClientControl
    {
        private String name, nic, gender, email, occupation, username, password;
        private Account account;
        public Client(String name, String nic, String gender, String email, String occupation, String username, String password)
        {
            this.name = name;
            this.nic = nic;
            this.gender = gender;
            this.email = email;
            this.occupation = occupation;
            this.username = username;
            this.password = password;

        }

        public string Name { get => name; set => name = value; }
        public string Nic { get => nic; set => nic = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string Occupation { get => occupation; set => occupation = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public Account Account { get => account; set => account = value; }

        public void createAccount(Account account)
        {
            
            this.account = account;
        }

        public string displayDetails()
        {
            return (name + "-" + nic + "-" + gender + "-" + email + "-" + occupation + "-" + username + "-" + password);
            
        }

        public void editDetails(string email, string occupation, string password)
        {
            throw new NotImplementedException();
        }
    }
}
