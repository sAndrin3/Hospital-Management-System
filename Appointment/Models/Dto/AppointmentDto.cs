using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.Dto
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
        public AppointmentTimeDto AppointmentTime { get; set; }

        public string Symptoms { get; set; } = string.Empty;
    }
    public class AppointmentTimeDto
    {
        public TimeSpan Ticks { get; set; }
    }
}
