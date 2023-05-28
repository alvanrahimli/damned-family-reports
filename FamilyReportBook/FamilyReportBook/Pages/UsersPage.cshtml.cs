using FamilyReportBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyReportBook.Pages;

public class UsersPage : PageModel
{
    private readonly ReportBookContext _context;
    public List<AppUser> Users { get; set; } = new();
    [BindProperty] public NewUserDto NewUser { get; set; } = new();

    public UsersPage(ReportBookContext context)
    {
        _context = context;
    }
    
    public async Task OnGetAsync()
    {
        Users = await _context.Users.ToListAsync();
    }

    public async Task<ActionResult> OnPostCreateUserAsync()
    {
        await _context.Users.AddAsync(new AppUser
        {
            FamilyRole = NewUser.Role,
            Name = NewUser.Name
        });
        await _context.SaveChangesAsync();
        await OnGetAsync();
        return Page();
    }

    public class NewUserDto
    {
        public FamilyRole Role { get; set; } = FamilyRole.None;
        public string Name { get; set; } = default!;
    }
}
