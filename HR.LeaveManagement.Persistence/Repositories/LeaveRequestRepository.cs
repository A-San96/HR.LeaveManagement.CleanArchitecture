using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await _context.LeaveRequests
            .AsNoTracking()
            .Include(q => q.LeaveType) // Get in same all data related to the leave type
            .ToListAsync();

        return leaveRequests;
    }

    public async Task<IReadOnlyList<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await _context.LeaveRequests
            .AsNoTracking()
            .Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType) // Get in same all data related to the leave type
            .ToListAsync();

        return leaveRequests;
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _context.LeaveRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveRequest;
    }
}
