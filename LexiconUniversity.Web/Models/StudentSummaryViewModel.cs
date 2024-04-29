using LexiconUniversity.Core.Entities;

namespace LexiconUniversity.Web.Models;

public class StudentSummaryViewModel
{
    public int Id { get; init; }
    public string Avatar { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public StudentSummaryViewModel() { }

    public StudentSummaryViewModel(int id, string avatar, string fullName, string email)
    {
        Id = id;
        Avatar = avatar;
        FullName = fullName;
        Email = email;
    }

    public StudentSummaryViewModel(int id, string avatar, string firstName, string lastName, string email)
    {
        Id = id;
        Avatar = avatar;
        FullName = $"{firstName} {lastName}";
        Email = email;
    }

    public StudentSummaryViewModel(Student student)
    {
        Id = student.Id;
        Avatar = student.Avatar;
        FullName = student.FullName;
        Email = student.Email;
    }
}
