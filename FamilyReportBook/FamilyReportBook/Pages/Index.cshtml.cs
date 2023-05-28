using FamilyReportBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FamilyReportBook.Pages;

public class IndexModel : PageModel
{
    private readonly ReportBookContext _context;
    private readonly ILogger<IndexModel> _logger;

    public List<Topic> Topics { get; set; } = new();
    public List<AppUser> Users { get; set; } = new();
    [BindProperty] public NewTopicDto NewTopic { get; set; }

    public IndexModel(ReportBookContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        Topics = await _context.Topics.OrderByDescending(t => t.Timestamp).Take(30).ToListAsync();
        Users = await _context.Users.ToListAsync();
        return Page();
    }

    public async Task<ActionResult> OnPostCreateTopicAsync()
    {
        await _context.Topics.AddAsync(new Topic
        {
            Title = NewTopic.Title,
            Timestamp = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            Answers = new List<Answer>
            {
                new()
                {
                    Text = NewTopic.Answer,
                    UserId = NewTopic.UserId,
                    AnsweredToUserId = NewTopic.AnsweredTo,
                    Timestamp = DateTime.UtcNow
                }
            }
        });
        await _context.SaveChangesAsync();
        await OnGetAsync();
        return Page();
    }

    public class NewTopicDto
    {
        public string Title { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int AnsweredTo { get; set; }
        public int UserId { get; set; }
    }
}
