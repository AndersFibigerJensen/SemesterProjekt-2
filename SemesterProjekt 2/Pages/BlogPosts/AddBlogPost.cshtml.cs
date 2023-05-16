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
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Member User { get; set; }

        [BindProperty]
        public Blog Post { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddBlogPostModel(IMemberService memberService, IBlogService blogService, IWebHostEnvironment webHostEnvironment)
        {
            _memberService = memberService;
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
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
                if (Image != null)
                {
                    if (Post.Image != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Post.Image);
                        System.IO.File.Delete(filePath);
                    }

                    Post.Image = ProcessUploadedFile();
                }
                Post.memberID = User.MemberID;
                _blogService.CreateBlogPostAsync(Post);
                return RedirectToPage("GetAllBlog");
            }
            catch(Exception ex) 
            {
                ViewData["Errormessage"] = ex.Message;
                return Page();

            }
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
