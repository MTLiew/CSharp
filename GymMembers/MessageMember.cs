using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LabAssignment2
{
    public class MessageMember : Member
    {
        public MessageMember(string fName, string lName, string mail, string message)
        {
            this.FirstName = fName;
            this.LastName = lName;
            this.Email = mail;
            Message = message;
        }

        public string Message { get; private set; }
    }
}
