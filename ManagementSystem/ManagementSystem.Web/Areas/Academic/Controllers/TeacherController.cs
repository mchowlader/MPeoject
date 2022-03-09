using Autofac;
using ManagementSystem.Foundation.Utilities;
using ManagementSystem.Web.Areas.Academic.Models.TeacherModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ManagementSystem.Web.Areas.Academic.Controllers
{
    [Area("Academic")]
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly ILifetimeScope _scope;

        public TeacherController(ILifetimeScope scope, ILogger<TeacherController> logger)
        {
            _scope = scope;
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            var model = _scope.Resolve<CreateTeacherModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTeacherModel model)
        {
            model.Resolve(_scope);
            if (ModelState.IsValid)
            {
                try
                {
                    await model.CreateTeacherAsync();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Teacher not created");
                }
            }

            return RedirectToAction(nameof(Data));
        }

        public IActionResult Data()
        {
            var model = _scope.Resolve<DataTeacherModel>();
            model.Resolve(_scope);

            return View(model);
        }

        public async Task<JsonResult> GetData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<DataTeacherModel>();
            model.Resolve(_scope);
            var data = await model.GetTeacherDataAsyns(dataTableModel);

            return Json(data);
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var model = _scope.Resolve<EditTeacherModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.LoadDataAsync(Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "User not found.");
                }
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTeacherModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.UpadteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "User not updated.");
                }
            }

            return RedirectToAction(nameof(Data));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var model = _scope.Resolve<DataTeacherModel>();

            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    await model.DeleteDataAsync(Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "User not Deleted.");
                }
            }

            return RedirectToAction(nameof(Data));
        }

    }
}
