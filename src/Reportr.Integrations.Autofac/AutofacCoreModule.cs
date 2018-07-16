namespace Reportr.Integrations.Autofac
{
    using global::Autofac;
    using Reportr.Data;
    using Reportr.Data.Querying;
    using Reportr.Data.Querying.Functions;
    using Reportr.Filtering;
    using Reportr.Integrations.Autofac.Repositories;
    using System.Linq;

    /// <summary>
    /// Represents a custom core module for an Autofac container builder
    /// </summary>
    public class AutofacCoreModule : Module
    {
        /// <summary>
        /// Overrides the modules base load with custom delegate registrations
        /// </summary>
        /// <param name="builder">The container builder</param>
        protected override void Load
            (
                ContainerBuilder builder
            )
        {
            // TODO: register rendering and templating services

            var assemblies = builder.GetAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => typeof(IDataSource)
                   .IsAssignableFrom(t))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutofacDataSourceRepository>()
                   .As<IDataSourceRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => typeof(IQuery)
                   .IsAssignableFrom(t))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutofacQueryRepository>()
                   .As<IQueryRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => typeof(IQueryAggregateFunction)
                   .IsAssignableFrom(t))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => typeof(IReportDefinitionBuilder)
                   .IsAssignableFrom(t))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutofacReportDefinitionBuilderRepository>()
                   .As<IReportDefinitionBuilderRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ReportGenerator>()
                   .As<IReportGenerator>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ReportFilterGenerator>()
                   .As<IReportFilterGenerator>()
                   .InstancePerLifetimeScope();
        }
    }
}
