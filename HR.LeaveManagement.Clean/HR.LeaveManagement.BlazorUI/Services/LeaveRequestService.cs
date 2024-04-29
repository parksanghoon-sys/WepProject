using AutoMapper;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IMapper _mapper;

        public LeaveRequestService(IClient client, IMapper mapper)
            : base(client)
        {
            _mapper = mapper;
        }

        public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
        {
            try
            {
                var response = new Response<Guid>();
                var request = new ChangeLeaveRequestApprovalCommand { Approved = approved, Id = id };
                await _client.UpdateApprovalAsync(request);
                return response;
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<Response<Guid>> CancelLeaveRequest(int id)
        {
            try
            {
                var respose = new Response<Guid>();
                var request = new CancelLeaveRequestCommand { Id = id };
                await _client.CancelRequestAsync(request);
                return respose;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);                
            }
        }

        public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
        {
            try
            {
                var respose = new Response<Guid>();
                CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);                
                await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
                return respose;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            var leaveRequest =  await _client.LeaveRequestsGETAsync(id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);
        }
    }
}
