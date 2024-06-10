using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail
{
    public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
    {
        private readonly ILeaveRequestRepository _leaveRequesetRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequesetRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequesetRepository = leaveRequesetRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveRequset = _mapper.Map<LeaveRequestDetailsDto>(await _leaveRequesetRepository.GetLeaveRequestWithDetails(request.Id));
            if (leaveRequset == null)
            {
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            }
            leaveRequset.Employee = await _userService.GetEmployee(leaveRequset.RequestingEmployeeId);
            return leaveRequset;
        }
    }
}
