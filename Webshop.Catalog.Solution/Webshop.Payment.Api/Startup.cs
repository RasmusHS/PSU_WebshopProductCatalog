using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using PSU_PaymentGateway.Repository;
using PSU_PaymentGateway.Services;
using Rebus.Config;
using Serilog;
using Webshop.Payment.Api.Messages.Handlers;
using Shared.Messages.Events;

namespace PSU_PaymentGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string sequrl = Configuration.GetValue<string>("Settings:SeqLogAddress");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Service", "Payment.Api") //enrich with the tag "service" and the name of this service
                .WriteTo.Seq(sequrl)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Webshop.Payment.Api", Version = "v1" });
            });
            //add custom services
            services.AddSingleton<IMemoryRepository, MemoryRepository>();
            services.AddSingleton<IThrottleService, ThrottleService>();
            //add healthchecks
            services.AddHealthChecks();
            //Console.WriteLine(Configuration.GetConnectionString("MessageBroker"));
            services.AddRebus(
            rebus => rebus
                .Transport(t =>
                    t.UseRabbitMq(
                        Configuration.GetConnectionString("MessageBroker"),
                        "OrderPaymentQueue"))
                //.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
                //.Options(o => o.Decorate<ISerializer>(c => new CustomMessageDeserializer(c.Get<ISerializer>())))
                ,
            onCreated: async bus =>
            {
                //await bus.Advanced.Topics.Subscribe("OrderCreatedEvent"); // OrderCreatedEvent
                await bus.Subscribe<OrderCreatedEvent>();
                //await bus.Advanced.Topics.Subscribe("PaymentProcessedEvent");
            });

            services.AddRebusHandler<OrderCreatedEventHandler>();
            //services.AutoRegisterHandlersFromAssemblyOf<OrderCreatedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway v1"));
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Add Serilog to the logging pipeline
            loggerFactory.AddSerilog();

            //enable prometheus metrics
            app.UseHttpMetrics();            
            app.UseHealthChecks("/health");
            app.UseMetricServer();
        }
    }
}
