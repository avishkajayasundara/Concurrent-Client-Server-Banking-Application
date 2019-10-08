using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;

namespace Server
{
    public partial class GUI : Form
    {

       
        private static int port = 8080;
        private static String ipAddress = "127.0.0.1";
        Socket ServerListener;
        Socket ClientSocket;

        Database database = new Database();


        public GUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ipText.Text = ipAddress;
            portText.Text = ""+port;


            


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void startButton_Click(object sender, EventArgs e)
        {
               Thread t = new Thread(new ThreadStart(StartServer));
                t.Start();

                startButton.Enabled = false;
            
        }
        
       

        
        private void creditButton_Click(object sender, EventArgs e)
        {
            database.CreditInterest();
            messageBox.AppendText("Interest Has been credited to all Savings accounts"); ;
            string response = "CreditInterest" + "," + "Interest has been Credited to your account";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            
        }

        private void debitButton_Click(object sender, EventArgs e)
        {
            database.DebitCharges();
            messageBox.AppendText("Bank charges have been debited from all accounts");
            string response = "DebitCharges" + "," + "Bank Charges have been debited from your account";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
        }
        public void StartServer()
        {
            int count = 0;
            ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream
                    , ProtocolType.Tcp);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                ServerListener.Bind(endpoint);
                ServerListener.Listen(100);
                messageBox.AppendText("Server has started...\n");
                messageBox.AppendText("Server is listening...\n\n");
                ClientSocket = default(Socket);
           
          while (true)
          {
                
                ClientSocket = ServerListener.Accept();
                Thread thread = new Thread(new ThreadStart(() => ProcessClientRequest(ClientSocket)));
                thread.Start();
               count++;
                
                messageBox.AppendText(count + " Clients connected\n");
                 }
           

        }

        public void ProcessClientRequest(Socket client)
        {
            while (true)
            {
                byte[] request = new byte[350000];
                int size = client.Receive(request);

                String name = System.Text.Encoding.UTF8.GetString(request, 0, size);

                Thread t = new Thread(() => HandelClientRequest(name, client));
                t.Start();             
                
            }
        }
        public void HandelClientRequest(String request, Socket ClientSocket)
        {
            string[] message = request.Split(',');
            String command = message[0];
            switch (command)
            {
                case "Register":
                    
                    RegisterClient(message, ClientSocket);
                    break;
                case"Login":Login(message, ClientSocket);
                    break;
                case "CreateAccount":CreateAccount(message, ClientSocket);
                    break;
                case"Deposit":Deposit(message, ClientSocket);
                    break;
                case"Withdraw":Withdraw(message, ClientSocket);
                    break;
                case "Transfer": Transfer(message, ClientSocket);
                    break;            
                case"View":ViewDetails(message, ClientSocket);
                    break;               
                case "Logout":Logout(message, ClientSocket);
                    break;
                case"Balance":CheckBalance(message, ClientSocket);
                    break;
                case "EditUname":
                    EditUname(message, ClientSocket);
                    break;
                case "EditEmail":
                    EditEmail(message, ClientSocket);
                    break;
                case "EditOccupation":
                    EditOccupation(message, ClientSocket);
                    break;
                case "EditPassword":
                    EditPassword(message, ClientSocket);
                    break;
                case "Exit":Exit( ClientSocket);
                    break;
                case "Deactivate":
                    DeactivateClient(message, ClientSocket);
                    break;
            }

        }

        public void DeactivateClient(string[] message, Socket ClientSocket)
        {
            database.Deactivate(message[1]);
            string response = "Deactivated";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(message[1] + "Deactivated the account");
        }

        public void Exit(Socket client)
        {
            messageBox.AppendText("Client Disconnected\n");
            
        }

        private void EditPassword(string[] message, Socket ClientSocket)
        {
            database.EditPassword(message[1],message[2]);
            string response = "EditPassword" + "," + "Password Updated";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(message[1] + " : Password Updated \n");
        }

        private void EditOccupation(string[] message, Socket ClientSocket)
        {
            database.EditOccupation(message[1], message[2]);
            string response = "EditOccupation" + "," + "Occupation Updated";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(message[1] + " : Occupation Updated \n");
        }

        private void EditEmail(string[] message, Socket ClientSocket)
        {
            database.EditEmail(message[1], message[2]);
            string response = "EditEmail" + "," + "Email Updated";
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(message[1] + " : Email Updated \n");
        }

        private void EditUname(string[] message, Socket ClientSocket)
        {
            if (database.ValidateUsername(message[2]))
            {
                string confirmation = "UnameF" + "," + " The Username already exists in the system";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(message[1] + " : Username wasn't updated \n");
            }
            else
            {
                database.EditUname(message[1], message[2]);
                string response = "EditUname" + "," + "Username Updated" + "," + message[2];
                ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
                messageBox.AppendText(message[1] + " : Username Updated \n");
            }
            
        }

        private void Logout(string[] message, Socket ClientSocket)
        {
            String uname = message[1];
            database.Logout(uname);
            string response = "Logout" + "," + "Logging out..." ;
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(message[1] + " : Logged Out \n");

        }
        
        

        private void ViewDetails(string[] message, Socket ClientSocket)
        {
            string username = message[1];
            string response = "View"+","+database.ViewDetails(username);
            ClientSocket.Send(Encoding.ASCII.GetBytes(response), response.Length, SocketFlags.None);
            messageBox.AppendText(username + " : Details sent \n");


        }

        

        private void Transfer(string[] message, Socket ClientSocket)
        {
            string username = message[1];
            int acc = Int32.Parse(message[2]);
            double amount= double.Parse(message[3]);

            if (database.SearchAcount(acc))
            {
                if (database.HasAccount(username))
                {
                    string status = database.Transfer(username, acc, amount);

                    string confirmation = status;
                    ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                    if (status.Equals("sucessfull"))
                    {
                        messageBox.AppendText(username + " : Transaction Performed of Rs." + amount + "\n");

                    }
                    else
                    {
                        messageBox.AppendText(username + " : Transaction did not proceed\n");

                    }

                }
                else
                {
                    string confirmation = "NoAccount" + "," + "You do not have an active account";
                    ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                    messageBox.AppendText(username + " : Transaction Failure \n");

                }
            }
            else
            {
                string confirmation = "InvalidAccount" + "," + "Recepient Account Does not exist";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " : Transaction Failure \n");

            }




        }

        private void Withdraw(string[] message, Socket ClientSocket)
        {
            string status;
            string username = message[1];
            double amount = double.Parse(message[2]);
            if (database.HasAccount(username))
            {

                status = database.Withdraw(username, amount);
                string confirmation = status;
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                if (status.Equals("sucessfull"))
                {
                    messageBox.AppendText(username + " : Withdrawal made of Rs." + amount + "\n");

                }
                else
                {
                    messageBox.AppendText(username + " : Withdrawal did not proceed\n");

                }
            }
            else
            {
                string confirmation = "NoAccount" + "," + "You do not have an active account";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " : Transaction Failure \n");

            }

            

        }

        private void Deposit(string[] message, Socket ClientSocket)
        {
            string username = message[1];
            double amount = double.Parse(message[2]);
            if (database.HasAccount(username))
            {
                database.Deposit(username, amount);
                string confirmation = "Deposit" + "," + "Deposit was made of Rs." + amount;
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " : Deposit made of Rs." + amount + "\n");

            }
            else
            {
                string confirmation = "NoAccount" + "," + "You do not have an active account";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " : Transaction Failure \n");

            }






        }

        private void CreateAccount(string[] message, Socket ClientSocket)
        {
            
            string username = message[1];

            if (HasAccount(username,ClientSocket))
            {
                string confirmation = "HasAccount" + "," + " You already have an active account";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " Account Creation unsuccessful\n");
            }
            else
            {
                int accountNumber = Int32.Parse(message[2]);
                Account account = new Account(accountNumber);
                database.addAccount(username,account);

                string confirmation = "AccountS" + "," + " Account Created Sucessfully";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " Created an Account Successfully\n");
            }
        }

        private void Login(string[] message, Socket ClientSocket)
        {
            string username = message[1];
            string password = message[2];
            string confirmation;
            if (database.IsLoggedIn(message[1]))
            {
                confirmation = "LoginF" + "," + "You are already logged in";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " : Failed Login Attempt\n");

                
            }
            else
            {
                if (database.AuthenticateUser(username, password))
                {
                   
                    confirmation = "LoginS" + "," + username + "," + " You Logged in Successfully";
                    database.AddLogin(username);
                    ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                    messageBox.AppendText(username + " : Logged In successfully\n");
                }
                else
                {
                    confirmation = "LoginF" + "," + "Invalid Credentials";
                    ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                    messageBox.AppendText("Failed login Attempt\n");
                }
            }
        }

        public void RegisterClient(string[] message, Socket ClientSocket)
        {
           

            string username = message[6];
            if (database.ValidateUsername(username))
            {
                string confirmation = "RegisteredF" + "," + " The Username already exists in the system";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText("Unsuccessful Registration Attempt\n");
            }
            else
            {
                Client client = new Client(message[1], message[2], message[3], message[4], message[5], message[6], message[7]);              
                database.AddClient(client);
                string confirmation = "RegisteredS" + "," + " You have been registered successfully";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " was successfully registered\n");

            }
                
        }
            
        public void CheckBalance(string[] message, Socket ClientSocket)
        {
            if (database.HasAccount(message[1]))
            {
                string username = message[1];
                double bal = database.getBalance(username);
                string confirmation = "Balance" + "," + bal;
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(username + " Viewed Balance\n");
            }
            else
            {
                string confirmation = "NoAccount" + "," + "You do not have an active account";
                ClientSocket.Send(Encoding.ASCII.GetBytes(confirmation), confirmation.Length, SocketFlags.None);
                messageBox.AppendText(message[1] + " Tried to view the balance with no active account\n");
            }
            


        }

        private void portText_TextChanged(object sender, EventArgs e)
        {

        }
        public bool HasAccount(string username, Socket ClientSocket)
        {
            bool hasAccount = database.HasAccount(username);
            return hasAccount;

        }
    }
    
}
