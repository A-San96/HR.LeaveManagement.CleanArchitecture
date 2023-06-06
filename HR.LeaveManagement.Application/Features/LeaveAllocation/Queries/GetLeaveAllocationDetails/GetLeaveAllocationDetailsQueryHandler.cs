using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler
    : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationDetailsQueryHandler> _logger;

    public GetLeaveAllocationDetailsQueryHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<GetLeaveAllocationDetailsQueryHandler> logger)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._logger = logger;
    }
    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        //Query the Database
        var leaveAllocation = await
            _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        // verify that record exist
        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        //Convert the data object to DTO object and return it
        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}
