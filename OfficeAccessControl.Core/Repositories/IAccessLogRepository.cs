using OfficeAccessControl.Core.Models;

namespace OfficeAccessControl.Core.Repositories
{
    public interface IAccessLogRepository
    {
        Task AddAsync(AccessLog log);
    }
}
