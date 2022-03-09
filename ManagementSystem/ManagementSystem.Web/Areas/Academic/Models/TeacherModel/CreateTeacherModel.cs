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
    public class CreateTeacherModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public IFormFile FromFile { get; set; }

        private IFileStoreUtility _fileStoreUtility;
        private ISystemImageResizer _systemImageResizer;
        private ILifetimeScope _scope;
        private ITeacherService _service;


        public CreateTeacherModel(ITeacherService service,
            IFileStoreUtility fileStoreUtility, 
            ISystemImageResizer systemImageResizer )
        {
            _service = service;
        }

        public CreateTeacherModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<ITeacherService>();
            _fileStoreUtility = _scope.Resolve<IFileStoreUtility>();
            _systemImageResizer = _scope.Resolve<ISystemImageResizer>();
        }

        internal async Task CreateTeacherAsync()
        {
            if (FromFile != null)
            {
                var tempImage = new FileInfo( _fileStoreUtility.StoreFile(FromFile).filePath);
                var resizeImage = await _systemImageResizer.ProfileImageResizeAsync(tempImage);
                Photo = resizeImage.Name;
            }
            else
            {
                Photo = "DefaultImage.jpg";
            }

            var teacher = new Teacher()
            {
                Name = Name,
                Address = Address,
                Number = Number,
                Photo = Photo
            };

            await _service.CreateTeacherAsync(teacher);
        }
    }
}
