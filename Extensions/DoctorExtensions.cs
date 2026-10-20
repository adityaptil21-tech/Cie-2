using System.Globalization;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Extensions;

public static class DoctorExtensions
{
    /// <summary>Flat hospital service charge in rupees.</summary>
    public const decimal ServiceCharge = 100m;

    /// <summary>Consultation fee + ₹100 service charge.</summary>
    public static decimal CalculateTotalCharge(this Doctor doctor)
        => doctor.ConsultationFee + ServiceCharge;

    private static readonly CultureInfo IndianCulture = CreateCulture();

    private static CultureInfo CreateCulture()
    {
        try { return new CultureInfo("en-IN"); }
        catch (CultureNotFoundException) { return CultureInfo.InvariantCulture; }
    }

    /// <summary>Formats a number as Indian rupees, e.g. ₹1,500.</summary>
    public static string ToRupees(this decimal amount)
        => "₹" + amount.ToString("N0", IndianCulture);
}
