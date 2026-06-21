using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypesDetailQueryHandler : IRequestHandler<GetLeaveTypesDetailsQuery, LeaveTypeDetailDto>
{
   private readonly IMapper _mapper;
   private readonly ILeaveTypeRepository _leaveTypeRepository;

   public GetLeaveTypesDetailQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
   {
      _mapper = mapper;
      _leaveTypeRepository = leaveTypeRepository;
   }

   public async Task<LeaveTypeDetailDto> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
   {
      // Query the database
      var leaveType = await _leaveTypeRepository.GetByIdAsync(request.id);
      if (leaveType == null)
      {
         throw new NotFoundException(nameof(LeaveType), request.id);
      }
      // Convert data objects to DTO objects
      var data = _mapper.Map<LeaveTypeDetailDto>(leaveType);

      // Returns DTO object
      return data;
   }
}
