namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails
{
    public class LeaveTypeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DefaltDays { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
