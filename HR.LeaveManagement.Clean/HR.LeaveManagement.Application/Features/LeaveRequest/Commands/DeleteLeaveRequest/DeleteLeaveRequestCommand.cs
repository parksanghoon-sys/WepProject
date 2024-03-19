using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
