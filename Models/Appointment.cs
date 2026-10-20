namespace HospitalAppointmentSystem.Models;

/// <summary>A booked appointment (stored in memory). Doctor details and charges are snapshotted.</summary>
public class Appointment
{
    public Guid AppointmentId { get; set; } = Guid.NewGuid();
    public string ReferenceNumber { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string AppointmentTime { get; set; } = string.Empty;
    public string Symptoms { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal TotalCharge { get; set; }
    public DateTime BookedOn { get; set; } = DateTime.Now;
}
