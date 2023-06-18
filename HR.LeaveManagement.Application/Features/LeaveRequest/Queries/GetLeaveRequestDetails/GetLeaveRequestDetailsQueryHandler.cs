using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler 
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestDetailsQuery> _logger;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestDetailsQuery> logger) 
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._mapper = mapper;
        this._logger = logger;
    }
    public async Task<LeaveRequestDetailsDto> Handle(
        GetLeaveRequestDetailsQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        var data = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        // Add employee details as needed

        return data;
    }
}
