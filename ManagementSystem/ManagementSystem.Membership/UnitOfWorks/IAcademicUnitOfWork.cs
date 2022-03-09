using DevSkill.Data;
using ManagementSystem.Membership.Repositories;

namespace ManagementSystem.Membership.UnitOfWorks
{
    public interface IAcademicUnitOfWork : IUnitOfWork
    {
        public ITeacherRepository TeacherRepository { get; }
    }
}