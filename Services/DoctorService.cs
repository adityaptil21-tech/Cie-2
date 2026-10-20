using HospitalAppointmentSystem.Models;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalAppointmentSystem.Services;

public interface IDoctorService
{
    IReadOnlyList<Doctor> GetAll();
    Doctor? GetById(int id);
}

public class DoctorService : IDoctorService
{
    private const string CacheKey = "doctor-list";
    private readonly IMemoryCache _cache;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IMemoryCache cache, ILogger<DoctorService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    /// <summary>Returns the doctor list, cached in memory for 3 minutes.</summary>
    public IReadOnlyList<Doctor> GetAll()
    {
        return _cache.GetOrCreate(CacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3);
            _logger.LogInformation("Doctor list loaded and cached for 3 minutes.");
            return LoadDoctors();
        })!;
    }

    public Doctor? GetById(int id) => GetAll().FirstOrDefault(d => d.DoctorId == id);

    // Sample in-memory data (replace with a database later)
    private static List<Doctor> LoadDoctors() => new()
    {
        new Doctor { DoctorId = 1, DoctorName = "Dr. Ananya Sharma", Specialization = "Cardiologist", Experience = 15, ConsultationFee = 800,
            Qualification = "MBBS, MD, DM (Cardiology)", IconClass = "bi-heart-pulse", AvailableDays = "Mon – Sat, 9:00 AM – 5:00 PM",
            About = "Dr. Ananya Sharma is a senior interventional cardiologist known for her work in preventive cardiology, heart failure management and non-invasive cardiac imaging." },
        new Doctor { DoctorId = 2, DoctorName = "Dr. Rajesh Patel", Specialization = "Neurologist", Experience = 12, ConsultationFee = 700,
            Qualification = "MBBS, MD, DM (Neurology)", IconClass = "bi-activity", AvailableDays = "Mon – Fri, 10:00 AM – 4:00 PM",
            About = "Dr. Rajesh Patel specialises in migraines, epilepsy, stroke rehabilitation and movement disorders with a patient-first approach." },
        new Doctor { DoctorId = 3, DoctorName = "Dr. Priya Mehta", Specialization = "Pediatrician", Experience = 9, ConsultationFee = 500,
            Qualification = "MBBS, MD (Pediatrics)", IconClass = "bi-emoji-smile", AvailableDays = "Mon – Sat, 9:00 AM – 3:00 PM",
            About = "Dr. Priya Mehta provides gentle, friendly care for infants, children and teenagers, including vaccinations and growth monitoring." },
        new Doctor { DoctorId = 4, DoctorName = "Dr. Vikram Singh", Specialization = "Orthopedic Surgeon", Experience = 18, ConsultationFee = 900,
            Qualification = "MBBS, MS (Orthopedics)", IconClass = "bi-bandaid", AvailableDays = "Tue – Sat, 10:00 AM – 5:00 PM",
            About = "Dr. Vikram Singh is an experienced surgeon focusing on joint replacement, sports injuries and fracture management." },
        new Doctor { DoctorId = 5, DoctorName = "Dr. Neha Desai", Specialization = "Dermatologist", Experience = 8, ConsultationFee = 600,
            Qualification = "MBBS, MD (Dermatology)", IconClass = "bi-droplet-half", AvailableDays = "Mon – Fri, 11:00 AM – 6:00 PM",
            About = "Dr. Neha Desai treats skin, hair and nail conditions and offers advanced cosmetic dermatology consultations." },
        new Doctor { DoctorId = 6, DoctorName = "Dr. Arjun Nair", Specialization = "General Physician", Experience = 10, ConsultationFee = 400,
            Qualification = "MBBS, MD (General Medicine)", IconClass = "bi-person-heart", AvailableDays = "Mon – Sat, 8:00 AM – 8:00 PM",
            About = "Dr. Arjun Nair is the first point of contact for everyday illnesses, health check-ups and chronic disease management." },
        new Doctor { DoctorId = 7, DoctorName = "Dr. Kavita Joshi", Specialization = "Gynecologist", Experience = 14, ConsultationFee = 750,
            Qualification = "MBBS, MS (Obstetrics & Gynecology)", IconClass = "bi-gender-female", AvailableDays = "Mon – Sat, 10:00 AM – 4:00 PM",
            About = "Dr. Kavita Joshi offers compassionate care in pregnancy, childbirth, fertility and women's wellness." },
        new Doctor { DoctorId = 8, DoctorName = "Dr. Sanjay Iyer", Specialization = "ENT Specialist", Experience = 11, ConsultationFee = 550,
            Qualification = "MBBS, MS (ENT)", IconClass = "bi-soundwave", AvailableDays = "Mon – Fri, 9:30 AM – 5:30 PM",
            About = "Dr. Sanjay Iyer diagnoses and treats ear, nose and throat conditions including sinus problems, hearing loss and tonsil issues." }
    };
}
