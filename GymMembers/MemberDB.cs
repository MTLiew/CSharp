using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabAssignment2
{
    class MemberDB : ObservableObject
    {
        private ObservableCollection<Member> members;
        private const string filepath = "../../../members.txt";
        public MemberDB(ObservableCollection<Member> m)
        {
            members = m;
        }
        
        /// <summary>
        /// Reads the saved text file database into the program's list of members.
        /// </summary>
        /// <returns>The list containing the text file data read in.</returns>
        public ObservableCollection<Member> GetMemberships()
        {
            try
            {
                StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));
                var inputStringList = input.ReadToEnd().Trim().Split("\n");

                foreach (var member in inputStringList)
                {
                    var stringSplit = member.Split(",");
                    Member newMember = new Member();
                    newMember.FirstName = stringSplit[0].Trim();
                    newMember.LastName = stringSplit[1].Trim();
                    newMember.Email = stringSplit[2].Trim();
                    
                    members.Add(newMember);
                }
                
                Console.WriteLine(input.ReadLine());
                input.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid e-mail address format.");
            }
            return members;
        }
        
        /// <summary>
        /// Saves the program's list of members into the text file database.
        /// </summary>
        public void SaveMemberships()
        {
            StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));

            foreach (var member in members)
            {
                output.WriteLine(member.ToString().Trim());
            }

            output.Close();
        }
    }
}