using Autofac;
using PremiumAccess.Application;
using PremiumAccess.Domain.Entities.Repositories;
using PremiumAccess.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumAccess.Infrastructure;

public class InfrastructureModule : Module
{
    private readonly string _connectionString;
    private readonly string _migrationAssembly;
    public InfrastructureModule(string connectionString, string migrationAssembly)
    {
        _connectionString = connectionString;
        _migrationAssembly = migrationAssembly;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

        builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
            .WithParameter("connectionString", _connectionString)
            .WithParameter("migrationAssembly", _migrationAssembly)
            .InstancePerLifetimeScope();


        builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
            .InstancePerLifetimeScope();

        builder.RegisterType<ContentRepository>().As<IContentRepository>()
            .InstancePerLifetimeScope();
    }
}
