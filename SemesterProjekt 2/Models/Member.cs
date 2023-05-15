using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.InteropServices;

namespace SemesterProjekt_2.Models
{
    public class Member
    {
        //Anders
        [Required]
        public int MemberID{ get; set; }

        [Required(ErrorMessage = "indtast venligst et navn")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "indtast venligst en addresse")]
        [StringLength(30)]
        public string Address { get; set; }

        [Required(ErrorMessage ="indtast venligst en email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                            ErrorMessage = "Please enter a valid email address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required(ErrorMessage ="indtast venligst et password")]
        [StringLength(30)]
        public string Password { get; set; }

        [Required]
        public bool IsFamily { get; set; }

        public List<Shift> Shifts { get; set; }

        public bool IsAdmin  { get; set; }

        public bool HasDoneHygieneCourse { get; set; }


        public string Image { get; set; }

        public Member()
        {
            Shifts = new List<Shift>();
            IsAdmin= false;
            HasDoneHygieneCourse= false;
        }

        public Member(int memberID, string name,string password,string email,string address, bool isFamily, bool hasDoneHygieneCourse, bool isAdmin, string image)
        {
            MemberID = memberID;
            Name = name;
            Address=address;
            Email = email;
            IsFamily= isFamily;
            HasDoneHygieneCourse=hasDoneHygieneCourse;
            Password = password;
            IsAdmin=isAdmin;
            Image = image;
            Shifts= new List<Shift>();
            

        }

        //for unit testing
        public Member(string name, string password, string email, string address, bool isFamily, bool hasDoneHygieneCourse, bool isAdmin)
        {
            Name = name;
            Address = address;
            Email = email;
            IsFamily = isFamily;
            HasDoneHygieneCourse = hasDoneHygieneCourse;
            Password = password;
            IsAdmin = isAdmin;
            Shifts = new List<Shift>();

        }

    }
}
