using OfficeAccessControl.Core.Models;

namespace OfficeAccessControl.Core.Repositories
{
    public interface IOfficeLocationRepository
    {
        Task<OfficeLocation?> GetByIdWithRolesAsync(string id);
    }
}
