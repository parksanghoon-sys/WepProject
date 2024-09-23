using Blazored.Toast.Services;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }
        [Inject]
        public ILeaveAllocationService LeaveAllocationService { get; set; }
        [Inject]
        IToastService _toastService { get; set; }
        protected string Message { get; set; } = string.Empty;
        protected List<LeaveTypeVM> LeaveTypes { get; private set; }
        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create");
        }
        protected void AllocateLeaveType(int id)
        {
            LeaveAllocationService.CreateLeaveAllocations(id);
        }

        protected void EditLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
        }

        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }
        protected async Task DeleteLeaveType(int id)
        {
            var response = await LeaveTypeService.DeleteLeaveType(id);
            if (response.IsSuccess)
            {
                _toastService.ShowSuccess("Leave Type Deleted Success");
                await OnInitializedAsync();
            }
            else
            {
                Message = response.Message;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }
    }
}
