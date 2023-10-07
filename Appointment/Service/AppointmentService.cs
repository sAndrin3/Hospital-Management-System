using AppointmentManagement.Data;
using AppointmentManagement.Models;
using AppointmentManagement.Models.Dto;
using AppointmentManagement.Service.IService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AppointmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            try
            {
                var appointment = _mapper.Map<Appointment1>(appointmentDto);
                _context.Appointment1s.Add(appointment);
                await _context.SaveChangesAsync();
                return appointment.AppointmentId;
            } catch (Exception ex)
            {
                throw new ApplicationException("Error creating appointment", ex);
            }
            
        }

        public async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            try
            {
                var appointmentToDelete = await _context.Appointment1s.FindAsync(appointmentId);
                if(appointmentToDelete == null)
                {
                    return false;
                }
                _context.Appointment1s.Remove(appointmentToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting appointment", ex);
            }
        }

        public async Task<Appointment1> GetAppointmentByIdAsync(int appointmentId)
        {
            try
            {
                var appointment = await _context.Appointment1s.FindAsync(appointmentId);
                if (appointment == null)
                {
                    return null;
                }
                return appointment;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error getting appointment by ID", ex);
            }
        }

        public async Task<IEnumerable<Appointment1>> GetAllAppointmentsAsync()
        {
            try
            {
                var appointment = await _context.Appointment1s.ToListAsync();
                return appointment;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error getting all appointments", ex);
            }
        }

        public async Task<IEnumerable<Appointment1>> GetAppointmentsForDoctorsAsync(Guid doctorId)
        {
            try
            {
                var appointments = await _context.Appointment1s
                    .Where(appointment => appointment.DoctorId == doctorId)
                    .ToListAsync();

                return appointments;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting appointment for doctor", ex);
            }
        }

        public async Task<IEnumerable<Appointment1>> GetAppointmentsForPatientsAsync(Guid patientId)
        {
            try
            {
                var appointments = await _context.Appointment1s
                    .Where(appointment => appointment.PatientId == patientId)
                    .ToListAsync();

                return appointments;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error getting appointments for patient", ex);
            }
        }

        public async Task<bool> UpdateAppointmentAsync(int appointmentId, AppointmentDto appointmentDto)
        {
            try
            {
                var appointmentToUpdate = await _context.Appointment1s.FindAsync(appointmentId);
                if (appointmentToUpdate == null)
                {
                    return false;
                }
                appointmentToUpdate.AppointmentDate = appointmentDto.AppointmentDate;
                appointmentToUpdate.AppointmentTime = appointmentDto.AppointmentTime;
                appointmentToUpdate.Symptoms = appointmentDto.Symptoms;

                _context.Appointment1s.Update(appointmentToUpdate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating appointment", ex);
            }
        }
    }
}
