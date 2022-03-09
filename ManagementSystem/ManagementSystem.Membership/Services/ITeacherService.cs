using ManagementSystem.Membership.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementSystem.Membership.Services
{
    public interface ITeacherService
    {
        Task CreateTeacherAsync(Teacher teacher);
        Task<(IList<Teacher> records, int total, int totalDispaly)> GetTeacherDataAsync
            (int pageIndex, int pageSize, string searchText, string sortText);
        Task<Teacher> LoadDataAsync(int id);
        Task UpdateAsync(Teacher user);
        Task DeleteAsync(int id);
    }
}