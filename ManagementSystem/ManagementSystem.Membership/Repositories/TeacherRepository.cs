using ManagementSystem.Membership.Contexts;
using ManagementSystem.Membership.Entities;
using DevSkill.Data;

namespace ManagementSystem.Membership.Repositories
{
    public class TeacherRepository : Repository<Teacher, int , AcademicDbContext>, ITeacherRepository
    {
        public TeacherRepository(IAcademicDbContext dbContext)
            : base((AcademicDbContext)dbContext)
        {
        }
    }
}