using Autofac;
using ManagementSystem.Web.Areas.Academic.Models.TeacherModel;
using ManagementSystem.Web.Models.AccountModel;

namespace ManagementSystem.Web
{
    public class WebModule : Module
    {
      
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<CreateTeacherModel>().AsSelf();
            builder.RegisterType<EditTeacherModel>().AsSelf();
            builder.RegisterType<DataTeacherModel>().AsSelf();

            base.Load(builder);
        }
    }
}
