namespace HospitalAppointmentSystem.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int Experience { get; set; }              // years
    public decimal ConsultationFee { get; set; }     // in Indian rupees

    // Extra display information
    public string Qualification { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string AvailableDays { get; set; } = string.Empty;
    public string IconClass { get; set; } = "bi-person-heart";
}
