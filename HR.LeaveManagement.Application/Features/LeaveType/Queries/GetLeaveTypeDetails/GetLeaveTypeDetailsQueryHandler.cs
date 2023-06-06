using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    internal class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery,
        LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<LeaveTypeDetailsDto> Handle(
            GetLeaveTypeDetailsQuery request,
            CancellationToken cancellationToken)
        {
            //Query the Database
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);

            // verify that record exist
            if (leaveType is null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }

            //Convert the data object to DTO object
            var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

            // return DTO object
            return data;
        }
    }
}
