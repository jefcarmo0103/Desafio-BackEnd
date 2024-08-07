using DesafioBackEnd.Application;
using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.Orchestration;
using DesafioBackEnd.Application.Orchestration.Dispatchers;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Motorcyle.Consumer;
using DesafioBackEnd.Domain.Core.Engines;
using DesafioBackEnd.Domain.Core.Interfaces.Engines;
using DesafioBackEnd.Domain.Core.Interfaces.Rules;
using DesafioBackEnd.Infra.Core;
using DesafioBackEnd.Infra.Core.ManagerFileBroker;
using DesafioBackEnd.Infra.Core.Mappings;
using DesafioBackEnd.Infra.Core.MessageBroker;
using DesafioBackEnd.Infra.Core.Repositories;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DesafioBackEnd.IoC
{
    public static class ContainerConfigurator
    {
        public static IServiceCollection AddAplicationDependencies(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(AsemblyRegister)));
            services.AddTransient<IManagerFileBus, ManagerFileBus>();

            DbDependencies(services);
            InfraDependencies(services);
            DomainCoreDependencies(services);
            ApplicationDependencies(services);
            ServicesQueuesDependencies(services);   

            return services;
        }

        private static void ServicesQueuesDependencies(IServiceCollection services)
        {
            string host = Environment.GetEnvironmentVariable("RABBIT_HOST") ?? throw new ArgumentNullException("Rabbit Host");
            string username = Environment.GetEnvironmentVariable("RABBIT_USERNAME") ?? throw new ArgumentNullException("Rabbit Username");
            string password = Environment.GetEnvironmentVariable("RABBIT_PASSWORD") ?? throw new ArgumentNullException("Rabbit Password");

            services.AddMassTransit(busConfig =>
            {
                busConfig.SetSnakeCaseEndpointNameFormatter();

                busConfig.AddConsumer<Motorcycle2024CreatedEventConsumer>();

                busConfig.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(host), h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    config.ReceiveEndpoint("motorcycleCreatedQueue", ep =>
                    {
                        ep.PrefetchCount = 10;
                        ep.UseMessageRetry(rt => rt.Interval(2, 100));
                        ep.ConfigureConsumer<Motorcycle2024CreatedEventConsumer>(context);
                        
                    });
                });
            });

            services.AddTransient<IEventBus, EventBus>();
        }

        private static void InfraDependencies(IServiceCollection services)
        {
            services.AddTransient<IDeliveryManRepository, DeliveryManRepository>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IRentPlanRepository, RentPlanRepository>();
            services.AddTransient<IRentRepository, RentRepository>();
            services.AddTransient<IMotorcycle2024Repository, Motorcycle2024Repository>();
            services.AddTransient<ITypeCnhRepository, TypeCnhRepository>();
        }

        private static void DomainCoreDependencies(IServiceCollection services)
        {
            services.AddTransient<ICalculatorEstimatedPriceEngine, CalculatorEstimatedPriceEngine>();
            var strategies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(IRentEstimatedValueRule).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var strategy in strategies)
            {
                services.AddTransient(
                    typeof(IRentEstimatedValueRule),
                    strategy);
            }
        }

        private static void ApplicationDependencies(IServiceCollection services)
        {
            services
                .AddScoped<IApplicationOperationDispatcher, ApplicationOperationDispatcher>()
                .AddScoped<ApplicationManager>();
        }

        private static void DbDependencies(IServiceCollection services)
        {
            services.AddDbContext<DesafioContext>();
        }

    }
}