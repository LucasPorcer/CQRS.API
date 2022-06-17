using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Orders.API.Domain.Interfaces.Repository.Order;
using System.Reflection;
using FluentValidation;
using Orders.API.InfraData.Database.Order;
using Orders.API.InfraData.Repositories.Order;
using Orders.API.Infra.Services.Commands.Order.Command;
using Microsoft.EntityFrameworkCore;
using Orders.API.Infra.Services.Commands.Order;
using Orders.API.Domain.Notifications;
using Orders.API.Application.Services.QuerieServices;
using Orders.API.Domain.Interfaces.Services;

namespace Orders.API.UI
{
    public class Startup
    {
        private object connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer("User ID=admin;Password=senha-sql;Host=localhost;Port=3306;Database=dev;Pooling=false;");
            });

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IEcomOrderService, EcomOrderService>();

            services.AddTransient<DomainNotification>();

            services.AddMediatR(typeof(Program).Assembly, typeof(OrderCommandHandler).Assembly);

            services.AddLogging();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders.API.UI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders.API.UI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
