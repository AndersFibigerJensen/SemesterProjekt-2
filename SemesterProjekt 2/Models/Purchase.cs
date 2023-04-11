namespace SemesterProjekt_2.Models
{
    public class Purchase
    {
        //Adam

        public int EventID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool isMemberRequired { get; set; }

        public List<Member> SignUpList { get; set; }

        public Purchase()
        {

        }
    }
}
