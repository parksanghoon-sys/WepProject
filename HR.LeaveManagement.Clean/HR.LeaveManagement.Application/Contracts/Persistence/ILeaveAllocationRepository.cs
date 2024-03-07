using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWidthDetail(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationWidthDetail();
        Task<List<LeaveAllocation>> GetLeaveAllocationWidthDetail(string userId);

        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetAllocations(string userId, int leaveTypeId);

    }
 
}
