namespace FamilyReportBook.Models;

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Timestamp { get; set; }
    public DateTime LastModified { get; set; }

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
