using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql;


namespace APIMDS
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ChatDbContext>(options =>
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IPersonnageRepository, PersonnageRepository>();
            services.AddScoped<IUniversRepository, UniversRepository>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

