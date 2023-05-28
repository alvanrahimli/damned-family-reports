using System.Security.Claims;
using FamilyReportBook.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyReportBook.Pages;

public class LoginPage : PageModel
{
    private readonly ReportBookContext _context;

    public LoginState State { get; set; } = LoginState.None;
    public List<AppUser> Users { get; set; } = new();
    [BindProperty] public LoginDto Login { get; set; } = new();

    public LoginPage(ReportBookContext context)
    {
        _context = context;
    }
    
    public async Task<ActionResult> OnGetAsync()
    {
        Users = await _context.Users.ToListAsync();
        return Page();
    }

    public async Task<ActionResult> OnPostLoginAsync()
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Login.UserId && u.Password == Login.Password);
        if (user is null)
        {
            await OnGetAsync();
            State = LoginState.Failure;
            return Page();
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.FamilyRole.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
                { IsPersistent = Login.RememberMe });
        await OnGetAsync();
        return RedirectToPage("/Index");
    }

    public class LoginDto
    {
        public int UserId { get; set; }
        public string Password { get; set; } = default!;
        public bool RememberMe { get; set; } = true;
    }

    public enum LoginState
    {
        None, Success, Failure
    }
}
