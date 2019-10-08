using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Goliath_National_Bank
{
    class Program
    {
        static Socket ClientSocket;
        static string command;
        static string message;
        static string menuChoice;
        static string clientName;
        static List<string> Notifications = new List<string>();
        


        static void Main(string[] args)
        {
            string ipAddress = "127.0.0.1";
            int port = 8080;
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            ClientSocket.Connect(endpoint);
            HomePage(ClientSocket);
        }
        
        public static void Heading()
        {
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\n\n                 Goliath National Bank\n\n");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\n\n-----------   The Bank At your Fingertips   -----------");
        } 

        public static void HomePage(Socket ClientSocket)
        {
            
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\n\n        Welcome To Goliath National Bank\n\n");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\n");
            Console.WriteLine("       1.LOGIN                  :           [Press '1']        ");
            Console.WriteLine("       2.REGISTER               :           [Press '2']        ");
            Console.WriteLine("       3.Exit                   :           [Press '3']        ");
            Console.WriteLine("\n\n");
            Console.Write("Enter your choice : ");
            menuChoice = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(menuChoice, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Try again");
                Console.Write("Enter Your choice : ");
                menuChoice = Console.ReadLine();
                
            }
            while (parsedValue != 1 && parsedValue != 2&& parsedValue != 3)
            {
                Console.WriteLine("Invalid Choice... ");
                Console.Write("Enter Your choice : ");
                parsedValue = Int32.Parse(Console.ReadLine()) ;
            }
            if (parsedValue == 1)
            {
                string message = Login(ClientSocket);
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse(ClientSocket);
            }
            else if(parsedValue == 2)
            {
                Register(ClientSocket);
                
                
            }else if (parsedValue == 3)
            {
                Console.WriteLine("Exiting Application.....");
                Exit(ClientSocket);
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Close();
                Thread.Sleep(1500);
                Environment.Exit(0);
            }
            
        }

        private static void Exit(Socket clientSocket)
        {

            message = "Exit";
            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            
        }

        private static void Register(Socket ClientSocket)
        {
            string name;
            string nic;
            string email;
            string occupation;
            string gender;
            string username;
            string password;

            Console.WriteLine("\n\n********************************************************* ");

            Console.WriteLine("\n                Be A Part Of the Future of Banking                      ");
            Console.WriteLine("\n**********************************************************");

            Console.Write("Name :");
            name = Console.ReadLine();
            Console.Write("NIC Number :");
            nic = Console.ReadLine();
            Console.Write("Email :");
            email = Console.ReadLine();
            while (!email.Contains("@")|| !email.Contains("."))
            {
                Console.WriteLine("Invalid Email");
                Console.Write("Email :");
                email = Console.ReadLine();
            }
            Console.Write("Occupation :");
            occupation = Console.ReadLine();
            Console.Write("Gender :");
            gender = Console.ReadLine();
            while(!gender.ToLower().Equals("male")&& !gender.ToLower().Equals("female"))
            {
                Console.WriteLine("Invalid Input");
                Console.Write("Gender :");
                gender = Console.ReadLine();
            }
            Console.Write("Username :");
            username = Console.ReadLine();
            Console.Write("Password :");
            password = Console.ReadLine();

            message = "Register" + "," + name + "," + nic + "," + email + "," + occupation + "," 
                + gender + "," + username + "," + password;

            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            getResponse(ClientSocket);
           

        }

        public static string Login(Socket ClientSocket)
        {
            string username = null;
            string password = null;
            Heading();

            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                       LOGIN                      ");
            Console.WriteLine("\n*****************************************************");

            Console.Write("\nUserame  : ");
            username = Console.ReadLine();
            Console.Write("\nPassword :");
            password = Console.ReadLine();
            message = "Login" + "," + username + "," + password;
            return message;


        }
        public static void getResponse(Socket ClientSocket)
        {
            while (true)
            {
                byte[] response = new byte[50000];
                int size = ClientSocket.Receive(response);
                string request = System.Text.Encoding.UTF8.GetString(response, 0, size);
                HandleResponse(request, ClientSocket);

            }
        }
        public static void HandleResponse(string response, Socket ClientSocket)
        {
            string[] recievedMessage = response.Split(',');
            command = recievedMessage[0];


            if (command.Equals("RegisteredS"))
            {
                Console.Clear();
                
                Console.WriteLine("**" + recievedMessage[1] + "**\n\n");
                Notifications.Add(recievedMessage[1]);


                HomePage( ClientSocket);

            }
            else if (command.Equals("RegisteredF"))
            {
                Console.Clear();
                Console.WriteLine("** " + recievedMessage[1] + " **");

                HomePage(ClientSocket);
            }
            else if (command.Equals("LoginS"))
            {
                clientName = recievedMessage[1];
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n** "+ recievedMessage[2] + " **\n\n");
                
                MainMenu(ClientSocket);

                
                


            }
            else if (command.Equals("LoginF"))
            {
                Console.Clear();
                Console.WriteLine("** " + recievedMessage[1] + " **");
               
                HomePage(ClientSocket);
                

            }
            else if (command.Equals("AccountS"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu( ClientSocket);
            }
            else if (command.Equals("HasAccount"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n** " + recievedMessage[1] + " **\n\n");

                MainMenu( ClientSocket);
            }
            else if (command.Equals("Deposit"))
            {
                
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu(ClientSocket);
            }
            else if (command.Equals("nocredit"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + "Insufficient Credit" + " **\n\n");
                MainMenu(ClientSocket);

            }
            else if (command.Equals("maxlimit"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n   ** " + "ERROR!!Transaction Amount exceeds Maximum Limit" + " **\n\n");
                MainMenu( ClientSocket);

            }
            else if (command.Equals("sucessfull"))
            {

                Console.Clear();
                Heading();

                Console.WriteLine("\n\n   ** " + "Transaction was made successfully" + " **\n\n");
                Notifications.Add("Transaction was made successfully");
                MainMenu( ClientSocket);
            }
            else if (command.Equals("View"))
            {

               DisplayDetails(recievedMessage[1]);
            }
            else if (command.Equals("Balance"))
            {

                Balance(recievedMessage[1]);
            }
            else if (command.Equals("EditPassword"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu( ClientSocket);
            }
            else if (command.Equals("EditOccupation"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu( ClientSocket);
            }
            else if (command.Equals("EditEmail"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu( ClientSocket);
            }
            else if (command.Equals("EditUname"))
            {
                clientName = recievedMessage[2];
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n        ** " + recievedMessage[1] + " **\n\n");
                Notifications.Add(recievedMessage[1]);
                MainMenu(ClientSocket);
            }
            else if (command.Equals("NoAccount"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n   ** " + recievedMessage[1] + " **\n\n");
                MainMenu(ClientSocket);
            }
            else if (command.Equals("InvalidAccount"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n   ** " + recievedMessage[1] + " **\n\n");
                MainMenu(ClientSocket);
            }
            else if (command.Equals("CreditInterest"))
            {
                Notifications.Add(recievedMessage[1]);
            }
            else if (command.Equals("DebitCharges"))
            {
                Notifications.Add(recievedMessage[1]);
            }
            else if (command.Equals("Logout"))
            {
                Console.Clear();
                Console.WriteLine("** " + recievedMessage[1] + " **");
                Thread.Sleep(1000);
                Console.Clear();

                HomePage(ClientSocket);
            }
            else if (command.Equals("Deactivated"))
            {
                Console.Clear();
                Console.WriteLine("Deactivating Account...");
                Thread.Sleep(1500);
                Console.Clear();

                HomePage(ClientSocket);
            }
            else if (command.Equals("UnameF"))
            {
                Console.Clear();
                Heading();
                Console.WriteLine("\n\n   ** " + recievedMessage[1] + " **\n\n");
                MainMenu(ClientSocket);
            }
            

        }

        public static void Balance(string v)
        {
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                       Account Balance                ");
            Console.WriteLine("\n\n****************************************************\n ");
            Console.Write("Your account balance : Rs." +v);

            Console.Write("\nPress any number Go back to Main Menu: ");
            string input = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(input, out parsedValue))
            {
                Console.WriteLine("Error In Input");

                Console.Write("Press any number Go back to Main Menu: ");
                input = Console.ReadLine();
            }


                Console.Clear();
                Heading();
                MainMenu(ClientSocket);
            

        }

        public static void MainMenu(Socket ClientSocket)
        {
            Console.WriteLine("\n\n********************************************************\n\n");
            Console.WriteLine("       1.Create Account            ");
            Console.WriteLine("       2.View Details              ");
            Console.WriteLine("       3.Edit Details         ");
            Console.WriteLine("       4.Account Balance         ");
            Console.WriteLine("       5.Deposit           ");
            Console.WriteLine("       6.Withdraw                 ");
            Console.WriteLine("       7.Fund Transfer                  ");
            Console.WriteLine("       8.View Notifications                  ");
            Console.WriteLine("       9.Logout                  ");
            Console.WriteLine("       10.Deactivate Account                  ");
           

            Console.WriteLine("\n\n*********************************************************\n\n");
            Console.Write("Enter Your Choice : ");
            menuChoice = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(menuChoice, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Try again");
                Console.Write("Enter Your choice : ");
                menuChoice = Console.ReadLine();
            }
            while (parsedValue != 1 && parsedValue != 2 && parsedValue != 3 && parsedValue != 4 && 
                parsedValue != 5 && parsedValue != 6 && parsedValue != 7 && parsedValue != 8 && parsedValue != 9 && parsedValue != 10)
            {
                Console.WriteLine("Invalid Choice... ");
                Console.Write("Enter Your choice : ");
                parsedValue = Int32.Parse(Console.ReadLine());
            }
           


            if (parsedValue == 1)
            {
                CreateAccount( ClientSocket);
                parsedValue = 0;
            }
            else if (parsedValue == 2)
            {
                 message = "View" + "," + clientName;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                //getResponse();
                parsedValue = 0;
            }
            else if (parsedValue == 3)
            {
                Edit(ClientSocket);
                parsedValue = 0;
            }
            else if (parsedValue == 4)
            {
                 message = "Balance" +","+ clientName;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                // getResponse();
                parsedValue = 0;
            }
            else if (parsedValue == 5)
            {
                Deposit( ClientSocket);
                parsedValue = 0;
            }
            else if (parsedValue == 6)
            {
                Withdraw( ClientSocket);
                parsedValue = 0;
            }
            else if (parsedValue == 7)
            {
                Transfer( ClientSocket);
            }
            else if (parsedValue == 8)
            {
                ViewNotifications();
            }
            else if (parsedValue == 9)
            {
                string logout = "Logout" + "," + clientName;
                ClientSocket.Send(Encoding.ASCII.GetBytes(logout), logout.Length, SocketFlags.None);
                parsedValue = 0;
            }
            else if (parsedValue == 10)
            {
                Deactivate(ClientSocket);
                parsedValue = 0;
            }
           


        }

        private static void Deactivate(Socket clientSocket)
        {
            message = "Deactivate" + "," + clientName;
            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            getResponse(ClientSocket);
        }

        private static void ViewNotifications()
        {
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                   Notifications                ");
            Console.WriteLine("\n*****************************************************\n");
            foreach(string n in Notifications)
            {
                Console.WriteLine(n);
            }
            Console.Write("\nPress any number Go back to Main Menu: ");
            string input = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(input, out parsedValue))
            {
                Console.WriteLine("Error In Input");

                Console.Write("Press any number Go back to Main Menu: ");
                input = Console.ReadLine();
            }
            
                parsedValue = 0;
                Console.Clear();
                Heading();
                MainMenu(ClientSocket);
            


        }

        private static void Edit(Socket ClientSocket)
        {
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                   Edit Personal Details                ");
            Console.WriteLine("\n*****************************************************\n");

            Console.WriteLine("\n\n********************************************************\n\n");
            Console.WriteLine("       1.Edit Username            ");
            Console.WriteLine("       2.Edit Email              ");
            Console.WriteLine("       3.Edit Occupation         ");
            Console.WriteLine("       3.Edit Password         ");
            Console.WriteLine("\n\n*********************************************************\n\n");
            Console.Write("Enter Your Choice : ");
            menuChoice = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(menuChoice, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Try again");
                Console.Write("Enter Your choice : ");
                menuChoice = Console.ReadLine();
            }
            while (parsedValue != 1 && parsedValue != 2 && parsedValue != 3 && parsedValue != 4 )
            {
                Console.WriteLine("Invalid Choice... ");
                Console.Write("Enter Your choice : ");
                parsedValue = Int32.Parse(Console.ReadLine());
            }
            if (parsedValue == 1)
            {
                Console.Write("Enter new Username : ");
                string uname = Console.ReadLine();

                message = "EditUname" + "," + clientName+","+uname;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse( ClientSocket);
            }
            else if (parsedValue == 2)
            {
                Console.Write("Enter New Email : ");
                string email = Console.ReadLine();
                while (!email.Contains("@") || !email.Contains("."))
                {
                    Console.WriteLine("Invalid Email");
                    Console.Write("Email :");
                    email = Console.ReadLine();
                }
                message = "EditEmail" + "," + clientName+","+email;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse(ClientSocket);
            }
            else if (parsedValue == 3)
            {
                Console.Write("Enter new Occupation : ");
                string occupation = Console.ReadLine();
                message = "EditOccupation" + "," + clientName+","+ occupation;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse( ClientSocket);
            }
            else if (parsedValue == 4)
            {
                Console.Write("Enter New Password : ");
                string password = Console.ReadLine();
                message = "EditPassword" + "," + clientName+ "," + password;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse( ClientSocket);
            }
        }

        public static void Transfer(Socket ClientSocket)
        {
            Console.WriteLine("\n\n**************************************************** ");
            Console.WriteLine("\n                         Fund Transfer                ");
            Console.WriteLine("\n*****************************************************\n");

            Console.Write("Enter recepient's account number : ");
            string acc = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(acc, out parsedValue))
            {
                Console.WriteLine("Invalid Account Number");
                Console.Write("Enter recepient's account number : ");

                acc = Console.ReadLine();
            }
            Console.Write("Enter Transfer Amount : Rs.");
            string am = Console.ReadLine();
            double amount;
            while (!double.TryParse(am, out amount))
            {
                Console.WriteLine("Error In Input.Please Enter a valid amount");
                Console.Write("Deposit Amount : Rs.");
                am = Console.ReadLine();
            }
            message = "Transfer" + "," + clientName + "," + parsedValue + "," + amount;
            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            getResponse(ClientSocket);

        }

        public static void Withdraw(Socket ClientSocket)
        {
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                         Withdraw                ");
            Console.WriteLine("\n*****************************************************\n");
            Console.Write("Withdraw Amount : Rs.");
            string amount = Console.ReadLine();
            double parsedValue;
            while (!double.TryParse(amount, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Enter a valid amount");
                Console.Write("Deposit Amount : Rs.");
                amount = Console.ReadLine();
            }
             message = "Withdraw" + "," + clientName + "," + parsedValue;
            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            getResponse(ClientSocket);
        }

        public static void Deposit(Socket ClientSocket)
        {
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                         Deposit                ");
            Console.WriteLine("\n*****************************************************\n");
            Console.Write("Deposit Amount : Rs.");
            string amount = Console.ReadLine();
            double parsedValue;
            while (!double.TryParse(amount, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Enter a valid amount");
                Console.Write("Deposit Amount : Rs.");
                amount = Console.ReadLine();
            }
             message = "Deposit" + "," + clientName + "," + parsedValue;
            ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
            getResponse( ClientSocket);

        }

        public static void DisplayDetails(string response)
        {
            string[] details = response.Split('-');
            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                       Personal Profile                ");
            Console.WriteLine("\n                            "   +details[0]+                  "");


            Console.WriteLine("\n*****************************************************");
            Console.WriteLine("Name       : " + details[0]);
            Console.WriteLine("Nic Number : " + details[1]);
            Console.WriteLine("Gender     : " + details[2]);
            Console.WriteLine("Email      : " + details[3]);
            Console.WriteLine("Occupation : " + details[4]);
            Console.WriteLine("Usename    : " + details[5]);
            Console.WriteLine("Password   : " + details[6]);
            Console.Write("\nPress any number Go back to Main Menu: ");
            string input = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(input, out parsedValue))
            {
                Console.WriteLine("Error In Input");

                Console.Write("Press any number Go back to Main Menu: ");
                input = Console.ReadLine();
            }
            if (parsedValue < 10)
            {
                parsedValue = 0;
                Console.Clear();
                Heading();
                MainMenu(ClientSocket);
            }
           
            


        }

        public static void CreateAccount(Socket ClientSocket)
        {

            Console.WriteLine("\n\n**************************************************** ");

            Console.WriteLine("\n                       Create Account\n                    ");
            Console.WriteLine("\n                Start Your transactions Imediately        ");


            Console.WriteLine("\n*****************************************************");

            Random rnd = new Random();
            int num = rnd.Next(1, 100000);
            Console.WriteLine("Account Number : " + num);
            Console.WriteLine("Account type : SAVINGS");
            Console.WriteLine("Ininitial Deposit : 500\n");
            Console.WriteLine("-------- 1.Create Account--------");
            Console.WriteLine("------- -2.Terminate Process-----");
            Console.Write("\nEnter your choice : ");
             menuChoice = Console.ReadLine();
            int parsedValue;
            while (!int.TryParse(menuChoice, out parsedValue))
            {
                Console.WriteLine("Error In Input.Please Try again");
                Console.Write("Enter Your choice : ");
                menuChoice = Console.ReadLine();
            }
            while (parsedValue != 1 && parsedValue != 2 )
            {
                Console.WriteLine("Invalid Choice... ");
                Console.Write("Enter Your choice : ");
                menuChoice = Console.ReadLine();
            }
            if (parsedValue == 1)
            {
                string message = "CreateAccount" + "," + clientName + "," + num;
                ClientSocket.Send(Encoding.ASCII.GetBytes(message), message.Length, SocketFlags.None);
                getResponse(ClientSocket);
            }
            else
            {
                Console.Clear();
                Heading();
                MainMenu(ClientSocket);
            }

           
        }
    }
}
