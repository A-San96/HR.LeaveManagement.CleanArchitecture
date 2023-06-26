using AutoMapper;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.MappingProfile;

public class MappingConfig : Profile
{
	public MappingConfig()
	{
		CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
		CreateMap<LeaveTypeDetailsDto, LeaveTypeViewModel>();
		CreateMap<LeaveTypeViewModel, CreateLeaveTypeCommand>();
		CreateMap<LeaveTypeViewModel, UpdateLeaveTypeCommand>();
    }
}
