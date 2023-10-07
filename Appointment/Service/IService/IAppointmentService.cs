using AppointmentManagement.Models;
using AppointmentManagement.Models.Dto;
namespace AppointmentManagement.Service.IService
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment1>> GetAllAppointmentsAsync();
        Task<Appointment1> GetAppointmentByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment1>> GetAppointmentsForDoctorsAsync(Guid doctorId);
        Task<IEnumerable<Appointment1>> GetAppointmentsForPatientsAsync(Guid patientId);
        Task<Guid> CreateAppointmentAsync(AppointmentDto appointmentDto);
        Task<bool> UpdateAppointmentAsync(int appointmentId, AppointmentDto appointmentDto);
        Task<bool> DeleteAppointmentAsync(int appointmentId);

    }
}
