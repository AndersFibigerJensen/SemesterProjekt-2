using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.BlogPosts
{
    public class DeleteBlogPostModel : PageModel
    {
        private IBlogService _blogservice;
        private IMemberService _memberService;

        public Member User { get; set; }

        [BindProperty]
        public Blog Post { get; set; }

        public DeleteBlogPostModel(IBlogService blogservice, IMemberService memberService)
        {
            _blogservice = blogservice;
            _memberService = memberService;
        }

        public async Task OnGetAsync(int id)
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
                Post = await _blogservice.GetBlogAsync(id);
            }
            catch (Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
            }
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _blogservice.DeleteBlogPostAsync(Post.memberID);
                return RedirectToAction("GetAllBlog");
            }
            catch(Exception ex)
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();
            }
        }
    }
}
