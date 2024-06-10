using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequest;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Parameter] public int id { get; set; }

        string ClassName;
        string HeadingText;

        public LeaveRequestVM Model { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            Model = await leaveRequestService.GetLeaveRequest(id);
            UpdateUIState();
            StateHasChanged();
        }

        private void UpdateUIState()
        {
            if (Model == null)
            {
                ClassName = string.Empty;
                HeadingText = string.Empty;
                return;
            }

            if (Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }
            else if (Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
        }

        async Task ChangeApproval(bool approvalStatus)
        {
            await leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
            navigationManager.NavigateTo("/leaverequests/");
        }
    }
}