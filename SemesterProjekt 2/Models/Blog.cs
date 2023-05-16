namespace SemesterProjekt_2.Models
{
    public class Blog
    {

        public int BlogID { get; set; }

        public string BlogPost { get; set; }

        public string Image { get; set;}

        public int memberID { get; set; }

        public Blog()
        {

        }

        public Blog(string blogPost,string image, int Member)
        {
            BlogPost = blogPost;
            Image = image;
            memberID = Member;
        }

        public Blog(int blogID,string blogPost,string image,int Member)
        {
            BlogID= blogID;
            BlogPost= blogPost;
            Image = image;
            memberID= Member;
        }
    }
}
