using BlazorBigexecutionExample.Models;
using BlazorBigexecutionExample.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorBigexecutionExample.Pages
{
    public partial class UniversityData
    {
        protected IEnumerable<University> universities;

        [Inject]
        protected IUniversityService UniversityService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            universities = await UniversityService.GetUniversities();
        }
    }
}