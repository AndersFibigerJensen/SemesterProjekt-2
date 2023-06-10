using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Interfaces
{
    public interface IBlogService
    {
        /// <summary>
        /// laver en ny blogpost i databasen
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>returnere en blogpost</returns>
        Task<Blog> CreateBlogPostAsync(Blog blog);

        /// <summary>
        /// opdatere en blogpost
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task<bool> UpdateBlogPostAsync(Blog blog);

        /// <summary>
        /// sletter en blog ud fra id
        /// </summary>
        /// <param name="BlogId">bruger BlogID til at finde den blog som skal slettes</param>
        /// <returns>returnere den blog som er slettet</returns>
        Task<Blog> DeleteBlogPostAsync(int BlogId);

        /// <summary>
        /// henter alle blogge i databasen
        /// </summary>
        /// <returns>returnere en liste af blogge</returns>
        Task<List<Blog>> GetBlogPostsAsync();

        /// <summary>
        /// henter en specifik blog ud fra dens unikke id
        /// </summary>
        /// <param name="id">bruger id til at finde bloggen</param>
        /// <returns> returnere en blog med et id der svarer til id i parameteren</returns>
        Task<Blog> GetBlogAsync(int id); 

    }
}
