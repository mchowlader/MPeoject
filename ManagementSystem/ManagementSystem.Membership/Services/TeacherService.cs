using ManagementSystem.Foundation.Services;
using ManagementSystem.Membership.BusinessObjects;
using ManagementSystem.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Membership.Services
{
    public class TeacherService : ITeacherService
    {
        private IAcademicUnitOfWork _unitOfWork;
        private IPathService _pathService;

        public TeacherService(IAcademicUnitOfWork unitOfWork, IPathService pathService)
        {
            _unitOfWork = unitOfWork;
            _pathService = pathService;
        }

        public async Task CreateTeacherAsync(Teacher teacher)
        {
            await _unitOfWork.TeacherRepository.AddAsync(
               new Entities.Teacher()
               {
                   Address = teacher.Address,
                   Name = teacher.Name,
                   Number = teacher.Number,
                   Photo = teacher.Photo,
               });

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.TeacherRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<(IList<Teacher> records, int total, int totalDispaly)> GetTeacherDataAsync(
            int pageIndex, int pageSize, string searchText, string sortText)
        {
            var userList = await _unitOfWork.TeacherRepository.GetDynamicAsync(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, null, pageIndex, pageSize, false);

            var result = (from user in userList.data
                          select new Teacher()
                          {
                              Name = user.Name,
                              Address = user.Address,
                              Number = user.Number,
                              Photo = user.Photo,
                              Id = user.Id
                          }).ToList();

            for (var i = 0; i < result.Count; i++)
            {
                if (result[i].Photo == null)
                {
                    var defaultProfileImage = _pathService.DefaultProfileImage;
                    result[i].Photo = _pathService.AttachPathWithDefaultProfileImage(defaultProfileImage);
                }
                else
                {
                    result[i].Photo = _pathService.AttachPathWithFile(result[i].Photo);
                }
            }

            return (result, userList.total, userList.totalDisplay);
        }

        public async Task<Teacher> LoadDataAsync(int id)
        {
            var teacher = await _unitOfWork.TeacherRepository.GetByIdAsync(id);

            return new Teacher()
            {
                Name = teacher.Name,
                Address = teacher.Address,
                Number = teacher.Number,
                Photo = teacher.Photo,
                Id=teacher.Id
            };
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            var userEntity = await _unitOfWork.TeacherRepository.GetByIdAsync(teacher.Id);

            if (userEntity != null)
            {
                userEntity.Name = teacher.Name;
                userEntity.Address = teacher.Address;
                userEntity.Number = teacher.Number;
                userEntity.Photo = teacher.Photo;

                await _unitOfWork.SaveAsync();
            }
        }
    }
}