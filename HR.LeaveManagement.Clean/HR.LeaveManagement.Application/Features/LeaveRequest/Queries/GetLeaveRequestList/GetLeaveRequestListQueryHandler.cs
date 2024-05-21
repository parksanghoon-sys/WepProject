using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequesetRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequesetRepository, IMapper mapper, IUserService userService)
        {
            _leaveRequesetRepository = leaveRequesetRepository;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<Domain.LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if(request.IsLoggedInUser)
            {
                var userid = _userService.UserId;
                leaveRequests = await _leaveRequesetRepository.GetLeaveRequestsWithDetails(userid);
                var employee = await _userService.GetEmployee(userid);

                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequesetRepository.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}
