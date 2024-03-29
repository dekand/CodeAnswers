﻿using CodeAnswers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeAnswers.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly CodeAnswers.Data.ApplicationDbContext _context;

        public ProfileModel(CodeAnswers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Users Users { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int id = _context.Users.FirstOrDefault(u => u.Name == User.FindFirstValue(ClaimTypes.Name)).Id;
            if (id == 0)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(c => c.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            Users = users;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync([Bind("Id,Name,Email,RegistrationDate,Location,LinkSocial,LinkGithub,About")] Users users)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Users.About = users.About;
            Users.LinkGithub = users.LinkGithub;
            Users.LinkSocial = users.LinkSocial;
            Users.Location = users.Location;

            _context.Attach(Users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(Users.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
