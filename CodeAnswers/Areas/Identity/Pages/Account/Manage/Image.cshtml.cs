using CodeAnswers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeAnswers.Areas.Identity.Pages.Account.Manage
{
    public class ImageModel : PageModel
    {
        private readonly CodeAnswers.Data.ApplicationDbContext _context;
        public readonly IWebHostEnvironment _appEnvironment;

        public ImageModel(CodeAnswers.Data.ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [BindProperty]
        public Images Images { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int id = (await _context.Users.FirstOrDefaultAsync(u => u.Name == User.FindFirstValue(ClaimTypes.Name))).Id;

            if (id == 0)
            {
                return NotFound();
            }

            var images = await _context.Images.FirstOrDefaultAsync(m => m.UserId == id);
            if (images == null)
            {
                return NotFound();
            }
            Images = images;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile uploadedFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (uploadedFile != null && uploadedFile.Name != null &&
                 (uploadedFile.ContentType.ToLower() == "image/jpeg"
               || uploadedFile.ContentType.ToLower() == "image/png"
               || uploadedFile.ContentType.ToLower() == "image/gif"))
            {
                //max size 10MB 
                if (uploadedFile.Length > 10000000) { return Page(); }

                var newName = Guid.NewGuid().ToString() + FileType(uploadedFile);
                try
                {
                    //save image
                    using var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath + "/img/", newName), FileMode.Create);
                    await uploadedFile.CopyToAsync(fileStream);
                }
                catch { return NotFound(); }
                Images.Name = newName;
                Images.ImagePath = "/img/" + newName;
            }

            _context.Attach(Images).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesExists(Images.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Image");
        }

        private bool ImagesExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
        static private string FileType(IFormFile uploadedFile)
        {
            switch (uploadedFile.ContentType.ToLower())
            {
                case "image/png": return ".png";
                case "image/gif": return ".gif";
            }
            return "image/jpeg";
        }
    }
}
