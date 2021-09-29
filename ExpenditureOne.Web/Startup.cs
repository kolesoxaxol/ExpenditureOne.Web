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
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Version="v1", Title="ExpenditureOne Api" }));
            services.AddDbContext<ExpenditureContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ExpenditureContext")));

            services.AddSingleton(typeof(IExpenditureInitializer), typeof(ExpenditureInitializer));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExpenditureService, ExpenditureService>();
            services.AddScoped<IExpenditureService2, ExpenditureService2>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericRepository2<>), typeof(GenericRepository2<>));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WebAutomapperProfile());
                mc.AddProfile(new BLAutomapperProfile());
            
            });

            // TODO: configurate it for improve security 
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                  );
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


            app.UseSwagger();
            app.UseSwaggerUI(sw=> sw.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenditureOne Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
