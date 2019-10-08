using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    interface IClientControl
    {
        string displayDetails();
        void editDetails(string email,string occupation,string password);

        

    }
}
