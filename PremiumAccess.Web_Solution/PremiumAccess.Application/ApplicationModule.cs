using Autofac;
using PremiumAccess.Application.Features.AccessStore;
using PremiumAccess.Domain.Features.AccessStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Application;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ContentManagementService>().As<IContentManagementService>()
            .InstancePerLifetimeScope();
    }
}
