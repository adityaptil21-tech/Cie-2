using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers;

public class HomeController : Controller
{
    private readonly IDoctorService _doctors;

    public HomeController(IDoctorService doctors) => _doctors = doctors;

    public IActionResult Index()
    {
        var model = new HomeViewModel
        {
            FeaturedDoctors = _doctors.GetAll().Take(3).ToList(),
            PatientName = Request.Cookies[AppointmentsController.PatientNameCookie],                    // cookie
            LastSpecialization = HttpContext.Session.GetString(AppointmentsController.SpecializationKey) // session
        };
        return View(model);
    }

    public IActionResult About() => View();

    [HttpGet]
    public IActionResult Contact() => View(new ContactMessage());

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Contact(ContactMessage model)
    {
        if (!ModelState.IsValid) return View(model);

        // Demo only: a real app would send an email or save to a database.
        TempData["Success"] = $"Thank you, {model.Name}! Your message has been received. We will reply soon.";
        return RedirectToAction(nameof(Contact));
    }

    public IActionResult Error(int? code) =>
        View(new ErrorViewModel { Code = code, RequestId = HttpContext.TraceIdentifier });
}
