using System.Net;

namespace SemesterProjekt_2.Models
{
    public class Member
    {
        //Anders

        public int MedlemID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsFamily { get; set; }

        public List<Shift> Shifts { get; set; }

        public bool IsAdmin  { get; set; }

        public bool HasDoneHygieneCourse { get; set; }

        public Member()
        {
        }

        public Member(int MemberID,string name,string password,string email,string address, bool isFamily, bool hasDoneHygieneCourse, bool isAdmin)
        {
            MedlemID = MemberID;
            Name = name;
            Address=address;
            Email = email;
            IsFamily= isFamily;
            HasDoneHygieneCourse=hasDoneHygieneCourse;
            Password = password;
            

        }

    }
}
