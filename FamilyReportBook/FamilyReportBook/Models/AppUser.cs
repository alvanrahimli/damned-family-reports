namespace FamilyReportBook.Models;

public class AppUser
{
    public int Id { get; set; }
    public FamilyRole FamilyRole { get; set; } = FamilyRole.None;
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
    
    public ICollection<Answer> GivenAnswers { get; set; } = new List<Answer>();
    public ICollection<Answer> ReceivedAnswers { get; set; } = new List<Answer>();
}
