using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SemesterProjekt_2.Models
{
    public class Member
    {
        //Anders
        [Required]
        public int MemberID{ get; set; }

        [Required(ErrorMessage ="du skal give et navn")]
        public string Name { get; set; }

        [Required(ErrorMessage ="du skal give en addresse")]
        public string Address { get; set; }

        [Required(ErrorMessage ="du skal give en email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="du skal give et password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "du skal vælge om du er en del af en familie eller enkel person ")]
        public bool IsFamily { get; set; }

        public List<Shift> Shifts { get; set; }

        public bool IsAdmin  { get; set; }

        public bool HasDoneHygieneCourse { get; set; }

        public Member()
        {
            Shifts = new List<Shift>();
            IsAdmin= false;
            HasDoneHygieneCourse= false;
        }

        public Member(int memberID, string name,string password,string email,string address, bool isFamily, bool hasDoneHygieneCourse, bool isAdmin)
        {
            MemberID = memberID;
            Name = name;
            Address=address;
            Email = email;
            IsFamily= isFamily;
            HasDoneHygieneCourse=hasDoneHygieneCourse;
            Password = password;
            IsAdmin=isAdmin;
            Shifts= new List<Shift>();
            

        }

    }
}
