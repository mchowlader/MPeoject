using Autofac;
using ManagementSystem.Foundation.Utilities;
using ManagementSystem.Membership.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementSystem.Web.Areas.Academic.Models.TeacherModel
{
    public class DataTeacherModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
        public string Photo { get; set; }

        private ILifetimeScope _scope;
        private ITeacherService _service;


        public DataTeacherModel(ITeacherService service)
        {
            _service = service;
        }

        public DataTeacherModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _service = _scope.Resolve<ITeacherService>();
        }

        public async Task<object> GetTeacherDataAsyns(DataTablesAjaxRequestModel dataTableModel)
        {
            var data = await _service.GetTeacherDataAsync(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "Photo", "Name", "Address", "Number" }));
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDispaly,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Photo,
                                record.Name,
                                record.Address,
                                record.Number.ToString(),
                                record.Id.ToString()
                        }
                        ).ToArray()
            };
        }

        public async Task DeleteDataAsync(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
