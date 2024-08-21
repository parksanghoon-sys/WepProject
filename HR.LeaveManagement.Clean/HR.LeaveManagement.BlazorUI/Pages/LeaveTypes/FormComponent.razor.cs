using HR.LeaveManagement.BlazorUI.Models.LeaveTypes;
using HR.LeaveManagement.BlazorUI.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class FormComponent
    {
        [Parameter]
        public bool IsDisabled { get; set; } = false;
        [Parameter]
        public LeaveTypeVM LeaveType { get; set; }
        [Parameter]
        public string ButtonText { get; set; } = "Save";
        [Parameter]
        public EventCallback OnValidSubmit { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Debug.Write("TEST");
        }
    }
}
