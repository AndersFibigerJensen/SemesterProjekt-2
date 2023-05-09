using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{
    public interface IBlogService
    {

        Task<Blog> CreateBlogPostAsync(Blog blog);

        Task<bool> UpdateBlogPostAsync(Blog blog);

        Task<Blog> DeleteBlogPostAsync(int BlogId);

        Task<List<Blog>> GetBlogPostsAsync();

        Task<Blog> GetBlogAsync(int id); 

    }
}
