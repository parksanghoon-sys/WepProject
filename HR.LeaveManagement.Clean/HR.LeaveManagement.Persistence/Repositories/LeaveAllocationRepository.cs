using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(p => p.EmployeeId == userId 
                                        && p.LeaveTypeId == leaveTypeId 
                                        && p.Period == period);
        }

        public async Task<LeaveAllocation> GetAllocations(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations
                .FirstOrDefaultAsync(p => p.EmployeeId == userId && p.LeaveTypeId == leaveTypeId);
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWidthDetail(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(p => p.LeaveType)
                .FirstOrDefaultAsync(p => p.Id == id);

            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWidthDetail()
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Include(p => p.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationWidthDetail(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations
                .Where(p => p.EmployeeId == userId)
                .Include(p => p.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }
    }
}
