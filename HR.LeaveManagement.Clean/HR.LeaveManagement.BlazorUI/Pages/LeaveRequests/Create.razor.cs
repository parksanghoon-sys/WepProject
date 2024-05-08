using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] ILeaveTypeService leaveTypeService { get; set; }
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        LeaveRequestVM LeaveRequestVM { get; set; } = new LeaveRequestVM();
        List<LeaveTypeVM> leaveTypeVMs { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            leaveTypeVMs = await leaveTypeService.GetLeaveTypes();

        }
        private async Task HandleValidSubmit()
        {
            await leaveRequestService.CreateLeaveRequest(LeaveRequestVM);
            NavigationManager.NavigateTo("/leaverequests/");
        }
    }
}
