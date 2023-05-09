using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;

namespace SemesterProjekt_2.Pages.BlogPosts
{
    public class AddBlogPostModel : PageModel
    {
        private IMemberService _memberService;
        private IBlogService _blogService;

        public Member User { get; set; }

        [BindProperty]
        public Blog Blog { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddBlogPostModel(IMemberService memberService, IBlogService blogService)
        {
            _memberService = memberService;
            _blogService = blogService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                string username = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (username != null)
                {
                    User = await _memberService.LoginMemberAsync(username, password);
                }
                else
                {
                    User = await _memberService.GetMemberByIdAsync(-1);
                }
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
        }

        public async Task<IActionResult> OnpostAsync()
        {
            try
            {
                _blogService.CreateBlogPostAsync(Blog);
                return RedirectToPage("GetAllBlog");
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }
        }
    }
}
