using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationsQueryHandler
    : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

    public GetLeaveAllocationsQueryHandler(
        IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._logger = logger;
    }
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        // To Add
        // - Get Record for specific user
        // - Get allocations per employee

        // Query the database
        var leaveAllocations = await 
            _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        //Convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        // log
        _logger.LogInformation("Leave allocations were retrieved successfully");

        //return list of DTO objects
        return data;
    }
}
