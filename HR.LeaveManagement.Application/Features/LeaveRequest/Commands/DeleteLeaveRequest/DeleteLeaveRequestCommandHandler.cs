using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveRequestRepository;
    private readonly IAppLogger<DeleteLeaveRequestCommand> _logger;

    public DeleteLeaveRequestCommandHandler(
        ILeaveTypeRepository leaveRequestRepository,
        IAppLogger<DeleteLeaveRequestCommand> logger)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._logger = logger;
    }
    public async Task<Unit> Handle(
        DeleteLeaveRequestCommand request, 
        CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null) 
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        await _leaveRequestRepository.DeletingAsync(leaveRequest);

        return Unit.Value;
    }
}
