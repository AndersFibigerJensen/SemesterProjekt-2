namespace SemesterProjekt_2.Models
{
    public class Member
    {

        public int MedlemID { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool isFamily { get; set; }

        public List<Shift> shifts { get; set; }

        public bool isAdmin  { get; set; }

        public Member()
        {

        }

    }
}
