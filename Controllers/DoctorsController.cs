using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers;

public class DoctorsController : Controller
{
    private readonly IDoctorService _doctors;

    public DoctorsController(IDoctorService doctors) => _doctors = doctors;

    public IActionResult Index(string? specialization)
    {
        var all = _doctors.GetAll();
        var list = string.IsNullOrWhiteSpace(specialization)
            ? all
            : all.Where(d => d.Specialization == specialization).ToList();

        if (!string.IsNullOrWhiteSpace(specialization))
        {
            // Session: remember the specialization the patient is interested in
            HttpContext.Session.SetString(AppointmentsController.SpecializationKey, specialization);
        }

        return View(new DoctorListViewModel
        {
            Doctors = list,
            Specializations = all.Select(d => d.Specialization).Distinct().OrderBy(s => s).ToList(),
            SelectedSpecialization = specialization
        });
    }

    // DoctorId arrives through the query string: /Doctors/Details?doctorId=1
    // Response caching: cached for 60 seconds, separately for each doctorId value.
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "doctorId" })]
    public IActionResult Details(int doctorId)
    {
        var doctor = _doctors.GetById(doctorId);
        if (doctor == null) return NotFound();
        return View(doctor);
    }
}
