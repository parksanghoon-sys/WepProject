using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequesetRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequsets = await _context.LeaveRequests
                .Include(p => p.LeaveType)
                .ToListAsync();
            return leaveRequsets;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequsets = await _context.LeaveRequests
                .Where(p => p.RequestingEmployeeId == userId)
                .Include(p => p.LeaveType)
                .ToListAsync();
            return leaveRequsets;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequset = await _context.LeaveRequests                
                .Include(p => p.LeaveType)
                .FirstOrDefaultAsync(p => p.Id == id);

            return leaveRequset;
        }
    }
}
