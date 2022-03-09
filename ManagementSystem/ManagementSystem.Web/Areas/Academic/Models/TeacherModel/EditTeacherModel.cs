using Autofac;
using ManagementSystem.Foundation.Services;
using ManagementSystem.Membership.BusinessObjects;
using ManagementSystem.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ManagementSystem.Web.Areas.Academic.Models.TeacherModel
{
    public class EditTeacherModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Photo { get; set; }
        public IFormFile FormFile { get; set; }

        private ILifetimeScope _scope;
        private ITeacherService _service;
        private IPathService _pathService;
        private IFileStoreUtility _fileStoreUtility;
        private ISystemImageResizer _systemImageResizer;

        public EditTeacherModel(ITeacherService service,
            IPathService pathService,
            IFileStoreUtility fileStoreUtility,
            ISystemImageResizer systemImageResizer)
        {
            _service = service;
            _pathService = pathService;
            _systemImageResizer = systemImageResizer;
            _fileStoreUtility = fileStoreUtility;
        }

        public EditTeacherModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<ITeacherService>();
            _pathService = _scope.Resolve<IPathService>();
            _systemImageResizer = _scope.Resolve<ISystemImageResizer>();
            _fileStoreUtility = _scope.Resolve<IFileStoreUtility>();
        }

        internal async Task LoadDataAsync(int id)
        {
            var data = await _service.LoadDataAsync(id);

            if (data != null)
            {
                if (data.Photo == null)
                {
                    var defaultProfileImage = _pathService.DefaultProfileImage;
                    Photo = _pathService.AttachPathWithDefaultProfileImage(defaultProfileImage);
                }
                else
                {
                    Photo = _pathService.AttachPathWithFile(data.Photo);
                }

                Id = data.Id;
                Name = data.Name;
                Address = data.Address;
                Number = data.Number;
            }
        }

        internal async Task UpadteAsync()
        {
            if (FormFile != null)
            {
                var temporaryImage = new FileInfo(_fileStoreUtility.StoreFile(FormFile).filePath);
                var resizeImage = await _systemImageResizer.ProfileImageResizeAsync(temporaryImage);
                Photo = resizeImage.Name;
            }
            else
            {
                Photo = Photo.Remove(0, 7);
            }

            var user = new Teacher()
            {
                Id = Id,
                Name = Name,
                Address = Address,
                Number = Number,
                Photo = Photo
            };

            await _service.UpdateAsync(user);
        }
    }
}
