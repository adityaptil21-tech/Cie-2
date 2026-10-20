using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HospitalAppointmentSystem.Models;

public class AppointmentFormViewModel : IValidatableObject
{
    public static readonly string[] TimeSlots =
    {
        "09:00 AM", "09:30 AM", "10:00 AM", "10:30 AM", "11:00 AM", "11:30 AM",
        "12:00 PM", "02:00 PM", "02:30 PM", "03:00 PM", "03:30 PM", "04:00 PM",
        "04:30 PM", "05:00 PM"
    };

    [Display(Name = "Patient Name")]
    [Required(ErrorMessage = "Please enter the patient's name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 80 characters.")]
    public string PatientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter the patient's age.")]
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int? Age { get; set; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Please enter a phone number.")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Enter a valid 10-digit Indian mobile number.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Doctor")]
    [Required(ErrorMessage = "Please select a doctor.")]
    public int? DoctorId { get; set; }

    [Display(Name = "Appointment Date")]
    [Required(ErrorMessage = "Please choose an appointment date.")]
    [DataType(DataType.Date)]
    public DateTime? AppointmentDate { get; set; }

    [Display(Name = "Appointment Time")]
    [Required(ErrorMessage = "Please choose a time slot.")]
    public string AppointmentTime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please describe the symptoms.")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Symptoms must be between 5 and 500 characters.")]
    public string Symptoms { get; set; } = string.Empty;

    // Display-only data (not posted / validated)
    [ValidateNever] public Doctor? SelectedDoctor { get; set; }
    [ValidateNever] public IReadOnlyList<Doctor> Doctors { get; set; } = Array.Empty<Doctor>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AppointmentDate.HasValue && AppointmentDate.Value.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "Appointment date cannot be in the past.", new[] { nameof(AppointmentDate) });
        }
        if (!string.IsNullOrEmpty(AppointmentTime) && !TimeSlots.Contains(AppointmentTime))
        {
            yield return new ValidationResult(
                "Please choose a valid time slot.", new[] { nameof(AppointmentTime) });
        }
    }
}
