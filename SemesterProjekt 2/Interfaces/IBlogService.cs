using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{
    public interface IBlogService
    {

        Task<Blog> CreateBlogPost();

        Task UpdateBlogPost();

        Task DeleteBlogPost();

        Task<List<Blog>> GetBlogPosts();

        Task<Blog> GetBlog(int id); 

    }
}
