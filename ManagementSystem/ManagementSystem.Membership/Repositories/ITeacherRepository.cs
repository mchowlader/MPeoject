using ManagementSystem.Membership.Contexts;
using ManagementSystem.Membership.Entities;
using DevSkill.Data;

namespace ManagementSystem.Membership.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher, int, AcademicDbContext>
    {
    }
}