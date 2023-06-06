using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler
    : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger;

    public DeleteLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
    {
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._logger = logger;
    }
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null) 
        {
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        }

        await _leaveAllocationRepository.DeletingAsync(leaveAllocation);
        return Unit.Value;
    }
}
