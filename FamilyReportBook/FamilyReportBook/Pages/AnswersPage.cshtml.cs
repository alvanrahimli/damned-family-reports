using FamilyReportBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyReportBook.Pages;

public class AnswersPage : PageModel
{
    private readonly ReportBookContext _context;

    public List<Answer> Answers { get; set; } = new();
    public List<AppUser> Users { get; set; } = new();
    [BindProperty(SupportsGet = true)] public int TopicId { get; set; }
    [BindProperty] public NewAnswerDto NewAnswer { get; set; } = new();

    public AnswersPage(ReportBookContext context)
    {
        _context = context;
    }
    
    public async Task<ActionResult> OnGetAsync()
    {
        Answers = await _context.Answers.Where(a => a.TopicId == TopicId)
            .OrderByDescending(a => a.Timestamp)
            .ToListAsync();
        Users = await _context.Users.ToListAsync();
        return Page();
    }

    public async Task<ActionResult> OnPostCreateAnswerAsync()
    {
        await _context.Answers.AddAsync(new Answer
        {
            UserId = NewAnswer.UserId,
            AnsweredToUserId = NewAnswer.AnsweredToUserId,
            Text = NewAnswer.Text,
            Timestamp = DateTime.UtcNow,
            TopicId = NewAnswer.TopicId
        });
        await _context.SaveChangesAsync();
        await OnGetAsync();
        return Page();
    }

    public class NewAnswerDto
    {
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public int AnsweredToUserId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
