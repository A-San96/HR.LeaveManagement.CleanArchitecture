using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

    public CreateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<CreateLeaveAllocationCommandHandler> logger)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }
    public async Task<int> Handle(
        CreateLeaveAllocationCommand request, 
        CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid leave allocation request", 
                validationResult);

        // Get Leave Type for Allocation
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get Employees

        // Get Period

        //Assign Allocations
        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
        await _leaveAllocationRepository.CreatingAsync(leaveAllocation);
        return leaveAllocation.Id;

    }
}
