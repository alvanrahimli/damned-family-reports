namespace FamilyReportBook.Models;

public class Answer
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public DateTime Timestamp { get; set; }

    public int UserId { get; set; }
    public AppUser User { get; set; } = default!;

    public int AnsweredToUserId { get; set; }
    public AppUser AnsweredToUser { get; set; } = default!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = default!;
}
