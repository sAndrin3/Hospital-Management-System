﻿namespace AppointmentManagement.Models
{
    public class Appointment1
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Symptoms { get; set; }
    }
}
