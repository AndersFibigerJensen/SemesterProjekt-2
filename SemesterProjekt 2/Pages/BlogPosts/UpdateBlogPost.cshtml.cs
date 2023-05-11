using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.BlogPosts
{
    public class UpdateBlogPostModel : PageModel
    {
        
        private IMemberService _memberService;
        private IBlogService _blogService;

        [BindProperty]
        public Blog Post { get; set; }

        public IFormFile Image { get; set; }

        public Member User { get; set; }

        public UpdateBlogPostModel(IMemberService memberService, IBlogService blogService)
        {
            _memberService = memberService;
            _blogService = blogService;
        }

        public async Task OnGetAsync(int id)
        {
            try
            {
                Post = await _blogService.GetBlogAsync(id);
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
            }
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _blogService.UpdateBlogPostAsync(Post);
                return RedirectToPage("GetAllBlog");
            }
            catch (Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }
            
        }
    }
}
