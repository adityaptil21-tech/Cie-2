using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models;

public class HomeViewModel
{
    public IReadOnlyList<Doctor> FeaturedDoctors { get; set; } = Array.Empty<Doctor>();
    public string? PatientName { get; set; }          // from cookie
    public string? LastSpecialization { get; set; }   // from session
}

public class DoctorListViewModel
{
    public IReadOnlyList<Doctor> Doctors { get; set; } = Array.Empty<Doctor>();
    public IReadOnlyList<string> Specializations { get; set; } = Array.Empty<string>();
    public string? SelectedSpecialization { get; set; }
}

public class PostsViewModel
{
    public IReadOnlyList<Post> Posts { get; set; } = Array.Empty<Post>();
    public string? ErrorMessage { get; set; }
}

public class ContactMessage
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a subject.")]
    [StringLength(120)]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please write your message.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be at least 10 characters.")]
    public string Message { get; set; } = string.Empty;
}

public class ErrorViewModel
{
    public int? Code { get; set; }
    public string? RequestId { get; set; }
}
