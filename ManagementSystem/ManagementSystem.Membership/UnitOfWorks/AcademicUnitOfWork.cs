using DevSkill.Data;
using ManagementSystem.Membership.Contexts;
using ManagementSystem.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Membership.UnitOfWorks
{
    public class AcademicUnitOfWork : UnitOfWork, IAcademicUnitOfWork
    {
        public ITeacherRepository TeacherRepository { get; private set; }

        public AcademicUnitOfWork(IAcademicDbContext dbContext,
            ITeacherRepository teacherRepository)
            : base((AcademicDbContext)dbContext)
        {
            TeacherRepository = teacherRepository;
        }
    }
}