using Autofac;
using EmailProject.DataLayer.Context;
using EmailProject.Services.Interface;
using EmailProject.Services.Services;

namespace EmailProject.Modules
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>();
            builder.RegisterType<EmailService>().As<IEmailService>();
        }
    }
}
