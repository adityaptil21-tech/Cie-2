using HospitalAppointmentSystem.Extensions;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers;

public class AppointmentsController : Controller
{
    public const string PatientNameCookie = "PatientName";
    public const string SpecializationKey = "SelectedSpecialization";

    private readonly IDoctorService _doctors;
    private readonly IAppointmentService _appointments;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IDoctorService doctors, IAppointmentService appointments,
        ILogger<AppointmentsController> logger)
    {
        _doctors = doctors;
        _appointments = appointments;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Create(int? doctorId)
    {
        var model = new AppointmentFormViewModel
        {
            // Cookie: pre-fill the patient name saved from a previous booking
            PatientName = Request.Cookies[PatientNameCookie] ?? string.Empty
        };

        if (doctorId.HasValue)
        {
            var doctor = _doctors.GetById(doctorId.Value);
            if (doctor == null) return NotFound();

            model.DoctorId = doctor.DoctorId;
            // Session: store the selected doctor's specialization
            HttpContext.Session.SetString(SpecializationKey, doctor.Specialization);
        }

        PopulateDoctors(model);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentFormViewModel model)
    {
        // DoctorId comes from the hidden form field (or the dropdown if no doctor was pre-selected)
        var doctor = model.DoctorId.HasValue ? _doctors.GetById(model.DoctorId.Value) : null;
        if (model.DoctorId.HasValue && doctor == null)
            ModelState.AddModelError(nameof(model.DoctorId), "The selected doctor was not found.");

        if (!ModelState.IsValid || doctor == null)
        {
            PopulateDoctors(model);
            TempData["Error"] = "Please correct the highlighted errors and try again.";
            return View(model);
        }

        var appointment = _appointments.Add(new Appointment
        {
            PatientName = model.PatientName.Trim(),
            Age = model.Age!.Value,
            PhoneNumber = model.PhoneNumber,
            DoctorId = doctor.DoctorId,
            DoctorName = doctor.DoctorName,
            Specialization = doctor.Specialization,
            AppointmentDate = model.AppointmentDate!.Value.Date,
            AppointmentTime = model.AppointmentTime,
            Symptoms = model.Symptoms.Trim(),
            ConsultationFee = doctor.ConsultationFee,
            ServiceCharge = DoctorExtensions.ServiceCharge,
            TotalCharge = doctor.CalculateTotalCharge()
        });

        // Cookie: remember the patient's name for 30 days
        Response.Cookies.Append(PatientNameCookie, appointment.PatientName, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });
        HttpContext.Session.SetString(SpecializationKey, doctor.Specialization);

        _logger.LogInformation("Appointment {Ref} booked with doctor {DoctorId}", appointment.ReferenceNumber, doctor.DoctorId);
        TempData["Success"] = "Your appointment has been booked successfully!";
        return RedirectToAction(nameof(Confirmation), new { id = appointment.AppointmentId });
    }

    public IActionResult Confirmation(Guid id)
    {
        var appointment = _appointments.GetById(id);
        if (appointment == null) return NotFound();
        return View(appointment);
    }

    private void PopulateDoctors(AppointmentFormViewModel model)
    {
        model.Doctors = _doctors.GetAll();
        model.SelectedDoctor = model.DoctorId.HasValue ? _doctors.GetById(model.DoctorId.Value) : null;
    }
}
