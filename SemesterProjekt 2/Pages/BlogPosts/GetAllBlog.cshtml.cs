using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.BlogPosts
{
    public class GetAllBlogModel : PageModel
    {

        private IBlogService _blogService;

        public List<Blog> Posts { get; set; }

        public GetAllBlogModel(IBlogService blogService, List<Blog> posts)
        {
            _blogService = blogService;
        }


        public async Task OnGetAsync()
        {
            try
            {
                Posts = await _blogService.GetBlogPostsAsync();
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
              
        }
    }
}
