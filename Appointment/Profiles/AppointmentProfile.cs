using AppointmentManagement.Models;
using AppointmentManagement.Models.Dto;
using AutoMapper;

namespace AppointmentManagement.Profiles
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile() 
        {
            CreateMap<AppointmentRequestDto, Appointment1>().ReverseMap();
        }
    }
}
