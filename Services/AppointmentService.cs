using System.Collections.Concurrent;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Services;

public interface IAppointmentService
{
    Appointment Add(Appointment appointment);
    Appointment? GetById(Guid id);
}

public class AppointmentService : IAppointmentService
{
    private readonly ConcurrentDictionary<Guid, Appointment> _store = new();

    public Appointment Add(Appointment appointment)
    {
        appointment.ReferenceNumber =
            $"CP-{DateTime.Now:yyMMdd}-{appointment.AppointmentId.ToString("N")[..4].ToUpperInvariant()}";
        _store[appointment.AppointmentId] = appointment;
        return appointment;
    }

    public Appointment? GetById(Guid id) => _store.TryGetValue(id, out var a) ? a : null;
}
