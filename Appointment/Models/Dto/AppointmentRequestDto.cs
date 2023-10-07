namespace AppointmentManagement.Models.Dto
{
    public class AppointmentRequestDto
    {
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName {  get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public AppointmentTimeDto AppointmentTime { get; set; }
        public string Symptoms { get; set; } = string.Empty;
    }
   
}
