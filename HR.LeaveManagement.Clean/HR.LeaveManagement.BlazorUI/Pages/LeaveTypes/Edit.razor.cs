using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Edit
    {
        [Inject]
        ILeaveTypeService _leaveTypeService {  get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }
        [Parameter]
        public int id { get; set; }
        public string Message { get; private set; }
        LeaveTypeVM leaveType = new LeaveTypeVM();
        protected override async Task OnParametersSetAsync()
        {
            leaveType = await _leaveTypeService.GetLeaveTypeDetails(id);
        }
        private async Task EditLeaveType()
        {
            var response = await _leaveTypeService.UpdateLeaveType(id, leaveType);
            if(response.IsSuccess)
            {
                _navigationManager.NavigateTo("/leavetypes");
            }
            Message = response.Message;
        }
    }
}
