using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;

        public GetLeaveRequestListQueryHandler(
            ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            IAppLogger<GetLeaveRequestListQueryHandler> logger
            )
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<List<LeaveRequestListDto>> Handle(
            GetLeaveRequestListQuery request, 
            CancellationToken cancellationToken)
        {
            // chek if it logged in employee

            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestListDto>>( leaveRequests );

            // Fill requests with employee information

            return requests;
        }
    }
}
