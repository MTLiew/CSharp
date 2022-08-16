using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAssignment2
{
    public class Member : ObservableObject
    {
        private int TEXT_LIMIT = 35;
        private string fName;
        private string lName;
        private string email;

        public Member() { }
        
        public string FirstName
        {
            get
            {
                return fName;
            }
            set
            {
                if (value.Length > TEXT_LIMIT)
                {
                    throw new ArgumentException("String too long");
                }

                if (value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                fName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lName;
            }
            set
            {
                
                lName = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value.Length > TEXT_LIMIT)
                {
                    throw new ArgumentException("String too long");
                }

                if (value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                email = value;
            }
        }

        public static ObservableCollection<Member> GetExampleMembers()
        {
            ObservableCollection<Member> employees = new ObservableCollection<Member>();
            for (int i = 0; i < 30; ++i)
            {
                employees.Add(new Member
                {
                    FirstName = "FName" + i.ToString(),
                    LastName = "LName" + i.ToString(),
                    Email = "Email" + i.ToString()
                });
            }

            return employees;
        }

        public override string ToString()
        {
            return FirstName + ", " + LastName + ", " + Email;
        }
    }
}