using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SemesterProjekt_2.Interfaces;
using SemesterProjekt_2.Models;
using SemesterProjekt_2.Services;

namespace SemesterProjekt_2.Pages.BlogPosts
{
    public class GetAllBlogModel : PageModel
    {

        private IBlogService _blogService;
        private IMemberService _memberService;

        [BindProperty]
        public Member User { get; set; }

        public List<Blog> Posts { get; set; }

        public IFormFile Image { get; set; }

        public GetAllBlogModel(IBlogService blogService, IMemberService memberService)
        {
            _memberService= memberService;
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
                Posts = await _blogService.GetBlogPostsAsync();
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;

            }
              
        }
    }
}
