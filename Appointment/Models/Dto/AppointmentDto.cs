using System.ComponentModel.DataAnnotations;

namespace Appointment.Models.Dto
{
    public class AppointmentDto
    {
        [Required]
        public string DoctorId { get; set; } = string.Empty;

        [Required]
        public string PatientId { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan AppointmentTime { get; set; }

        public string Symptoms { get; set; } = string.Empty;
    }
}
