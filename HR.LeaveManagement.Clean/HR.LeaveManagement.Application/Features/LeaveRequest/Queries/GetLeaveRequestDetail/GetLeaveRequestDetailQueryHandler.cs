using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequesetRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequesetRepository, IMapper mapper)
        {
            _leaveRequesetRepository = leaveRequesetRepository;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequset = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequesetRepository.GetLeaveRequestWithDetails(request.Id));
            if (leaveRequset == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            }
            return leaveRequset;
        }
    }
}
