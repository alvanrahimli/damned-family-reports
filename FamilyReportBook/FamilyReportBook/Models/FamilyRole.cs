namespace FamilyReportBook.Models;

public enum FamilyRole
{
    None, Mother, Father, Son, Doughter, Grandpa, Grandma, Outsider
}

public static class FamilyRoleExtensions
{
    public static string TranslateTo(this FamilyRole role, string lang)
    {
        if (lang == "az")
        {
            return role switch
            {
                FamilyRole.None => "--",
                FamilyRole.Mother => "Ana",
                FamilyRole.Father => "Ata",
                FamilyRole.Son => "Oğul",
                FamilyRole.Doughter => "Qız",
                FamilyRole.Grandpa => "Baba",
                FamilyRole.Grandma => "Nənə",
                FamilyRole.Outsider => "Başqası",
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
        
        return "No translation";
    }
}
