namespace SemesterProjekt_2.Models
{
    public class Blog
    {

        public int BlogID { get; set; }

        public string BlogPost { get; set; }

        public string Image { get; set;}

        public Member member { get; set; }

        public Blog()
        {

        }

        public Blog(int blogID,string blogPost,string image,Member Member)
        {
            BlogID= blogID;
            BlogPost= blogPost;
            Image = image;
            member= Member;
        }
    }
}
