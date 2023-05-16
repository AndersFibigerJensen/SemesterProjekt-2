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
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Blog Post { get; set; }

        public IFormFile Image { get; set; }

        [BindProperty]
        public Member User { get; set; }

        public UpdateBlogPostModel(IMemberService memberService, IBlogService blogService,IWebHostEnvironment webhost)
        {
            _memberService = memberService;
            _blogService = blogService;
            _webHostEnvironment = webhost;
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
                return RedirectToPage("/Members/HomePage");
            }
            catch (Exception ex) 
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
