using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }
    public async Task<Unit> Handle(
        UpdateLeaveAllocationCommand request, 
        CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateLeaveAllocationCommandValidator(
            _leaveTypeRepository, 
            _leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}",
                typeof(Domain.LeaveAllocation), request.Id); // Don't use string interpolation
            throw new BadRequestException("Invalid leave allocation request", validationResult);
        }

        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null) 
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        // convert request to domain entity object
        _mapper.Map(request, leaveAllocation);

        // Add to database
        await _leaveAllocationRepository.UpdatingAsync(leaveAllocation);
        return Unit.Value;
    }
}
