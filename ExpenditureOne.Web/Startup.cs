using AutoMapper;
using ExpenditureOne.BL;
using ExpenditureOne.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExpenditureOne.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ExpenditureContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ExpenditureContext")));
            services.AddSingleton(typeof(IExpenditureInitializer), typeof(ExpenditureInitializer));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebAutomapperProfile());
                mc.AddProfile(new BLAutomapperProfile());
            
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
